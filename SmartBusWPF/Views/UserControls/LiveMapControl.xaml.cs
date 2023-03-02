using System;
using CefSharp;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SmartBusWPF.Views.UserControls
{
    public partial class LiveMapControl : UserControl
    {
        private bool isWorkerRunning;

        public LiveMapControl()
        {
            InitializeComponent();
            InitializeChromiumWebBrowser();
        }

        private void InitializeChromiumWebBrowser()
        {
            isWorkerRunning = false;
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Html", "RealTimeMap.html");
                string html = File.ReadAllText(htmlFilePath);
                Browser.LoadHtml(html, htmlFilePath);
                Browser.LoadingStateChanged += LoadingStateChangedHandler;
            }
        }

        private void LoadingStateChangedHandler(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                RunWorker();
            }
        }

        private async void RunWorker()
        {
            if (isWorkerRunning)
            {
                return;
            }
            await Task.Run(async () =>
            {
                isWorkerRunning = true;
                while (App.Current != null && App.Current.BusDriverSession.IsTripStarted)
                {
                    string currentLocation = App.Current.BusDriverSession.Bus.CurrentLocation;

                    if (!string.IsNullOrEmpty(currentLocation))
                    {
                        float longitude = float.Parse(currentLocation.Split('|')[0]);
                        float latitude = float.Parse(currentLocation.Split('|')[1]);

                        await Dispatcher.BeginInvoke(new Action(() =>
                        {
                            Browser.ExecuteScriptAsync("refreshLocation", longitude, latitude);
                        }));
                    }
                    await Task.Delay(3000);
                }
            });
        }
    }
}