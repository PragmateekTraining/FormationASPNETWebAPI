using Logging.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Client.WPF
{
    internal class LoggingServiceClient
    {
        private readonly HttpClient httpClient = new HttpClient();

        private const string baseUri = "http://localhost:8001/api/";

        public async Task<IEnumerable<Log>> GetAllLogs()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUri + "logs");

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Log[]>(json);
        }

        public async Task<IEnumerable<Log>> GetLogsFrom(DateTime from)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUri + "logs/from/" + from.ToString("yyyyMMddHHmmss"));

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Log[]>(json);
        }

        public async Task AddLog(Level level, string message)
        {
            await httpClient.PostAsync(baseUri + "logs/add/" + level + "/" + message, null);
        }
    }
}
