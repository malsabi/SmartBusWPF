using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using SmartBusWPF.Commands;
using System.Threading.Tasks;
using SmartBusWPF.Common.Extensions;
using SmartBusWPF.Common.Interfaces.Services;

namespace SmartBusWPF.Services
{
    public class ClientSocketService : IClientSocketService
    {
        public bool IsConnected { get; private set; }

        public bool IsAuthenticated { get; private set; }
        
        public bool EnableReconnect { get; private set; }

        public int MaximumMessageSize { get; private set; }

        public IPEndPoint EndPoint { get; private set; }

        public Socket Handler { get; private set; }

        public event IClientSocketService.OnClientConnectedEvent OnClientConnected;
        
        public event IClientSocketService.OnClientDisconnectedEvent OnClientDisconnected;
        
        public event IClientSocketService.OnClientMessageReceivedEvent OnClientMessageReceived;
        
        public event IClientSocketService.OnClientMessageSentEvent OnClientMessageSent;

        public event IClientSocketService.OnClientExceptionEvent OnClientException;

        public ClientSocketService(Socket Handler, bool IsConnected, bool IsAuthenticated, bool EnableReconnect, int MaximumMessageSize)
        {
            this.Handler = Handler;
            this.IsConnected = IsConnected;
            this.IsAuthenticated = IsAuthenticated;
            this.EnableReconnect = EnableReconnect;
            this.MaximumMessageSize = MaximumMessageSize;
            
            if (Handler == null)
            {
                throw new ArgumentNullException(nameof(Handler));
            }
            if (MaximumMessageSize <= 0 || MaximumMessageSize >= 1024 * 1024)
            {
                throw new ArgumentOutOfRangeException(nameof(MaximumMessageSize));
            }

            EndPoint = Handler.RemoteEndPoint as IPEndPoint;
        }

        /// <summary>
        /// Connects to the server by giving the host and port. If the EnableReconnect was set to true, 
        /// it will attempt to reconnect to the server if the connection was lost.
        /// </summary>
        /// <param name="host">Represents the host or the ip address.</param>
        /// <param name="port">Represents the port number.</param>
        public async void ConnectAsync(string host, int port)
        {
            await Task.Run(async () =>
            {
                while (EnableReconnect)
                {
                    if (!IsConnected)
                    {
                        try
                        {
                            await Handler.ConnectAsync(host, port);
                            OnClientConnected?.Invoke(this);
                            IsConnected = true;
                        }
                        catch
                        {
                            IsConnected = false;
                        }
                    }
                    await Task.Delay(1000);
                }
            });
        }

        /// <summary>
        /// Disconnects the client from the server and sets the <see cref="IsConnected"/> to false.
        /// </summary>
        public async Task DisconnectAsync()
        {
            if (IsConnected)
            {
                await Handler.DisconnectAsync(false);
                OnClientDisconnected?.Invoke(this);
                EndPoint = null;
                IsConnected = false;
            }
        }

        /// <summary>
        /// Continues to receive messages from the sender. Once a message is received it will raise an event <see cref="OnClientMessageReceived"/>.
        /// </summary>
        /// <returns>Returns an async task.</returns>
        public async Task ReceiveAsync()
        {
            await Task.Run(async () =>
            {
                while (IsConnected)
                {
                    try
                    {
                        byte[] headerBytes = new byte[4];
                        int headerBytesLength = await Handler.ReceiveAsync(headerBytes, 0, headerBytes.Length, SocketFlags.None);

                        if (headerBytesLength != 4)
                        {
                            OnClientException?.Invoke(this, new Exception("Invalid Header Bytes Length"));
                            await DisconnectAsync();
                            return;
                        }

                        int messageSize = BitConverter.ToInt32(headerBytes);

                        if (messageSize <= 0 || messageSize >= MaximumMessageSize)
                        {
                            OnClientException?.Invoke(this, new Exception("Invalid Message Size"));
                            await DisconnectAsync();
                            return;
                        }

                        byte[] messageBytes = new byte[messageSize];

                        int messageBytesLength = await Handler.ReceiveAsync(messageBytes, 0, messageBytes.Length, SocketFlags.None);

                        if (messageBytesLength != messageSize)
                        {
                            OnClientException?.Invoke(this, new Exception("The Received Message Length Is Invalid"));
                            await DisconnectAsync();
                            return;
                        }

                        string message = Encoding.UTF8.GetString(messageBytes);

                        if (!message.Contains("|"))
                        {
                            OnClientException?.Invoke(this, new Exception("Invalid Message Format"));
                            await DisconnectAsync();
                            return;
                        }

                        string[] messageParts = message.Split('|');

                        string command = messageParts[0];
                        string content = messageParts[1];

                        if (!IsAuthenticated)
                        {
                            if (command == SocketCommands.HANDSHAKE_COMMAND)
                            {
                                await SendAsync(SocketCommands.ACKNOWLEDGE_COMMAND, "SmartBus");
                                OnClientConnected?.Invoke(this);
                                IsAuthenticated = true;
                            }
                            else
                            {
                                OnClientException?.Invoke(this, new Exception("Invalid Handshake Command"));
                                await DisconnectAsync();
                                return;
                            }
                        }
                        else
                        {
                            OnClientMessageReceived?.Invoke(this, command, content);
                        }
                    }
                    catch (Exception ex)
                    {
                        OnClientException?.Invoke(this, ex);
                        await DisconnectAsync();
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        /// <param name="message">Represents the message content to be sent to the server.</param>
        /// <returns>Returns an async task.</returns>
        public async Task SendAsync(string command, string content = "")
        {
            await Task.Run(async () => 
            {
                if (IsConnected)
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(string.Format("{0}|{1}", command, content));
                    await Handler.SendAsync(messageBytes);
                    OnClientMessageSent?.Invoke(this, command, content);
                }
            });
        }
    }
}