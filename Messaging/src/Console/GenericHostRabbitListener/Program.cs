using System;
using System.Collections.Generic;
using ConsoleGenericHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using Steeltoe.Messaging.RabbitMQ.Host;

namespace ConsoleGenericHost
{
    class Program
    {
       
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            RabbitMQHost.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Add queue to be declared
                    services.AddRabbitQueues(new Queue(Names.QUEUE_NAME, durable: true, exclusive: false, autoDelete: true, new Dictionary<string, object>
                    {
                        { "x-message-ttl", 10000 }
                    }));
  
                    // Add topic Exchange

                    services.AddRabbitExchange(new TopicExchange(Names.EXCHANGE_NAME, true, true));
                    services.AddRabbitBindings(new QueueBinding(Names.BINDING_NAME, Names.QUEUE_NAME, Names.EXCHANGE_NAME, Names.ROUTING_KEY, null));

                    // Add the rabbit listener
                    services.AddSingleton<MyRabbitListener>();
                    services.AddRabbitListeners<MyRabbitListener>();

                    

                    services.AddSingleton<IHostedService, MyRabbitSender>();
                });
    }
    public static class Names
    {
        public const string BINDING_NAME = "myTopicExchange.binding";
        public const string QUEUE_NAME = "myqueue";
        public const string EXCHANGE_NAME = "myTopicExchange.topic";
        public const string ROUTING_KEY = "myRoutingKey";
    }
}
