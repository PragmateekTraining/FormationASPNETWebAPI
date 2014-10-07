using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Model
{
    public enum Level
    {
        _ = 0,
        INFO,
        WARNING,
        ERROR
    }

    [Table(Name = "Logs")]
    public class Log
    {
        [Column(DbType = "BIGINT IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
        public long ID { get; set; }

        [Column(DbType = "DATETIME2")]
        public DateTime Timestamp { get; set; }

        [Column(DbType = "VARCHAR(10)")]
        public Level Level { get; set; }

        [Column]
        public string Message { get; set; }
    }
}
