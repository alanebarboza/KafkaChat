using Confluent.Kafka;
using KafkaChat.Domain.Entities;
using KafkaChat.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KafkaChat.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IProducerService _producer;

        public ProducerController(IProducerService producer) => _producer = producer;

        [HttpPost("SendMessage")]
        public async Task SendMessage(Message message) => await _producer.SendMessage(message);
    }
}
