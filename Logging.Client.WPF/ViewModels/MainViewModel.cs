using GalaSoft.MvvmLight;
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
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Log> Logs { get; set; }
        public RelayCommand StartMonitoringCommand { get; set; }

        private bool isMonitoring;
        public bool IsMonitoring
        {
            get { return isMonitoring; }
            set
            {
                Set(ref isMonitoring, value);
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                Set(ref isRefreshing, value);
            }
        }

        public IList<Level> Levels { get; set; }
        public Level NewLogLevel { get; set; }

        private string newLogMessage;
        public string NewLogMessage
        {
            get { return newLogMessage; }
            set
            {
                Set(ref newLogMessage, value);
                AddLogCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddLogCommand { get; set; }

        private readonly LoggingServiceClient loggingServiceClient = new LoggingServiceClient();

        public MainViewModel()
        {
            Logs = new ObservableCollection<Log>();
            StartMonitoringCommand = new RelayCommand(StartMonitoring);

            Levels = (Level[])Enum.GetValues(typeof(Level));
            AddLogCommand = new RelayCommand(AddLog, () => !string.IsNullOrWhiteSpace(NewLogMessage));
        }

        private async void StartMonitoring()
        {
            IsMonitoring = true;

            IEnumerable<Log> initialLogs = await loggingServiceClient.GetAllLogs();

            foreach (Log log in initialLogs)
            {
                Logs.Add(log);
            }

            while (true)
            {
                await Task.Delay(2000);

                try
                {
                    IsRefreshing = true;
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
                finally
                {
                    IsRefreshing = false;
                }
            }
        }

        private async void AddLog()
        {
            await loggingServiceClient.AddLog(NewLogLevel, NewLogMessage);
        }
    }
}
