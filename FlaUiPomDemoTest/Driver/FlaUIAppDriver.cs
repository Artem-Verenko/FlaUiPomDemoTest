using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Logging;
using FlaUI.UIA3;
using FlaUiPomDemoTest.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace FlaUiPomDemoTest.Driver
{
    public class FlaUIAppDriver
    {
        private Dictionary<string, Application> _apps = new Dictionary<string, Application>();
        private UIA3Automation _automation;

        // Launches an app, supports both Store and Desktop apps
        public void LaunchApp(string appKey, bool isStoreApp)
        {
            var appPath = AppConfig.GetAppPath(appKey);

            Application app;
            // Launch the application
            app = isStoreApp ? Application.LaunchStoreApp(appPath) : Application.Launch(appPath);

            _automation = new UIA3Automation();

            // Wait for the main window to appear with retries
            var mainWindow = WaitForMainWindow(app);

            if (mainWindow != null)
            {
                _apps[appKey] = app; // Store the app instance in the dictionary
                Logger.Default.Info($"Launched the application: {appKey}.");
            }
            else
            {
                throw new Exception($"Failed to retrieve the main window for app: {appKey}");
            }
        }

        // Waits for the main window of the application to become available
        private Window WaitForMainWindow(Application app, int timeoutInSeconds = 30)
        {
            Window mainWindow = null;
            var startTime = DateTime.Now;

            // Retry until the window is found or the timeout is reached
            while ((DateTime.Now - startTime).TotalSeconds < timeoutInSeconds)
            {
                try
                {
                    mainWindow = app.GetMainWindow(_automation);
                    if (mainWindow != null && mainWindow.IsEnabled)
                    {
                        return mainWindow;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Default.Warn($"Waiting for the main window: {ex.Message}");
                }

                Thread.Sleep(1000); // Wait 1 second before retrying
            }

            return null; // Return null if the window wasn't found in the timeout period
        }

        // Returns the main window of the launched app
        public Window GetMainWindow(string appKey)
        {
            if (_apps.ContainsKey(appKey))
            {
                var app = _apps[appKey];
                return app.GetMainWindow(_automation);
            }

            throw new ArgumentException($"No main window found for app: {appKey}");
        }

        // Attempts to close the app gracefully, or forcefully if it does not close
        public void CloseApp(string appKey)
        {
            if (_apps.ContainsKey(appKey))
            {
                var app = _apps[appKey];
                try
                {
                    if (!app.HasExited) // Try to close gracefully
                    {
                        app.Close();
                    }

                    if (!app.HasExited) // If still running, force close
                    {
                        Logger.Default.Warn($"Application {appKey} did not exit gracefully. Killing the process.");
                        app.Kill();
                    }

                    Logger.Default.Info($"Closed the application: {appKey}.");
                }
                catch (Exception ex)
                {
                    Logger.Default.Error($"Failed to close the application: {appKey}. Error: {ex.Message}");
                }
                finally
                {
                    _apps.Remove(appKey); // Remove app from the list after closing
                }
            }
            else
            {
                Logger.Default.Warn($"Application {appKey} was not found in the running apps list.");
            }
        }

        // Closes all running apps that were started by the driver
        public void CloseAllApps()
        {
            foreach (var appKey in _apps.Keys.ToList()) // Convert to List to avoid modifying the dictionary during iteration
            {
                CloseApp(appKey);
            }
        }

        private void KillProcessIfRunning(string appKey)
        {
            try
            {
                var appPath = AppConfig.GetAppPath(appKey);
                var processName = Path.GetFileNameWithoutExtension(appPath); // Get process name from app path
                var processes = Process.GetProcessesByName(processName); // Find processes by name

                if (processes.Any())
                {
                    foreach (var p in processes)
                    {
                        if (!p.HasExited)
                        {
                            p.Kill();
                            p.WaitForExit(); // Ensure the process has exited
                            Logger.Default.Info($"Killed process: {p.ProcessName}");
                        }
                    }
                }
                else
                {
                    Logger.Default.Info($"No running process found for: {processName}");
                }
            }
            catch (Exception ex)
            {
                Logger.Default.Error($"Failed to kill process for app: {appKey}. Error: {ex.Message}");
            }
        }

        // Clears app data (if needed)
        public void ClearAppData(string appDataPath)
        {
            try
            {
                if (Directory.Exists(appDataPath))
                {
                    Directory.Delete(appDataPath, true);
                    Logger.Default.Info($"Cleared app data at {appDataPath}");
                }
            }
            catch (Exception ex)
            {
                Logger.Default.Error($"Failed to clear app data at {appDataPath}: {ex.Message}");
            }
        }
    }
}
