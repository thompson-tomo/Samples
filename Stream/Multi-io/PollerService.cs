using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Steeltoe.Messaging.Support;
using System.Threading;
using System.Threading.Tasks;

namespace MultiIo
{
    internal class PollerService : BackgroundService
    {
        private readonly SampleSource _source;

        public PollerService(SampleSource source)
        {
            _source = source;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                Thread.Sleep(3000);
                _source.SendOutput1();

                _source.SendOutput2();
            }
        }
    }
}
