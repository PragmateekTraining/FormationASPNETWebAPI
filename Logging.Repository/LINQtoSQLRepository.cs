using Logging.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Repository
{
    internal class LINQToSQLLoggingContext : DataContext
    {
        public Table<Log> Logs
        {
            get { return GetTable<Log>(); }
        }

        private void SetUpDatabase()
        {
            if (!DatabaseExists())
            {
                CreateDatabase();

                /*string script = @"CREATE TABLE Logs(ID INT PRIMARY KEY AUTO_INCREMENT, Timestamp DATETIME2, Level VARCHAR(10), Message VARCHAR(MAX))";

                ExecuteCommand(script);*/
            }
        }

        public LINQToSQLLoggingContext(DbConnection connection)
            : base(connection)
        {
            SetUpDatabase();
        }

        public LINQToSQLLoggingContext(string connectionString)
            : base(connectionString)
        {
            SetUpDatabase();
        }
    }

    public class LINQtoSQLRepository : ILogsRepository
    {
        private readonly DataContext context = null;

        public LINQtoSQLRepository(DbConnection connection)
        {
            context = new LINQToSQLLoggingContext(connection);
        }

        public LINQtoSQLRepository(string connectionString)
        {
            ConnectionStringSettings match = ConfigurationManager.ConnectionStrings[connectionString];

            if (match != null)
            {
                connectionString = match.ConnectionString;
            }

            context = new LINQToSQLLoggingContext(connectionString);
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return context.GetTable<Log>().ToArray();
        }

        public IEnumerable<Log> GetLogsFrom(DateTime from)
        {
            return context.GetTable<Log>().Where(log => log.Timestamp > from).ToArray();
        }

        public void Add(Level level, string message)
        {
            DateTime now = DateTime.UtcNow;

            Log log = new Log
            {
                Level = level,
                Message = message,
                Timestamp = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Utc)
            };

            context.GetTable<Log>().InsertOnSubmit(log);
            context.SubmitChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
