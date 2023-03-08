using KafkaChat.Domain.Entities;

namespace KafkaChat.Domain.Interfaces.Services
{
    public interface IProducerService
    {
        Task SendMessage(Message message);
    }
}
