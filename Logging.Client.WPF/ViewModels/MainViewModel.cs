using GalaSoft.MvvmLight.Command;
using Logging.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Client.WPF
{
    public class MainViewModel
    {
        public ObservableCollection<Log> Logs { get; set; }
        public RelayCommand StartMonitoringCommand { get; set; }

        public IList<Level> Levels { get; set; }
        public Level NewLogLevel { get; set; }
        public string NewLogMessage { get; set; }
        public RelayCommand AddLogCommand { get; set; }

        private readonly LoggingServiceClient loggingServiceClient = new LoggingServiceClient();

        public MainViewModel()
        {
            Logs = new ObservableCollection<Log>();
            StartMonitoringCommand = new RelayCommand(StartMonitoring);

            Levels = (Level[])Enum.GetValues(typeof(Level));
            AddLogCommand = new RelayCommand(AddLog);
        }

        private async void StartMonitoring()
        {
            IEnumerable<Log> initialLogs = await loggingServiceClient.GetAllLogs();

            foreach (Log log in initialLogs)
            {
                Logs.Add(log);
            }

            while (true)
            {
                await Task.Delay(2000);

                DateTime last = DateTime.MinValue;
                if (Logs.Count != 0)
                {
                    last = Logs.Last().Timestamp;
                }

                IEnumerable<Log> newLogs = await loggingServiceClient.GetLogsFrom(last);

                foreach (Log newLog in newLogs)
                {
                    Logs.Add(newLog);
                }
            }
        }

        private async void AddLog()
        {
            await loggingServiceClient.AddLog(NewLogLevel, NewLogMessage);
        }
    }
}
