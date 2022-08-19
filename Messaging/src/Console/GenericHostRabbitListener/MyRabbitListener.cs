﻿using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Steeltoe.Messaging.Handler.Attributes;
using Steeltoe.Messaging.RabbitMQ;
using Steeltoe.Messaging.RabbitMQ.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGenericHost
{
    public class MyRabbitListener
    {
        private ILogger<MyRabbitListener> logger;

        public MyRabbitListener(ILogger<MyRabbitListener> logger)
        {
            this.logger = logger;
        }

        [RabbitListener(Binding = Names.BINDING_NAME )]
        public void Listen(string input)
        {
            logger.LogInformation(input);
        }
    }
}
