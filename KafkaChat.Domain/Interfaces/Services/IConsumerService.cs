using KafkaChat.Domain.Entities;

namespace KafkaChat.Domain.Interfaces.Services
{
    public interface IConsumerService
    {
        Task<IEnumerable<Message>> GetMessages(string receptor);
        Task<IEnumerable<Message>> GetAllMessages();
        Task<IEnumerable<Message>> ConsumeAllUnreadMessages();
        Task ReadAllMessages(IEnumerable<Message> messages);

    }
}
