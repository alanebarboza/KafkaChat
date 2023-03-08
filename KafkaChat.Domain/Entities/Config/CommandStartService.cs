namespace KafkaChat.Domain.Entities.Config
{
    public class CommandStartService
    {
        public string Kafka { get; set; }
        public bool StartKafkaServer { get; set; }
        public string Zookeeper { get; set; }
        public bool StartZookeeperServer { get; set; }
        public bool ClearKafkaLogs { get; set; }
    }
}
