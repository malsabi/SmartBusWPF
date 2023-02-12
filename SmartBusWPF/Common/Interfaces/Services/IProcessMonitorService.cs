using System;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface IProcessMonitorService
    {
        public string ProcessPath { get; }

        public delegate void OnProcessMonitorException(Exception ex);
        public event OnProcessMonitorException OnException;
        
        public void StartProcess();

        public bool IsProcessRunning();

        public void TerminateProcess();

        public void RestartProcess();
    }
}