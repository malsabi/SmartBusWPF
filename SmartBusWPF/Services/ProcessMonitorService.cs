using System;
using System.IO;
using System.Diagnostics;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class ProcessMonitorService : IProcessMonitorService
    {
        public string ProcessPath { get; set; }

        public event IProcessMonitorService.OnProcessMonitorException OnException;

        public ProcessMonitorService()
        {
            ProcessPath = "./Assets/Apps/SmartBus.HuskyLens.exe";
        }

        public void StartProcess()
        {
            try
            {
                if (!IsProcessRunning())
                {
                    Process process = new Process();
                    process.StartInfo.FileName = ProcessPath;
                    process.Start();
                }
            }
            catch (Exception ex)
            {
                OnException?.Invoke(new Exception("An error occurred while starting the process: " + ex.Message));
            }
        }

        public bool IsProcessRunning()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ProcessPath));
                return processList.Length > 0;
            }
            catch (Exception ex)
            {
                OnException?.Invoke(new Exception("An error occurred while checking if the process is running: " + ex.Message));
                return false;
            }
        }

        public void TerminateProcess()
        {
            try
            {
                if (IsProcessRunning())
                {
                    Process[] processList = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(ProcessPath));
                    foreach (Process process in processList)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                OnException?.Invoke(new Exception("An error occurred while terminating the process: " + ex.Message));
            }
        }

        public void RestartProcess()
        {
            try
            {
                TerminateProcess();
                StartProcess();
            }
            catch (Exception ex)
            {
                OnException?.Invoke(new Exception("An error occurred while restarting the process: " + ex.Message));
            }
        }
    }
}