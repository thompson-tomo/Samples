﻿using Microsoft.Extensions.Hosting;
using Steeltoe.Messaging.RabbitMQ.Config;
using Steeltoe.Messaging.RabbitMQ.Core;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGenericHost
{
    public class MyRabbitSender : IHostedService
    {
        private RabbitTemplate template;
        private IExchange topic; 
        private Timer timer;

        public MyRabbitSender(IServiceProvider services, IExchange topicExchange)
        {
            template = services.GetRabbitTemplate();
            topic = topicExchange;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(Sender, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            return Task.CompletedTask;
        }

        private void Sender(object state)
        {
            template.ConvertAndSend(topic.ExchangeName, Names.ROUTING_KEY, "foo");
        }
    }
}
