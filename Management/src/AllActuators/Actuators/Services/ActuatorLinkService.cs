using Microsoft.Extensions.Logging;
using Steeltoe.Actuators.Models;
using Steeltoe.Management.Endpoint.Web.Hypermedia;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Steeltoe.Actuators.Services
{
    public class ActuatorLinkService : IActuatorLinkService
    {
        private readonly IActuatorEndpointHandler actuatorEndpointHandler;
        private readonly ILogger<ActuatorLinkService> logger;

        public ActuatorLinkService(IActuatorEndpointHandler actuatorEndpointHandler, ILogger<ActuatorLinkService> logger)
        {
            this.actuatorEndpointHandler = actuatorEndpointHandler;
            this.logger = logger;
        }

        public IEnumerable<HrefProperties> GetActuatorLinks()
        {
            logger.LogInformation("Retrieving actuators");

            var actuatorLinks = Enumerable.Empty<HrefProperties>();

            var actuatorEndpoints = actuatorEndpointHandler.InvokeAsync("/actuator", new CancellationTokenSource().Token).Result;

            if (actuatorEndpoints is not null)
            {
                logger.LogInformation($"Found {actuatorEndpoints.Entries.Count} actuators");

                actuatorLinks = actuatorEndpoints.Entries.Select(entry =>
                    new HrefProperties
                    {
                        Display = entry.Key != "self" ? entry.Key : "all actuators",
                        Address = entry.Value.Href
                    });
            }
            else
            {
                logger.LogError("No actuators were found");
            }

            return actuatorLinks;
        }
    }
}
