using Logging.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Repository
{
    internal class LoggingContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }

        public LoggingContext(DbConnection connection)
            : base(connection, false)
        {
        }

        public LoggingContext(string connectionString)
            : base(connectionString)
        {
        }
    }

    public class LogsRepository : IDisposable
    {
        private readonly LoggingContext context = null;

        public LogsRepository(DbConnection connection)
        {
            context = new LoggingContext(connection);
        }

        public LogsRepository(string connectionString)
        {
            context = new LoggingContext(connectionString);
        }

        public IEnumerable<Log> GetLogsFrom(DateTime from)
        {
            return context.Logs.Where(log => log.Timestamp >= from);
        }

        public void Add(Level level, string message)
        {
            Log log = new Log
            {
                Level = level,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            context.Logs.Add(log);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
