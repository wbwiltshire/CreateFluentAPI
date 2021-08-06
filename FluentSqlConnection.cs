using System;
using System.Data;

namespace CreateFluentAPI
{
    public class FluentSqlConnection : 
        IServerSelectionStage, IDatabaseSelectionStage, IUserSelectionStage, IPasswordSelectionStage, IConnectionIntializerStage
    {
        private string server;
        private string database;
        private string user;
        private string password;

        // Hide the ctor 
        private FluentSqlConnection ()            
        {

        }

        // Create a public method for creation / instantiation
        public static IServerSelectionStage CreateConnection(Action<ConnectionConfiguration> config )
        {
            ConnectionConfiguration configuration = new ConnectionConfiguration();
            // Be careful, it might be null
            config?.Invoke(configuration);          // Apply the delegate/action on the new instatiated version 
            return new FluentSqlConnection();
        }

        public IDatabaseSelectionStage ForServer(string s)
        {
            server = s;
            return this;
        }

        public IUserSelectionStage WithDatabase(string d)
        {
            database = d;
            return this;
        }

        public IPasswordSelectionStage AsUser(string u)
        {
            user = u;
            return this;
        }

        public IConnectionIntializerStage WithPassword(string p)
        {
            password = p;
            return this;
        }

        public IDbConnection Connect()
        {
            IDbConnection connection = null;
            
            return connection;
        }


    }

    public class ConnectionConfiguration
    {
        public string ConnectionName { get; set; }
    }

    // 
    /// Interfaces should be in another file
    ///  
    /// 

    // Define the first "stage" of the API
    public interface IServerSelectionStage
    {
        // Next stage
        public IDatabaseSelectionStage ForServer(string server);
    }

    // Define the next "stage" after the Server Selection
    public interface IDatabaseSelectionStage
    {
        // Next stage
        public IUserSelectionStage WithDatabase(string database);
    }

    // Define the next "stage" after the User Selection
    public interface IUserSelectionStage
    {
        // Next stage
        public IPasswordSelectionStage AsUser(string user);
    }

    // Define the next "stage" after the Password Selection
    public interface IPasswordSelectionStage
    {
        // Next stage
        public IConnectionIntializerStage WithPassword(string password);
    }

    // Define the final "stage" - Connection Initializatoin
    public interface IConnectionIntializerStage
    {
        // Next stage
        public IDbConnection Connect();
    }
}