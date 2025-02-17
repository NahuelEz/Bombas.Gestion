using Clover.DbLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Clover.Shared
{
    public class Envelope
    {
        public User Sender { get; set; }
        public User Addressee { get; set; }
        public DateTime Timestamp { get; set; }
        public EnvelopeType EnvelopeType { get; set; }
        public string Body { get; set; }
        public bool IsQueuedEnvelope { get; set; }

        public Envelope(User sender, User addressee, EnvelopeType envelopeType, string body, bool isQueuedEnvelope)
        {
            Sender = sender;
            Addressee = addressee;
            Timestamp = DateTime.Now;
            EnvelopeType = envelopeType;
            Body = body;
            IsQueuedEnvelope = isQueuedEnvelope;
        }
        public Envelope() { }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, new IsoDateTimeConverter());
        }
        public static Envelope FromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<Envelope>(jsonString, new IsoDateTimeConverter());
        }
    }

    public enum EnvelopeType
    {
        ServerShuttingDown,
        BroadcastMessage,
        ConnectedClientsList,
        Handshake,
        Message
    }
}