using System.Text.Json.Serialization;

namespace KafkaChat.Domain.Entities
{
    public class Message
    {
        [JsonIgnore]
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Sender { get; set; }
        public string Receptor { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        public bool Read { get; private set; } = false;
        public DateTime InsertedDate { get; private set; } = DateTime.Now;
    }
}
