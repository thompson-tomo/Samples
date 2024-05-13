using Microsoft.Extensions.Configuration;
using WeMicroIt.Dashboard.Service;

namespace WeMicroIt.Dashboard.Extensions
{
    public static class DashConfigurationExtensions
    {
        public static IConfigurationBuilder AddDashConfiguration(this IConfigurationBuilder builder)
        {
            return builder.Add(new DashConfigurationSource());
        }
    }
}
