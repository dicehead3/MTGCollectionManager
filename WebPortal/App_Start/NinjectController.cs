using System.Collections;
using System.Web;
using System.Web.Mvc;
using Data.Repositories;
using Data.Utils;
using Domain.AbstractRepositories;
using Infrastructure.Encryption;
using NHibernate;
using Ninject;
using Ninject.Web.Common;

namespace WebPortal.App_Start
{
    public class NinjectController : DefaultControllerFactory
    {
        private readonly IKernel _ninjectKernel;

        public NinjectController()
        {
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) _ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _ninjectKernel.Bind<IUserRepository>().To<UserRepository>();
            _ninjectKernel.Bind<ICardRepository>().To<CardRepository>();
            _ninjectKernel.Bind<IDeckRepository>().To<DeckRepository>();
            _ninjectKernel.Bind<IEncryptor>().To<DefaultEncryptor>();
            _ninjectKernel.Bind<ISession>().ToMethod(x => GetRequestSession()).InRequestScope();
        }

        private static ISession GetRequestSession()
        {
            return NHibernateHelper.SessionFactory.OpenSession();
        }  
    }
}