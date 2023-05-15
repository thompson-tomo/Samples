using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Steeltoe.Messaging.Support;
using System.Threading;
using System.Threading.Tasks;

namespace MultiIo
{
    internal class PollerService : BackgroundService
    {
        private readonly SampleSinkAndSource _source;

        public PollerService(SampleSinkAndSource source)
        {
            _source = source;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                Thread.Sleep(3000);
                _source.HandleOutput1("Message1");
              //  _source.HandleOutput2("Message2");
            }
        }
    }
}
