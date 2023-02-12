using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace SmartBusWPF.Common.Interfaces.Services
{
    public interface IServerSocketService
    {
        public ICollection<IClientSocketService> Clients { get; }

        public IPEndPoint EndPoint { get; }

        public bool IsListening { get; }

        public TcpListener Listener { get; }

        public delegate void OnServerStartedListeningEvent(IPEndPoint endPoint);
        public event OnServerStartedListeningEvent OnServerStartedListening;

        public delegate void OnServerStoppedListeningEvent();
        public event OnServerStoppedListeningEvent OnServerStoppedListening;

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

        public void StartListening(int port);

        public void AcceptClientAsync();

        public void StopListening();

        public IClientSocketService GetFirstClient();
    }
}