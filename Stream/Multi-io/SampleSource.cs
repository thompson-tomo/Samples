using Microsoft.Extensions.Logging;
using Steeltoe.Messaging;
using Steeltoe.Messaging.Handler.Attributes;
using Steeltoe.Messaging.Support;
using Steeltoe.Stream.Attributes;


namespace MultiIo
{
    [EnableBinding(typeof(IMultiOutputSource))]
    public class SampleSource
    {
        private ILogger<SampleSource> _logger;

        public SampleSource(ILogger<SampleSource> logger)
        {
            _logger = logger;
        }

        [SendTo(IMultiOutputSource.OUTPUT1)]
        public IMessage SendOutput1()
        {
            var output = "Message 1";
            _logger?.LogInformation($"Sample Source output :{output}");
            return  MessageBuilder.WithPayload(output).Build(); 
        }

        [SendTo(IMultiOutputSource.OUTPUT2)]
        public IMessage SendOutput2()
        {
            var output = "Message 2";
            _logger?.LogInformation($"Sample Source output :{output}");
            return MessageBuilder.WithPayload(output).Build();
        }

     
    }
    public interface IMultiOutputSource
    {
        const string OUTPUT1 = "output1";
        const string OUTPUT2 = "output2";

        [Output(OUTPUT1)]
        IMessageChannel Output1 { get; }

        [Output(OUTPUT2)]
        IMessageChannel Output2 { get; }

    }
}