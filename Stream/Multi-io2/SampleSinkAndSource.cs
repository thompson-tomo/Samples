using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Steeltoe.Messaging;
using Steeltoe.Messaging.Handler.Attributes;
using Steeltoe.Messaging.Support;
using Steeltoe.Stream.Attributes;
using Steeltoe.Stream.Messaging;

namespace MultiIo
{

    [EnableBinding(typeof(IMultiInputSinkAndSource))]
    public class SampleSinkAndSource
    {
        private ILogger<SampleSinkAndSource> _logger;

        ISink sink;
        public SampleSinkAndSource(ILogger<SampleSinkAndSource> logger)
        {
            _logger = logger;
        }

        [StreamListener(IMultiInputSinkAndSource.INPUT1)]
        public void HandleInput1(string message)
        {
            _logger?.LogInformation($"SampleSink Received message at Input1: "+ message);
        }

        [StreamListener(IMultiInputSinkAndSource.INPUT2)]
        public void HandleInput2(string message)
        {
            _logger?.LogInformation($"SampleSink Received message at Input2: " + message);
        }

        [SendTo(IMultiInputSinkAndSource.OUTPUT1)]
        public IMessage HandleOutput1(string input)
        {
            var output = "[1]: " + input;
            _logger?.LogInformation($"Sample Source output :{output}");
            return MessageBuilder.WithPayload(output).Build();
        }

        [SendTo(IMultiInputSinkAndSource.OUTPUT2)]
        public IMessage HandleOutput2(string input)
        {
            var output = "[2]: " + input;
            _logger?.LogInformation($"Sample Source output :{output}");
            return MessageBuilder.WithPayload(output).Build();
        }

    }
    public interface IMultiInputSinkAndSource
    {
        const string INPUT1 = "input1";
        const string INPUT2 = "input2";

        const string OUTPUT1 = "output1";
        const string OUTPUT2 = "output2";

        [Input(INPUT1)]
        ISubscribableChannel Input1();

        [Input(INPUT2)]
        ISubscribableChannel Input2();

        [Output(OUTPUT1)]
        IMessageChannel Output1 { get; }

        [Output(OUTPUT2)]
        IMessageChannel Output2 { get; }
    }

}