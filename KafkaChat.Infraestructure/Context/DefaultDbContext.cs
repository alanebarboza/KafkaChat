using KafkaChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KafkaChat.Infraestructure.Context
{
    public class DefaultDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }
    }
}
