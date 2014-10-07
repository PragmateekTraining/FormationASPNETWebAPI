using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Repository
{
    public class LogsRepositoryFactory
    {
        public static ILogsRepository New(string connectionString)
        {
            return new LINQtoSQLRepository(connectionString);
        }
    }
}
