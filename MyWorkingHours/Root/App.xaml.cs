using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyWorkingHours.Common;
using MyWorkingHours.Data.DataAccess;
using MyWorkingHours.Data.Repository.Contracts;
using MyWorkingHours.Data.Repository.Implementations;
using MyWorkingHours.Views;
using MyWorkingHours.Workers;
using Serilog;
using Serilog.Events;

namespace MyWorkingHours.Root
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost? _host;
        private readonly MainWindow _mainWindow;
        private readonly NotifyIcon _notifyIcon;

        /// <summary>
        ///     Get called on app start up
        /// </summary>
        public App()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.MouseDoubleClick += NotifyIconOnClick;
            _mainWindow = new MainWindow();
            _mainWindow.Closing += MainWindowOnClosing;

            Log.Logger = LoggerConfiguration().CreateLogger();

            try
            {
                _host = CreateHostBuilder().Build();
                CreateDatabase();
                Log.Information("MyWorkingHours successfully started");
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Failed to start the Service!");
            }
        }

        private void CreateDatabase()
        {
            using var dbContext = new SqliteDbContext(_host.Services.GetService<DbContextOptions<SqliteDbContext>>());
            dbContext.Database.MigrateAsync();
        }

        /// <summary>
        ///     Create Serilog Logger Configuration
        /// </summary>
        /// <returns>Returns LoggerConfigurations</returns>
        private static LoggerConfiguration LoggerConfiguration()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.Debug()
                .WriteTo.File(ApplicationDirectory.GetApplicationFilePath(SpecialAppFile.LogsFile),
                    rollingInterval: RollingInterval.Day)
                .Enrich.FromLogContext();
        }

        /// <summary>
        ///     Create host builder
        /// </summary>
        /// <returns>Return configured IHostBuilder</returns>
        private static IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                .UseSerilog()
                .ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configurationBuilder.AddJsonFile("Configuration/appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<SystemObserverWorker>();
                    services.AddEntityFrameworkSqlite().AddDbContext<SqliteDbContext>((_, builder) =>
                    {
                        builder.UseSqlite(ApplicationDirectory.GetApplicationFilePath(SpecialAppFile.SqliteDbFile));
                    });
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<ISessionSwitchRepository, SessionSwitchRepository>();
                    services.AddScoped<IStatusTimeStampRepository, StatusTimeStampRepository>();
                });
        }

        /// <summary>
        ///     Handler for closing MainWindow. The MainWindow gets hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowOnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            _mainWindow.Hide();
        }

        /// <summary>
        ///     Handler for the Tray Icon. Handles click events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotifyIconOnClick(object? sender, EventArgs e)
        {
            if (_mainWindow.IsVisible)
            {
                if (_mainWindow.WindowState == WindowState.Minimized) _mainWindow.WindowState = WindowState.Normal;
                _mainWindow.Activate();
            }
            else
            {
                _mainWindow.Show();
            }
        }

        /// <summary>
        ///     Override OnStartup method
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            Debug.Assert(_host != null, nameof(_host) + " != null");
            await _host.StartAsync();
            SetIcon();
            ShowIcon();
            base.OnStartup(e);
        }

        /// <summary>
        ///     Set the Icon visibility to true to show the icon in the app bar
        /// </summary>
        private void ShowIcon()
        {
            _notifyIcon.Visible = true;
        }

        /// <summary>
        ///     Set icon properties
        /// </summary>
        private void SetIcon()
        {
            _notifyIcon.Icon = new Icon("Resources/watch_purple.ico");
            _notifyIcon.Text = "Meine Arbeitszeit";
        }

        /// <summary>
        ///     Override OnExit method
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnExit(ExitEventArgs e)
        {
            Debug.Assert(_host != null, nameof(_host) + " != null");
            await _host.StopAsync();
            _host.Dispose();
            _notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}