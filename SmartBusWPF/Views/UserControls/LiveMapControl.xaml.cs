using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;

namespace SmartBusWPF.Views.UserControls
{
    public partial class LiveMapControl : UserControl
    {
        public LiveMapControl()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions("--allow-insecure-localhost");
                CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, null, options);
                await webview.EnsureCoreWebView2Async(environment);
                webview.CoreWebView2.PermissionRequested += PermissionRequestedHandler;
                webview.CoreWebView2.ContextMenuRequested += ContextMenuRequestedHandler;
                webview.Source = new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Html", "RealTimeMap.html"));
            }
        }

        private void PermissionRequestedHandler(object sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            e.State = CoreWebView2PermissionState.Allow;
        }

        private void ContextMenuRequestedHandler(object sender, CoreWebView2ContextMenuRequestedEventArgs e)
        {
            e.Handled = true;
        }
    }
}