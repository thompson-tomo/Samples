using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace WeMicroIt.Dashboard.Service
{
    public class DashConfigurationProvider : ConfigurationProvider
    {

        public DashConfigurationProvider()
        {
        }


        public override void Load()
        {
            try
            {
                var baseAddress = new Uri("http://127.0.0.1:90");
                //Eureka
                Data.Add("eureka:client:ShouldRegisterWithEureka", true.ToString());
                Data.Add("eureka:client:ShouldFetchRegistry", false.ToString());
                Data.Add("Eureka:Client:ServiceUrl", $"http://{baseAddress.Host}:{baseAddress.Port}/API/Eureka/v2");
                Data.Add("eureka:client:ValidateCertificates", false.ToString());
                Data.Add("eureka:instance:AppGroupName", "WeMictoIt");
                Data.Add("eureka:instance:AppName", "WeMicroIt-Portal");
                Data.Add("eureka:instance:InstanceId", $"Inst={baseAddress.Host}:{baseAddress.Port}");
                Data.Add("eureka:instance:InstanceEnabledOnInit", true.ToString());
                Data.Add("eureka:instance:HostName", Dns.GetHostName());
                Data.Add("eureka:instance:IpAddress", baseAddress.Host);
                Data.Add("eureka:instance:PreferIpAddress", true.ToString());
                if (baseAddress.Scheme == "http")
                {
                    Data.Add("eureka:instance:Port", baseAddress.Port.ToString());
                    Data.Add("eureka:instance:IssUnsecurePortEnabled", true.ToString());
                    Data.Add("eureka:instance:IsSecurePortEnabled", false.ToString());
                }
                else
                {
                    Data.Add("eureka:instance:SecurePort", baseAddress.Port.ToString());
                    Data.Add("eureka:instance:IsUnsecurePortEnabled", false.ToString());
                    Data.Add("eureka:instance:IsSecurePortEnabled", true.ToString());
                }
            }
            catch (Exception exc)
            {
                //assumed there is an issue connecting to db to get settings
            }

        }
    }
}
