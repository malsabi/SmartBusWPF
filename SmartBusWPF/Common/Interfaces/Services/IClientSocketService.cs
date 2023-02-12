using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface IClientSocketService
    {
        public bool IsConnected { get; }

        public bool IsAuthenticated { get; }

        public bool EnableReconnect { get; }

        public int MaximumMessageSize { get; }

        public IPEndPoint EndPoint { get; }

        public Socket Handler { get; }

        public delegate void OnClientConnectedEvent(IClientSocketService client);
        public event OnClientConnectedEvent OnClientConnected;

        public delegate void OnClientDisconnectedEvent(IClientSocketService client);
        public event OnClientDisconnectedEvent OnClientDisconnected;

        public delegate void OnClientMessageReceivedEvent(IClientSocketService client, string command, string content);
        public event OnClientMessageReceivedEvent OnClientMessageReceived;

        public delegate void OnClientMessageSentEvent(IClientSocketService client, string command, string content);
        public event OnClientMessageSentEvent OnClientMessageSent;

        public delegate void OnClientExceptionEvent(IClientSocketService client, Exception ex);
        public event OnClientExceptionEvent OnClientException;

        public void ConnectAsync(string host, int port);

        public Task DisconnectAsync();

        public Task ReceiveAsync();

        public Task SendAsync(string command, string content);
    }
}