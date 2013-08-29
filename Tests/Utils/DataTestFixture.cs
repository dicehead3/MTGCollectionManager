using Data.Utils;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;

namespace Tests.Utils
{
    [TestFixture]
    public abstract class DataTestFixture
    {
        protected ISession Session;
        protected ISessionFactory SessionFactory;
        
        [SetUp]
        public void Setup()
        {
            NHibernateHelper.CreateDatabaseSchema();
            SessionFactory = NHibernateHelper.SessionFactory;
            Session = NHibernateHelper.SessionFactory.OpenSession();
            CurrentSessionContext.Bind(Session);
        }

        [TearDown]
        public void TearDown()
        {
            var session = CurrentSessionContext.Unbind(SessionFactory);
            session.Close();
            Session.Dispose();
        }
    }
}
