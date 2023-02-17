using System;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class ServerSocketService : IServerSocketService
    {
        public ICollection<IClientSocketService> Clients { get; private set; }

        public IPEndPoint EndPoint { get; private set; }
        
        public bool IsListening { get; private set; }

        public TcpListener Listener { get; private set; }

        public event IServerSocketService.OnServerStartedListeningEvent OnServerStartedListening;
        
        public event IServerSocketService.OnServerStoppedListeningEvent OnServerStoppedListening;
        
        public event IServerSocketService.OnClientConnectedEvent OnClientConnected;
        private void SetOnClientConnected(IClientSocketService client)
        {
            OnClientConnected?.Invoke(client);
        }
        
        public event IServerSocketService.OnClientDisconnectedEvent OnClientDisconnected;
        private void SetOnClientDisconnected(IClientSocketService client)
        {
            OnClientDisconnected?.Invoke(client);
        }
        
        public event IServerSocketService.OnClientMessageReceivedEvent OnClientMessageReceived;
        private void SetOnClientMessageReceived(IClientSocketService client, string command, string content)
        {
            OnClientMessageReceived?.Invoke(client, command, content);
        }
        
        
        public event IServerSocketService.OnClientMessageSentEvent OnClientMessageSent;
        private void SetOnClientMessageSent(IClientSocketService client, string command, string content)
        {
            OnClientMessageSent?.Invoke(client, command, content);
        }

        public event IServerSocketService.OnClientExceptionEvent OnClientException;
        private void SetOnClientException(IClientSocketService client, Exception ex)
        {
            OnClientException?.Invoke(client, ex);
        }

        public ServerSocketService()
        {
            Clients = new List<IClientSocketService>();
            IsListening = false;
        }

        public void StartListening(int port)
        {
            if (!IsListening)
            {
                EndPoint = new IPEndPoint(IPAddress.Any, port);
                Listener = new TcpListener(EndPoint);
                Listener.Start();
                IsListening = true;
                OnServerStartedListening?.Invoke(EndPoint);
            }
        }
        
        public async void AcceptClientAsync()
        {
            await Task.Run(async () => 
            {
                while (IsListening)
                {
                    try
                    {
                        Socket clientHandler = await Listener.AcceptSocketAsync();
                        ClientSocketService clientSocket = new(clientHandler, true, false, false, 1024 * 16);
                        AddClient(clientSocket);
                    }
                    catch
                    {
                    }
                }
            });
        }

        public void StopListening()
        {
            if (IsListening)
            {
                Listener.Stop();
                if (Clients.Any())
                {
                    foreach (ClientSocketService clientSocket in Clients.ToList())
                    {
                        RemoveClient(clientSocket);
                    }
                }
                IsListening = false;
                OnServerStoppedListening?.Invoke();
            }
        }

        public IClientSocketService GetFirstClient()
        {
            return Clients.FirstOrDefault();
        }

        private async void AddClient(ClientSocketService clientSocket)
        {
            clientSocket.OnClientConnected += SetOnClientConnected;
            clientSocket.OnClientMessageSent += SetOnClientMessageSent;
            clientSocket.OnClientMessageReceived += SetOnClientMessageReceived;
            clientSocket.OnClientDisconnected += SetOnClientDisconnected;
            clientSocket.OnClientException += SetOnClientException;
            await clientSocket.ReceiveAsync();
            Clients.Add(clientSocket);
        }

        private async void RemoveClient(ClientSocketService clientSocket)
        {
            clientSocket.OnClientConnected -= SetOnClientConnected;
            clientSocket.OnClientMessageSent -= SetOnClientMessageSent;
            clientSocket.OnClientMessageReceived -= SetOnClientMessageReceived;
            clientSocket.OnClientDisconnected -= SetOnClientDisconnected;
            clientSocket.OnClientException -= SetOnClientException;
            await clientSocket.DisconnectAsync();
            Clients.Remove(clientSocket);
        }
    }
}