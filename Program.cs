using System;
using System.Data;

namespace CreateFluentAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            IDbConnection connection = null;

            connection = FluentSqlConnection
                .CreateConnection(config => {
                    config.ConnectionName = "Connection1";
                })
                .ForServer("TestServer")
                .WithDatabase("CustomerDB")
                .AsUser("MyUser")
                .WithPassword("SecretPassword")
                .Connect();

            Console.WriteLine("Connection created!");
        }
    }
}
