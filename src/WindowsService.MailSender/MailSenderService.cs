using System;
using System.Reflection;
using System.ServiceProcess;
using Autofac;
using Data;
using Data.Utils;
using Infrastructure.ApplicationSettings;
using Infrastructure.Cache;
using Infrastructure.Loggers;
using Infrastructure.Mailers;
using Infrastructure.TemplateMailMessages;
using Infrastructure.Translations;
using Magnum.Extensions;
using MassTransit;
using NHibernate;
using RazorMailMessage;
using RazorMailMessage.TemplateCache;
using RazorMailMessage.TemplateResolvers;

namespace WindowsService.MailSender
{
    public partial class MailSenderService : ServiceBase
    {
        private readonly IContainer _container;
        private IServiceBus _bus;

        public MailSenderService()
        {
            InitializeComponent();

            _container = BuildContainer();
        }

        protected override void OnStart(string[] args)
        {
            _bus = _container.Resolve<IServiceBus>();
        }

        protected override void OnStop()
        {
            _bus.Dispose();
        }

        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            // Register all consumers in this assembly
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Implements<IConsumer>())
                .AsSelf();

            // Register service bus
            builder.Register(c => ServiceBusFactory.New
                (
                    sbc =>
                    {
                        sbc.UseMsmq(x =>
                        {
                            x.VerifyMsmqConfiguration();
                            x.UseMulticastSubscriptionClient();
                        });
                        sbc.ReceiveFrom("msmq://localhost/test_queue");


                        // This will find all of the consumers in the container and register them with the bus.
                        // Resolve<ILifeTimeScope> is used to get all registrations from the container, the loadfrom method filters out all IConsumers and ISaga's
                        // Loadfrom is an extension method for autofac implemented in masstransit (Separate package: MassTransit.Autofac)
                        sbc.Subscribe(x => x.LoadFrom(c.Resolve<ILifetimeScope>()));
                    }
                )
            )
            .As<IServiceBus>()
            .SingleInstance();

            // Register translation service
            builder.Register(c =>
                {
                    var translations = c.Resolve<ITranslationRepository>().GetAll();
                    return new TranslationService(translations, new ApplicationSettings().DefaultCulture);
                }
            )
            .As<ITranslationService>();

            // Register razor mail message factory
            builder.RegisterType<RazorMailMessageFactory>()
                .WithParameter(new NamedParameter("templateResolver", new DefaultTemplateResolver("Infrastructure", "TemplateMailMessages")))
                .WithParameter(new NamedParameter("templateBase", typeof(ViewBaseClass<>)))
                .WithParameter(new NamedParameter("dependencyResolver", new Func<Type, object>(x => _container.Resolve(x)))) // This is somewhat weird, use service bus like construction
                .WithParameter(new NamedParameter("templateCache", new InMemoryTemplateCache()))
                .As<IRazorMailMessageFactory>();

            // Register smtp mailer
            builder.Register(c =>
                {
                    var applicationSettings = c.Resolve<IApplicationSettings>();
                    return new SmtpMailer
                    (
                        applicationSettings.SmtpHost,
                        applicationSettings.SmtpPort,
                        applicationSettings.SmtpUsername,
                        applicationSettings.SmtpPassword,
                        applicationSettings.SmtpSslEnabled,
                        c.Resolve<ILogger>()
                    );
                }
            )
            .As<IMailer>();

            builder.Register(c => NHibernateHelper.SessionFactory.OpenSession()).As<ISession>();
            builder.RegisterType<InMemoryKeyValueCache>().As<IKeyValueCache>();
            builder.RegisterType<TranslationRepository>().As<ITranslationRepository>();
            builder.RegisterType<ApplicationSettings>().As<IApplicationSettings>();
            builder.RegisterType<NLogLogger>().As<ILogger>();
            
            return builder.Build();
        }
    }
}
