using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using System;
using System.IO;

namespace test_api.Configuration
{
    public class SessionFactory
    {
        private static ISessionFactory iSessionFactory;

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    if (iSessionFactory == null)
                    {
                        iSessionFactory = BuildSessionFactory();
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(Directory.GetCurrentDirectory())) 
                 .AddJsonFile("appsettings.json", optional: false)
                 .AddJsonFile($"appsettings.{envName}.json", optional: false)
                 .Build();

            return Fluently.Configure()
                 .Database(MySQLConfiguration.Standard.ConnectionString(configuration.GetConnectionString("DataBase")))
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                 .BuildSessionFactory();
        }

        /* Create session */
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(t => t.Namespace == "test_api.Domain.Model");
        }
    }
}
