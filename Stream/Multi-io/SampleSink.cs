using Microsoft.Extensions.Logging;
using Steeltoe.Messaging;
using Steeltoe.Stream.Attributes;
using Steeltoe.Stream.Messaging;

namespace MultiIo
{

    [EnableBinding(typeof(MultiInputSink))]
    public class SampleSink
    {
        private ILogger<SampleSink> _logger;

        ISink sink;
        public SampleSink(ILogger<SampleSink> logger)
        {
            _logger = logger;
        }

        [StreamListener(MultiInputSink.INPUT1)]
        public void RxInput(string message)
        {
            _logger.LogInformation($"SampleSink Received message at Input1: "+ message);
        }

        [StreamListener(MultiInputSink.INPUT2)]
        public void RxInput2(string message)
        {
            _logger.LogInformation($"SampleSink Received message at Input2: " + message);
        }


    }
    public interface MultiInputSink
    {
        const string INPUT1 = "input1";
        const string INPUT2 = "input2";

        [Input(INPUT1)]
        ISubscribableChannel Input1();

        [Input(INPUT2)]
        ISubscribableChannel Input2();

    }

}