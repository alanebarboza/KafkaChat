using Confluent.Kafka;
using KafkaChat.Domain.Entities;
using KafkaChat.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace KafkaChat.Services.Services
{
    public class ProducerService : IProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ProducerConfig _producerConfig;
        private readonly string _topic;
        public ProducerService(IConfiguration configuration)
        {
            _configuration = configuration;
            _producerConfig = configuration.GetSection("Kafka:ProducerConfig").Get<ProducerConfig>();
            _topic = configuration.GetValue<string>("Kafka:Topic");
        }

        public async Task SendMessage(Message message)
        {
            if (string.IsNullOrEmpty(message.Sender) || string.IsNullOrEmpty(message.Value))
                throw new ArgumentNullException("É obrigatório informar o Sender e o Value");

            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                var kafkaMessage = new Message<string, string>
                {
                    Key = message.Id.ToString(),
                    Value = JsonConvert.SerializeObject(message),
                };

                await producer.ProduceAsync(_topic, kafkaMessage);
            }
        }
    }
}