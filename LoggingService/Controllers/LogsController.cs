using Logging.Model;
using Logging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Logging.Service.Controllers
{
    public class LogsController : ApiController
    {
        private readonly string connectionString = null;

        public LogsController()
            : this(null)
        {
        }

        public LogsController(string connectionString)
        {
            this.connectionString = connectionString ?? "prodConnection";
        }

        private ILogsRepository NewRepository()
        {
            return LogsRepositoryFactory.New(connectionString);
        }

        public IEnumerable<Log> GetAll()
        {
            using (var repository = NewRepository())
            {
                return repository.GetAllLogs();
            }
        }

        public async Task<IEnumerable<Log>> GetFrom(string from)        
        {
            // Simulate a delay
            await Task.Delay(2000);

            DateTime fromDate = DateTime.ParseExact(from, "yyyyMMddHHmmss", null);

            using (var repository = NewRepository())
            {
                return repository.GetLogsFrom(fromDate);
            }
        }

        public void Add(Level level, string message)
        {
            using (var repository = NewRepository())
            {
                repository.Add(level, message);
            }
        }
    }
}
