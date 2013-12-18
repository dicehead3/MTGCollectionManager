using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using Domain.AbstractRepository;
using Infrastructure.ApplicationSettings;
using Infrastructure.Cache;
using Infrastructure.Encryption;
using Infrastructure.FilterAttributes;
using Infrastructure.Loggers;
using Infrastructure.PasswordPolicies;
using Infrastructure.Translations;
using MassTransit;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using NHibernate;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using UI.Web.Cultures;

[assembly: WebActivator.PreApplicationStartMethod(typeof(UI.Web.App_Start.NinjectSetup), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(UI.Web.App_Start.NinjectSetup), "Stop")]

namespace UI.Web.App_Start
{
    public static class NinjectSetup
    {
        public static IKernel Kernel { get; private set; }
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            // Global filters
            // https://github.com/ninject/ninject.web.mvc/wiki/Dependency-injection-for-filters
            // http://stackoverflow.com/a/9005664/426840
            kernel.BindFilter<LogActionFilter>(FilterScope.Global, 1);
            //kernel.BindFilter<HandleErrorAndLogAttribute>(FilterScope.Global, 2);
            
            RegisterServices(kernel);
            Kernel = kernel;

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IApplicationSettings>().To<ApplicationSettings>()
                .InSingletonScope();

            var applicationSettings = kernel.Get<IApplicationSettings>();

            kernel.Bind<ISession>().ToMethod(x => GetRequestSession()).InRequestScope();
            kernel.Bind<IPasswordPolicy>().To<RegularExpressionPasswordPolicy>()
                .WithConstructorArgument("regularExpression", applicationSettings.PasswordPolicy);
            kernel.Bind<ILogger>().ToMethod(x =>
            {
                var httpContext = HttpContext.Current;
                var className = x.Request.ParentContext == null ? x.Request.Service.FullName : x.Request.ParentContext.Request.Service.FullName;

                return httpContext != null ?
                    new NLogLogger(applicationSettings, className, new HttpContextWrapper(httpContext)) :
                    new NLogLogger(applicationSettings, className);
            });
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ILogRepository>().To<LogRepository>();
            kernel.Bind<IEncryptor>().To<DefaultEncryptor>();
            kernel.Bind<IKeyValueCache>().To<InMemoryKeyValueCache>().InSingletonScope();
            kernel.Bind<ICultureService>().To<CultureService>();
            kernel.Bind<ITranslationRepository>().To<TranslationRepository>();

            kernel.Bind<IServiceBus>().ToMethod(context => ServiceBusFactory.New(sbc =>
            {
                sbc.UseMsmq(x =>
                {
                    x.VerifyMsmqConfiguration();
                    x.UseMulticastSubscriptionClient();
                });
                sbc.ReceiveFrom("msmq://localhost/web_queue");
                sbc.SetDefaultTransactionTimeout(TimeSpan.FromMinutes(5));
            }
            )).InSingletonScope();

            kernel.Bind<ITranslationService>().ToMethod(context =>
            {
                var culture = applicationSettings.DefaultCulture;

                try
                {
                    culture = kernel.Get<ICultureService>().GetCulture();
                }
                catch (Exception exception)
                {
                    kernel.Get<ILogger>().Fatal("Failed to retrieve culture from user", exception);
                }

                if (!applicationSettings.AcceptedCultures.Contains(culture))
                {
                    culture = applicationSettings.DefaultCulture;
                }

                var translationsRepository = kernel.Get<ITranslationRepository>();

                return new TranslationService(translationsRepository.GetAll(), culture);
            }).InRequestScope();
        }

        /// <summary>
        /// The NHibernate session is stored in the HttpContext items.
        /// This is a key-value collection which can be used to store and share
        /// data during the handlng of one request.
        /// This method is used by NinjectSetup to bind the NHibernate session to ISession,
        /// which is used by the repositories.
        /// Concluding, we follow the session-per-request pattern which is a best practice for web applications
        /// </summary>
        private static ISession GetRequestSession()
        {
            IDictionary httpContextItems = HttpContext.Current.Items;

            ISession session;
            if (!httpContextItems.Contains(MvcApplication.SessionKey))
            {
                // Create an NHibernate session for this request
                session = MvcApplication.SessionFactory.OpenSession();
                httpContextItems.Add(MvcApplication.SessionKey, session);
            }
            else
            {
                // Re-use the NHibernate session for this request
                session = (ISession)httpContextItems[MvcApplication.SessionKey];
            }
            return session;
        }  
    }
}
