using Confluent.Kafka;
using Confluent.Kafka.Admin;
using KafkaChat.Domain.Entities;
using KafkaChat.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace KafkaChat.Services.HostedServices
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IConsumer<string, string> _consumer;
        private readonly DefaultDbContext _context;
        private readonly DbSet<Message> _entity;
        private readonly string _bootstrapServer;
        private readonly string _topic;

        public ConsumerHostedService(IConfiguration configuration, DefaultDbContext context)
        {
            _configuration = configuration;
            _context = context;
            _entity = context.Set<Message>();
            _topic = configuration.GetValue<string>("Kafka:Topic");
            _bootstrapServer = configuration.GetValue<string>("Kafka:ConsumerConfig:BootstrapServers");

            var consumerConfig = configuration.GetSection("Kafka:ConsumerConfig").Get<ConsumerConfig>();
            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe(_topic);

            Task.Run(async () =>
            {
                await CheckTopicExists();
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        if (consumeResult.Message.Value != null)
                        {
                            _entity.Add(JsonConvert.DeserializeObject<Message>(consumeResult.Message.Value));
                            await _context.SaveChangesAsync();
                        }

                        _consumer.Commit(consumeResult);
                    }
                    catch { }
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            return Task.CompletedTask;
        }

        private async Task CheckTopicExists()
        {
            var config = new AdminClientConfig
            {
                BootstrapServers = _bootstrapServer
            };

            using (var adminClient = new AdminClientBuilder(config).Build())
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));
                if (!metadata.Topics.Any(t => t.Topic == _topic))
                    await CreateTopic(adminClient);
            }
        }

        private async Task CreateTopic(IAdminClient adminClient)
        {
            var topicSpecification = new TopicSpecification
            {
                Name = _topic,
                NumPartitions = 1,
                ReplicationFactor = 1
            };

            await adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification });
        }
    }
}
