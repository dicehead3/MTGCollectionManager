using System;
using System.Linq;
using System.Text;
using Data;
using Data.Utils;
using Infrastructure.ApplicationSettings;
using Infrastructure.Cache;
using Infrastructure.Encryption;
using Infrastructure.Loggers;
using Infrastructure.PasswordPolicies;
using MassTransit;
using UI.AdminConsole.DemoData;

namespace UI.AdminConsole
{
    class Program
    {
        static void Main(params string[] args)
        {
            //NHibernateProfiler.Initialize();
            System.IO.Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            Bus.Initialize(sbc =>
            {
                sbc.UseMsmq(x =>
                    {
                        x.VerifyMsmqConfiguration();
                        x.UseMulticastSubscriptionClient();
                    });
                sbc.ReceiveFrom("msmq://localhost/admin_queue");
            });

            if (args.Any())
            {
                ExecuteOption(args[0]);
            }
            else
            {
                string option;
                do
                {
                    // Create menu
                    var menu = new StringBuilder();
                    menu.AppendLine("(1) Create database schema");
                    menu.AppendLine("(2) Create database schema and demo data");
                    menu.AppendLine("(3) Log test");
                    menu.AppendLine("(x) Exit");

                    // Display menu and wait for user input
                    Console.WriteLine(menu);
                    option = Console.ReadKey().KeyChar.ToString();
                    Console.WriteLine();
                    ExecuteOption(option);
                    Console.WriteLine();
                } while (option.ToLower() != "x");
            }
        }

        private static void ExecuteOption(string option)
        {
            switch (option.ToLower())
            {
                case "1":
                    CreateDatabaseSchema();
                    break;
                case "2":
                    CreateDatabaseSchemaAndDemoData();
                    break;
                case "3":
                    LogTest();
                    break;
                case "x":
                    break;
                default:
                    Console.WriteLine("That's not an option!");
                    break;
            }
        }

        private static void CreateDatabaseSchema()
        {
            NHibernateHelper.CreateDatabaseSchema();
            Console.WriteLine("Database schema created");
        }

        private static void CreateDatabaseSchemaAndDemoData()
        {
            CreateDatabaseSchema();

            var session = NHibernateHelper.SessionFactory.OpenSession();
            var passwordPolicy = new RegularExpressionPasswordPolicy(".{5,}$");
            var translationsRepository = new TranslationRepository(session, new InMemoryKeyValueCache());
            var applicationSettings = new ApplicationSettings();
            var encryptor = new DefaultEncryptor();

            var userRepository = new UserRepository(session, passwordPolicy, applicationSettings, encryptor);

            // Create administrators
            var administrators = PocoGenerator.CreateAdministrators(userRepository);

            // Create users
            var users = PocoGenerator.CreateUsers(100, userRepository).ToList(); // Weird, need to do 'ToList()' to get the method to execute...

            session.Transaction.Begin();

            // Create translations
            var translations = PocoGenerator.CreateTranslations(translationsRepository);

            session.Transaction.Commit();
        }

        private static void LogTest()
        {
            var logger = new NLogLogger(new ApplicationSettings());
            logger.Debug("Just testing debug", "Debug");
            logger.Info("Just testing info", "Info");
            logger.Warn("Just testing warning", "Warning");
            logger.Error("Just testing error", "Error");
            logger.Fatal("Testing with exception", new Exception("TestException"), "Some details again");
        }
    }
}
