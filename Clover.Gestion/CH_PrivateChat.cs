using Clover.DbLayer;
using Clover.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class CH_PrivateChat : Form
    {
        private User partnerUser;
        private DateTime? lastMessageDate = null;

        public CH_PrivateChat(User partnerUser)
        {
            InitializeComponent();
            this.partnerUser = partnerUser;
            this.Tag = partnerUser;
            lblAddresseeName.Text = $"Conversación con {partnerUser.UserName}";
        }
        
        private void CH_PrivateChat_Load(object sender, EventArgs e)
        {
            // Carga conversación.
            foreach (var messageGroup in ((Main)this.Owner).MessageHistory[partnerUser.UserID]
                .GroupBy(m => m.Timestamp.Date)
                .OrderBy(g => g.Key))
            {
                rtbChatMessages.DeselectAll();
                rtbChatMessages.AppendText(Environment.NewLine);
                rtbChatMessages.SelectionAlignment = HorizontalAlignment.Center;
                rtbChatMessages.AppendText(messageGroup.Key.ToString("dddd, d 'de' MMMM"));
                foreach (var message in messageGroup
                    .OrderBy(m => m.Timestamp))
                {
                    rtbChatMessages.DeselectAll();
                    rtbChatMessages.AppendText(Environment.NewLine);
                    rtbChatMessages.SelectionAlignment = HorizontalAlignment.Left;
                    if (message.SenderID == AppEnvironment.CurrentUser.UserID)
                    {
                        rtbChatMessages.AppendText("Tu");
                    }
                    else
                    {
                        rtbChatMessages.AppendText(partnerUser.UserName);
                    }
                    rtbChatMessages.AppendText(" : ");
                    rtbChatMessages.AppendText(message.Body);
                }
                lastMessageDate = messageGroup.Key;
            }
            rtbChatMessages.ScrollToCaret();
        }

        private void CH_PrivateChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendMessage.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChatInput.Text))
            {
                return;
            }
            var envelope = new Envelope(AppEnvironment.CurrentUser, partnerUser, EnvelopeType.Message, txtChatInput.Text, false);
            txtChatInput.Text = string.Empty;
            // Envía mensaje.
            try
            {
                ((Main)this.Owner).ChatServerEndPoint.SendEnvelope(envelope);
            }
            catch (Exception exception)
            {
                // Waypoint CH201
                MessageBox.Show("Error al enviar mensaje."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH201. Message: " + exception.Message);
                return;
            } 
            // Agrega el mensaje al historial en memoria.
            ((Main)this.Owner).MessageHistory[partnerUser.UserID].Add(new ChatMessage()
            {
                SenderID = envelope.Sender.UserID,
                Timestamp = envelope.Timestamp,
                Body = envelope.Body
            });
            // Muestra el mensaje en el chat.
            PrintMessage(envelope);
            // Guarda el mensaje en la base de datos.
            StoreMessage(envelope);
        }

        public void HandleEnvelope(Envelope envelope)
        {
            // Agrega el mensaje al historial en memoria.
            ((Main)this.Owner).MessageHistory[partnerUser.UserID].Add(new ChatMessage()
            {
                SenderID = envelope.Sender.UserID,
                Timestamp = envelope.Timestamp,
                Body = envelope.Body
            });
            // Muestra el mensaje en el chat.
            PrintMessage(envelope);
            // Guarda el mensaje en la base de datos.
            StoreMessage(envelope);
        }
        private void PrintMessage(Envelope envelope)
        {
            if (lastMessageDate == null || lastMessageDate != DateTime.Today)
            {
                rtbChatMessages.DeselectAll();
                rtbChatMessages.AppendText(Environment.NewLine);
                rtbChatMessages.SelectionAlignment = HorizontalAlignment.Center;
                rtbChatMessages.AppendText(DateTime.Today.ToString("dddd, d 'de' MMMM"));
                lastMessageDate = DateTime.Today;
            }
            rtbChatMessages.DeselectAll();
            rtbChatMessages.AppendText(Environment.NewLine);
            rtbChatMessages.SelectionAlignment = HorizontalAlignment.Left;
            if (envelope.Sender.UserID == AppEnvironment.CurrentUser.UserID)
            {
                rtbChatMessages.AppendText($"Tu : {envelope.Body}");
            }
            else
            {
                rtbChatMessages.AppendText($"{envelope.Sender.UserName} : {envelope.Body}");
            }
            rtbChatMessages.ScrollToCaret();
        }
        private async void StoreMessage(Envelope envelope)
        {
            var message = new ChatMessage()
            {
                OwnerID = AppEnvironment.CurrentUser.UserID,
                SenderID = envelope.Sender.UserID,
                AddresseeID = envelope.Addressee.UserID,
                Timestamp = envelope.Timestamp,
                Body = envelope.Body
            };
            try
            {
                await Task.Run(() => message.Insert());
            }
            catch (Exception dbException)
            {
                // Waypoint CH202
                MessageBox.Show("Error al guardar mensaje en historial."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH202 (Flag: MySQL). Message: " + dbException.Message);
            }
        }

        private async void cmsItemClearHistory_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Por favor, confirme borrado del historial.", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog != DialogResult.Yes)
            {
                return;
            }
            try
            {
                await Task.Run(() => ChatMessage.DeleteMessagesBetweenTwoUsers(AppEnvironment.CurrentUser.UserID, partnerUser.UserID));
            }
            catch (Exception dbException)
            {
                // Waypoint CH203
                MessageBox.Show("Error en servidor MySQL."
                    + Environment.NewLine + Environment.NewLine + "Mensaje: " + dbException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.AppendLog("Exception at Waypoint CH203 (Flag: MySQL). Message: " + dbException.Message);
                return;
            }
            ((Main)this.Owner).MessageHistory[partnerUser.UserID].Clear();
            rtbChatMessages.Rtf = string.Empty;
            lastMessageDate = null;
            MessageBox.Show("Historial eliminado con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
