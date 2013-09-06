using System.Web.Mvc;
using Data.Repositories;
using Data.Utils;
using Domain.AbstractRepositories;
using Infrastructure.Encryption;
using NHibernate;
using Ninject;
using Ninject.Web.Common;

namespace Web.UI.App_Start
{
    public class NinjectController : DefaultControllerFactory
    {
        public NinjectController()
        {
            Kernel = new StandardKernel();
            AddBindings();
        }

        public static IKernel Kernel { get; private set; }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)Kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            Kernel.Bind<IUserRepository>().To<UserRepository>();
            Kernel.Bind<ICardRepository>().To<CardRepository>();
            Kernel.Bind<IDeckRepository>().To<DeckRepository>();
            Kernel.Bind<IEncryptor>().To<DefaultEncryptor>();
            Kernel.Bind<ISession>().ToMethod(x => GetRequestSession()).InRequestScope();
        }

        private static ISession GetRequestSession()
        {
            return NHibernateHelper.SessionFactory.OpenSession();
        }
    }
}