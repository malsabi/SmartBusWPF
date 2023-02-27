using System;
using CefSharp;
using System.IO;
using System.ComponentModel;
using System.Windows.Controls;

namespace SmartBusWPF.Views.UserControls
{
    public partial class LiveMapControl : UserControl
    {
        public LiveMapControl()
        {
            InitializeComponent();
            InitializeChromiumWebBrowser();
        }

        private void InitializeChromiumWebBrowser()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                string htmlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Html", "RealTimeMap.html");
                string html = File.ReadAllText(htmlFilePath);
                Browser.LoadHtml(html, htmlFilePath);
            }
        }
    }
}