using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using MyWorkingHours.Data.Models;
using MyWorkingHours.Data.Repository.Contracts;

namespace MyWorkingHours.Workers
{
    public class SystemObserverWorker : BackgroundService
    {
        private readonly ILogger<SystemObserverWorker> _logger;
        private readonly IStatusTimeStampRepository _stampRepository;
        private readonly ISessionSwitchRepository _switchRepository;

        public SystemObserverWorker(ILogger<SystemObserverWorker> logger, ISessionSwitchRepository switchRepository,
            IStatusTimeStampRepository stampRepository)
        {
            _logger = logger;
            _switchRepository = switchRepository;
            _stampRepository = stampRepository;
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
                    await _stampRepository.CreateAsync(statusStamp);
                }
                else
                {
                    await Task.Delay(1000, stoppingToken);
                    var statusStamp = new StatusTimeStamp(locked);
                    await _stampRepository.CreateAsync(statusStamp);
                }
            }
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    _switchRepository.CreateAsync(new SessionSwitch("SessionLock"));
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _switchRepository.CreateAsync(new SessionSwitch("SessionUnlock"));
                    break;
                case SessionSwitchReason.ConsoleConnect:
                    _switchRepository.CreateAsync(new SessionSwitch("ConsoleConnect"));
                    break;
                case SessionSwitchReason.ConsoleDisconnect:
                    _switchRepository.CreateAsync(new SessionSwitch("ConsoleDisconnect"));
                    break;
                case SessionSwitchReason.RemoteConnect:
                    _switchRepository.CreateAsync(new SessionSwitch("RemoteConnect"));
                    break;
                case SessionSwitchReason.RemoteDisconnect:
                    _switchRepository.CreateAsync(new SessionSwitch("RemoteDisconnect"));
                    break;
                case SessionSwitchReason.SessionLogon:
                    _switchRepository.CreateAsync(new SessionSwitch("SessionLogon"));
                    break;
                case SessionSwitchReason.SessionLogoff:
                    _switchRepository.CreateAsync(new SessionSwitch("SessionLogoff"));
                    break;
                case SessionSwitchReason.SessionRemoteControl:
                    _switchRepository.CreateAsync(new SessionSwitch("SessionRemoteControl"));
                    break;
            }
        }
    }
}