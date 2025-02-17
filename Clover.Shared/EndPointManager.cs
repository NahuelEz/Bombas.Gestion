using Clover.DbLayer;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clover.Shared
{
    public sealed class EndPointManager : IDisposable
    {
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly Socket _socket;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;
        private bool _closing;

        public User AssociatedUser { get; set; }
        public IPEndPoint RemoteEndPoint { get { return (IPEndPoint)_socket.RemoteEndPoint; } }
        public Task ReadTask { get; }

        public static async Task<EndPointManager> ConnectAsync(IPEndPoint remoteEndPoint, Action<EndPointManager, Envelope> envelopeReceivedCallback)
        {
            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            await Task.Factory.FromAsync<EndPoint>(socket.BeginConnect, socket.EndConnect, remoteEndPoint, null);
            return new EndPointManager(socket, envelopeReceivedCallback);
        }
        public static async Task<EndPointManager> AcceptAsync(Socket listener, Action<EndPointManager, Envelope> envelopeReceivedCallback)
        {
            Socket clientSocket = await Task.Factory.FromAsync(listener.BeginAccept, listener.EndAccept, null);
            return new EndPointManager(clientSocket, envelopeReceivedCallback);
        }

        public void SendEnvelope(Envelope envelope)
        {
            _semaphore.Wait();
            try
            {
                if (!_closing)
                {
                    _writer.WriteLine(envelope.Serialize());
                    _writer.Flush();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public async Task SendEnvelopeAsync(Envelope envelope)
        {
            await _semaphore.WaitAsync();
            try
            {
                if (!_closing)
                {
                    await _writer.WriteLineAsync(envelope.Serialize());
                    await _writer.FlushAsync();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
        public void Shutdown()
        {
            _Shutdown(SocketShutdown.Send);
        }
        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();
            _socket.Close();
        }

        private EndPointManager(Socket socket, Action<EndPointManager, Envelope> envelopeReceivedCallback)
        {
            _socket = socket;
            Stream stream = new NetworkStream(_socket);
            _reader = new StreamReader(stream, Encoding.UTF8, false, 1024, true);
            _writer = new StreamWriter(stream, Encoding.UTF8, 1024, true);
            ReadTask = _ConsumeSocketAsync(envelopeReceivedCallback);
        }
        private async Task _ConsumeSocketAsync(Action<EndPointManager, Envelope> envelopeReceivedCallback)
        {
            string line;
            while ((line = await _reader.ReadLineAsync()) != null)
            {
                envelopeReceivedCallback(this, Envelope.FromJsonString(line));
            }
            _Shutdown(SocketShutdown.Both);
        }
        private void _Shutdown(SocketShutdown reason)
        {
            _semaphore.Wait();
            try
            {
                if (!_closing)
                {
                    _socket.Shutdown(reason);
                    _closing = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
