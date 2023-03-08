using KafkaChat.Domain.Entities;
using KafkaChat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KafkaChat.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _consumer;

        public ConsumerController(IConsumerService consumer) => _consumer = consumer;

        [HttpGet("GetMessages")]
        public async Task<IEnumerable<Message>> GetMessages([Required] string receptor) => await _consumer.GetMessages(receptor);

        [HttpGet("GetAllMessages")]
        public async Task<IEnumerable<Message>> GetAllMessages() => await _consumer.GetAllMessages();

        [HttpGet("ConsumeAllUnreadMessages")]
        public async Task<IEnumerable<Message>> ConsumeAllUnreadMessages() => await _consumer.ConsumeAllUnreadMessages();
    }
}
