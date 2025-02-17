using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.ChatServer
{
    public partial class ServerAdmin : Form
    {
        private object lockObject = new object();
        private Socket listener;
        private List<EndPointManager> connectedClients = new List<EndPointManager>();
        private Dictionary<int, Queue<Envelope>> queues = new Dictionary<int, Queue<Envelope>>();
        private Task listenTask;
        private bool closing;
        private Queue<string> statusQueue = new Queue<string>();

        public ServerAdmin()
        {
            Logger.SetOutputFilename(Path.Combine(Application.StartupPath, "Clover.ChatServer.log"));
            InitializeComponent();
        }

        private void ServerAdmin_Load(object sender, EventArgs e)
        {
            while (statusQueue.Count > 0)
            {
                if (txtConsole.Text != string.Empty)
                {
                    txtConsole.AppendText(Environment.NewLine);
                }
                txtConsole.AppendText(statusQueue.Dequeue());
                txtConsole.ScrollToCaret();
            }
        }
        private async void ServerAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            else if (listenTask != null && !listenTask.IsCompleted)
            {
                e.Cancel = true;
                if (!closing)
                {
                    ReportStatus("Server shutdown process initiated.");
                    listener.Close();
                    await listenTask;
                    Application.Exit();
                }
            }
        }
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public async void InitializeServer()
        {
            try
            {
                var settings = Settings.LoadFromFile(Path.Combine(Application.StartupPath, "settings.xml"));
                DbLayerSettings.SetConnectionString(settings.MySqlServerIP, settings.MySqlServerPort, settings.DatabaseName, settings.MySqlUserName, settings.MySqlUserPassword.DecodeBase64());
                var users = await Task.Run(() => User.GetUsersByAccessLevel(4));
                foreach (var user in users)
                {
                    queues.Add(user.UserID, new Queue<Envelope>());
                }
            }
            catch (Exception dbException)
            {
                Logger.AppendLog($"Exception occurred when loading users. Message: {dbException.Message}");
                ReportStatus($"Exception occurred when loading users. Message: {dbException.Message}");
            }
            try
            {
                listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Any, 5678));
                listener.Listen(int.MaxValue);
            }
            catch (Exception exception)
            {
                Logger.AppendLog($"Server exception. Message: {exception.Message}");
                ReportStatus($"Server exception. Message: {exception.Message}");
                return;
            }
            ReportStatus("Server succesfully deployed.");
            listenTask = ListenAsync();
        }
        private async Task ListenAsync()
        {
            try
            {
                while (true)
                {
                    var client = await EndPointManager.AcceptAsync(listener, HandleEnvelope);
                    AddClient(client);
                    CleanupClientAsync(client);
                }
            }
            catch (ObjectDisposedException)
            {
                ReportStatus("Server listening socket closed.");
            }
            catch (IOException exception)
            {
                Logger.AppendLog($"Listening socket IOException: {exception.Message}");
                ReportStatus($"Listening socket IOException: {exception.Message}");
            }
            await CleanupServerAsync();
        }
        private async Task CleanupServerAsync()
        {
            EndPointManager[] clients;
            lock (lockObject)
            {
                closing = true;
                clients = connectedClients.ToArray();
            }
            var envelope = new Envelope(null, null, EnvelopeType.ServerShuttingDown, null, false);
            foreach (var client in clients)
            {
                ForwardEnvelope(client, envelope);
                client.Shutdown();
            }
            ReportStatus("Waiting for all clients to acknowledge server shutdown...");
            await Task.WhenAny(Task.WhenAll(clients.Select(c => c.ReadTask)), Task.Delay(5000));
        }
        private async void CleanupClientAsync(EndPointManager client)
        {
            try
            {
                await client.ReadTask;
            }
            catch (IOException exception)
            {
                Logger.AppendLog($"Client {client.RemoteEndPoint} IOException: {exception.Message}");
                ReportStatus($"Client {client.RemoteEndPoint} IOException: {exception.Message}");
            }
            finally
            {
                RemoveClient(client);
                client.Dispose();
            }
        }

        private void HandleEnvelope(EndPointManager sender, Envelope receivedEnvelope)
        {
            lock (lockObject)
            {
                if (closing)
                {
                    return;
                }
                switch (receivedEnvelope.EnvelopeType)
                {
                    case EnvelopeType.Handshake:
                        {
                            sender.AssociatedUser = receivedEnvelope.Sender;
                            // Broadcast connected clients list
                            var envelope = new Envelope(null, null, EnvelopeType.ConnectedClientsList, string.Join(";", connectedClients.Select(c => c.AssociatedUser.UserName)), false);
                            foreach (var client in connectedClients)
                            {
                                ForwardEnvelope(client, envelope);
                            }
                            // Send queued messages
                            if (queues.ContainsKey(sender.AssociatedUser.UserID))
                            {
                                var queue = queues[sender.AssociatedUser.UserID];
                                while (queue.Count > 0)
                                {
                                    var queuedEnvelope = queue.Dequeue();
                                    queuedEnvelope.IsQueuedEnvelope = true;
                                    ForwardEnvelope(sender, queuedEnvelope);
                                }
                            }
                            break;
                        }
                    case EnvelopeType.BroadcastMessage:
                        {
                            foreach (var client in connectedClients.Where(c => c != sender))
                            {
                                ForwardEnvelope(client, receivedEnvelope);
                            }
                            foreach (var queue in queues.Where(q => connectedClients.All(c => c.AssociatedUser.UserID != q.Key)).Select(q => q.Value))
                            {
                                queue.Enqueue(receivedEnvelope);
                            }
                            break;
                        }
                    case EnvelopeType.Message:
                        {
                            if (connectedClients.Any(c => c.AssociatedUser.UserID == receivedEnvelope.Addressee.UserID))
                            {
                                var client = connectedClients.Single(c => c.AssociatedUser.UserID == receivedEnvelope.Addressee.UserID);
                                ForwardEnvelope(client, receivedEnvelope);
                            }
                            else if (queues.ContainsKey(receivedEnvelope.Addressee.UserID))
                            {
                                queues[receivedEnvelope.Addressee.UserID].Enqueue(receivedEnvelope);
                            }
                            break;
                        }
                }
            }
        }
        private void ForwardEnvelope(EndPointManager client, Envelope forwardedEnvelope)
        {
            try
            {
                client.SendEnvelope(forwardedEnvelope);
            }
            catch (IOException exception)
            {
                Logger.AppendLog($"Client {client.RemoteEndPoint} IOException: {exception.Message}");
                ReportStatus($"Client {client.RemoteEndPoint} IOException: {exception.Message}");
            }
        }
        private void AddClient(EndPointManager clientToAdd)
        {
            lock (lockObject)
            {
                connectedClients.Add(clientToAdd);
                ReportStatus($"{clientToAdd.RemoteEndPoint} connected. -- {connectedClients.Count} clients connected.");
            }
        }
        private void RemoveClient(EndPointManager clientToRemove)
        {
            lock (lockObject)
            {
                connectedClients.Remove(clientToRemove);
                ReportStatus($"{clientToRemove.RemoteEndPoint} disconnected. -- {connectedClients.Count} clients connected.");
                // Broadcast connected clients list
                if (closing)
                {
                    return;
                }
                var envelope = new Envelope(null, null, EnvelopeType.ConnectedClientsList, string.Join(";", connectedClients.Select(c => c.AssociatedUser.UserName)), false);
                foreach (var client in connectedClients)
                {
                    ForwardEnvelope(client, envelope);
                }
            }
        }
        private void ReportStatus(string statusText)
        {
            string status = $"{DateTime.Now:HH:mm:ss} : {statusText}";
            if (IsHandleCreated)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (txtConsole.Text != string.Empty)
                    {
                        txtConsole.AppendText(Environment.NewLine);
                    }
                    txtConsole.AppendText(status);
                    txtConsole.ScrollToCaret();
                });
            }
            else
            {
                statusQueue.Enqueue(status);
            }
        }
    }
}
