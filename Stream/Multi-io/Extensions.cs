using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steeltoe.Common.Contexts;
using Steeltoe.Common.Expression.Internal.Spring.Standard;
using Steeltoe.Common.Expression.Internal.Spring.Support;
using Steeltoe.Common.Expression.Internal;
using Steeltoe.Messaging.RabbitMQ.Extensions;
using Steeltoe.Stream.Attributes;
using Steeltoe.Stream.Extensions;
using Steeltoe.Stream.Messaging;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Steeltoe.Extensions.Configuration.SpringBoot;
using Steeltoe.Stream.StreamHost;

namespace MultiIo
{
    internal static class Extensions
    {
        public static IServiceCollection AddEnableBindings(this IServiceCollection services, params Type[] types)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (types == null)
            {
                throw new ArgumentNullException(nameof(types));
            }

            foreach (var type in types)
            {
                var attr = type.GetCustomAttributes(true).SingleOrDefault(attr => attr.GetType() == typeof(EnableBindingAttribute));

                if (attr != null)
                {
                    var enableBindingAttribute = (EnableBindingAttribute)attr;
                    var filtered = enableBindingAttribute.Bindings.Where(b => b.Name != nameof(ISource) && b.Name != nameof(ISink) && b.Name != nameof(IProcessor)).ToArray(); // These are added by default
                    if (filtered.Length > 0)
                    {
                        services.AddStreamBindings(filtered);
                    }
                    services.AddStreamListeners(type);
                    services.TryAddSingleton(type);
                }
            }

            return services;
        }
        public static void AddStreamServicesForTypes(this IServiceCollection services, IConfiguration configuration, params Type[] types)
        {
            services.AddOptions();
            services.SafeAddRabbitMQConnection(configuration);
            services.AddRabbitConnectionFactory();
            services.ConfigureRabbitOptions(configuration);
            services.AddSingleton<IApplicationContext, GenericApplicationContext>();

            services.AddStreamServices(configuration);
            services.AddSourceStreamBinding();
            services.AddSinkStreamBinding();

            services.AddSingleton<IExpressionParser, SpelExpressionParser>();
            services.AddSingleton<IEvaluationContext, StandardEvaluationContext>();

            services.AddEnableBindings(types);
        }
        public static IHostBuilder AddStreamServices(this IHostBuilder builder, params Type[] types)
        {
            return builder.AddSpringBootConfiguration()
                .ConfigureServices((context, services) =>
                {
                    services.AddStreamServicesForTypes(context.Configuration, types);
                    services.AddHostedService<StreamLifeCycleService>();

                });
        }
    }
}
