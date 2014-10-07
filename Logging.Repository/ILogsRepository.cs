using Logging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Repository
{
    public interface ILogsRepository : IDisposable
    {
        IEnumerable<Log> GetAllLogs();
        IEnumerable<Log> GetLogsFrom(DateTime from);
        void Add(Level level, string message);
    }
}
