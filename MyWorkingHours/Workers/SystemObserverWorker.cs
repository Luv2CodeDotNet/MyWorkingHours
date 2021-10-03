using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using MyWorkingHours.Data.DataAccess;
using MyWorkingHours.Data.Models;

namespace MyWorkingHours.Workers
{
    public class SystemObserverWorker : BackgroundService
    {
        private readonly ILogger<SystemObserverWorker> _logger;
        private readonly SqliteDbContext _dbContext;

        public SystemObserverWorker(ILogger<SystemObserverWorker> logger, SqliteDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var locked = Process.GetProcessesByName("logonui").Any();
                
                if (locked)
                {
                    await Task.Delay(1000, stoppingToken);

                    var statusStamp = new StatusTimeStamp(locked);

                    await _dbContext.StatusTimeStamps.AddAsync(statusStamp, stoppingToken);
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }
                else
                {
                    await Task.Delay(1000, stoppingToken);
                    
                    var statusStamp = new StatusTimeStamp(locked);

                    await _dbContext.StatusTimeStamps.AddAsync(statusStamp, stoppingToken);
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }
            }
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("SessionLock"));
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("SessionUnlock"));
                    break;
                case SessionSwitchReason.ConsoleConnect:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("ConsoleConnect"));
                    break;
                case SessionSwitchReason.ConsoleDisconnect:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("ConsoleDisconnect"));
                    break;
                case SessionSwitchReason.RemoteConnect:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("RemoteConnect"));
                    break;
                case SessionSwitchReason.RemoteDisconnect:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("RemoteDisconnect"));
                    break;
                case SessionSwitchReason.SessionLogon:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("SessionLogon"));
                    break;
                case SessionSwitchReason.SessionLogoff:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("SessionLogoff"));
                    break;
                case SessionSwitchReason.SessionRemoteControl:
                    _dbContext.SessionSwitches.Add(new SessionSwitch("SessionRemoteControl"));
                    break;
            }
        }
    }
}