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
            CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions("--allow-insecure-localhost");
            CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, null, options);
            await webview.EnsureCoreWebView2Async(environment);
            webview.CoreWebView2.PermissionRequested += PermissionRequestedHandler;
        }
        private void PermissionRequestedHandler(object sender, CoreWebView2PermissionRequestedEventArgs e)
        {
            e.State = CoreWebView2PermissionState.Allow;
        }
    }
}