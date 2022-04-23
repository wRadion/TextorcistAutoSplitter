using System;
using System.Net.Sockets;
using System.Text;

namespace TextorcistAutosplitter
{
    internal class LiveSplitClient : IDisposable
    {
        private readonly Socket _socket;

        public bool Connected => _socket.Connected;

        public LiveSplitClient(int port = 16834)
        {
            _socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect("localhost", port);
        }

        public void Split()
        {
            if (!_socket.Connected) return;
            _socket.Send(Encoding.ASCII.GetBytes("split\r\n"));
        }

        public void Dispose()
        {
            if (!_socket.Connected) return;
            _socket.Close();
        }
    }
}
