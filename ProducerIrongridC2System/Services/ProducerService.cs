using Confluent.Kafka;
using Confluent.Kafka.Admin;
using ProducerIrongridC2System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProducerIrongridC2System.Services
{
    public class ProducerService
    {
        private readonly string _bootstrapServers;
        private readonly IProducer<Null, string> _producer;
        public ProducerService(string bootstrapServers)
        {
            _bootstrapServers = bootstrapServers;
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendToKafka(string topicName, AssetReport jsonMessage)
        {
            string valu = JsonSerializer.Serialize<AssetReport>(jsonMessage);
            var message = new Message<Null, string> { Value = valu };
            await _producer.ProduceAsync(topicName, message);
        }

        public async Task EnshurTopicExists(string topicName)
        {
            var adminConfig = new AdminClientConfig
            {
                BootstrapServers = _bootstrapServers
            };
            var client = new AdminClientBuilder(adminConfig).Build();
            try
            {
                await client.CreateTopicsAsync(new[]
                {
                    new TopicSpecification
                    {
                        Name = topicName
                    }
                });
                Console.WriteLine("topic created");
            }
            catch (CreateTopicsException e)
            {
                if (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
                {
                    Console.WriteLine("topic already exists");
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
