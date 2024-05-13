using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace WeMicroIt.Dashboard.Service
{
    public class DashConfigurationSource : IConfigurationSource
    {

        public DashConfigurationSource()
        {
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new DashConfigurationProvider();
        }
    }
}
