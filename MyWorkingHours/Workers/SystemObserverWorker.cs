using System;
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

                    var statusStamp = GetStatusTimeStamp(locked);

                    await _dbContext.StatusTimeStamps.AddAsync(statusStamp, stoppingToken);
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }
                else
                {
                    await Task.Delay(1000, stoppingToken);
                    
                    var statusStamp = GetStatusTimeStamp(locked);

                    await _dbContext.StatusTimeStamps.AddAsync(statusStamp, stoppingToken);
                    await _dbContext.SaveChangesAsync(stoppingToken);
                }
            }
        }

        private static StatusTimeStamp GetStatusTimeStamp(bool locked)
        {
            var statusStamp = new StatusTimeStamp
            {
                MachineName = Environment.MachineName,
                UserName = Environment.UserName,
                ScreenLocked = locked,
                TimeStamp = DateTime.Now,
                Guid = Guid.NewGuid()
            };
            return statusStamp;
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _dbContext.SessionSwitches.Add(new SessionSwitch
                    {
                        Guid = Guid.NewGuid(),
                        MachineName = Environment.MachineName,
                        UserName = Environment.UserName,
                        SwitchReason = "SessionLock",
                        TimeStamp = DateTime.Now
                    });
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _dbContext.SessionSwitches.Add(new SessionSwitch
                    {
                        Guid = Guid.NewGuid(),
                        MachineName = Environment.MachineName,
                        UserName = Environment.UserName,
                        SwitchReason = "SessionUnlock",
                        TimeStamp = DateTime.Now
                    });
                    break;
                case SessionSwitchReason.ConsoleConnect:
                    break;
                case SessionSwitchReason.ConsoleDisconnect:
                    break;
                case SessionSwitchReason.RemoteConnect:
                    break;
                case SessionSwitchReason.RemoteDisconnect:
                    break;
                case SessionSwitchReason.SessionLogon:
                    break;
                case SessionSwitchReason.SessionLogoff:
                    break;
                case SessionSwitchReason.SessionRemoteControl:
                    break;
            }
        }
    }
}