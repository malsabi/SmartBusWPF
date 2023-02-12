using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SmartBusWPF.Common.Extensions
{
    public static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public static Task<int> ReceiveAsync(this Socket socket, byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            if (socket == null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            socket.BeginReceive(buffer, offset, size, socketFlags, ar =>
            {
                try
                {
                    tcs.TrySetResult(socket.EndReceive(ar)); 
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            }, state: null);
            return tcs.Task;
        }

        public static Task<int> SendAsync(this Socket socket, byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            if (socket == null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            TaskCompletionSource<int> tcs = new TaskCompletionSource<int>();
            socket.BeginSend(buffer, offset, size, socketFlags, ar =>
            {
                try
                {
                    tcs.TrySetResult(socket.EndSend(ar));
                }
                catch (Exception e)
                {
                    tcs.TrySetException(e);
                }
            }, state: null);
            return tcs.Task;
        }
        
        public static Task<int> SendAsync(this Socket socket, byte[] message)
        {
            if (socket == null)
            {
                throw new ArgumentNullException(nameof(socket));
            }

            byte[] messageLength = BitConverter.GetBytes(message.Length);
            byte[] buffer = new byte[messageLength.Length + message.Length];

            Array.Copy(messageLength, buffer, messageLength.Length);
            Array.Copy(message, 0, buffer, messageLength.Length, message.Length);

            return socket.SendAsync(buffer, 0, buffer.Length, SocketFlags.None);
        }
    }
}