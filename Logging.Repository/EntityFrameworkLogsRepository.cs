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

    public class EntityFrameworkLogsRepository : ILogsRepository
    {
        private readonly LoggingContext context = null;

        public EntityFrameworkLogsRepository(DbConnection connection)
        {
            context = new LoggingContext(connection);
        }

        public EntityFrameworkLogsRepository(string connectionString)
        {
            context = new LoggingContext(connectionString);
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return context.Logs.ToArray();
        }

        public IEnumerable<Log> GetLogsFrom(DateTime from)
        {
            return context.Logs.Where(log => log.Timestamp > from).ToArray();
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

            context.Logs.Add(log);
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
