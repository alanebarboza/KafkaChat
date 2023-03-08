using KafkaChat.Domain.Entities;
using KafkaChat.Domain.Interfaces.Services;
using KafkaChat.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace KafkaChat.Services.Services
{
    public class ConsumerService : IConsumerService
    {
        private readonly DefaultDbContext _context;
        private readonly DbSet<Message> _entity;

        public ConsumerService(DefaultDbContext context)
        {
            _context = context;
            _entity = context.Set<Message>();
        }
        public async Task<IEnumerable<Message>> GetMessages(string receptor) => await _entity.Where(x => x.Receptor == receptor).AsNoTracking().ToListAsync();
        public async Task<IEnumerable<Message>> GetAllMessages() => await _entity.OrderBy(x => x.InsertedDate).AsNoTracking().ToListAsync();
        public async Task<IEnumerable<Message>> ConsumeAllUnreadMessages()
        {
            var unreadMessages = await _entity.Where(x => !x.Read).AsNoTracking().ToListAsync();
            if (unreadMessages.Any())
                await ReadAllMessages(unreadMessages);

            return unreadMessages;
        }

        public async Task ReadAllMessages(IEnumerable<Message> messages)
        {
            var messageIds = messages.Select(m => m.Id).ToList();
            var unreadMessages = await _entity.Where(m => messageIds.Contains(m.Id)).ToListAsync();
            unreadMessages.ForEach(m =>
            {
                var message = _context.Entry(m);
                message.Property(x => x.Read).CurrentValue = true;
            });
            await _context.SaveChangesAsync();
        }
    }
}
