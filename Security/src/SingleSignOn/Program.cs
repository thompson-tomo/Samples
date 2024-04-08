using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Steeltoe.Common;
using Steeltoe.Common.Security;
using Steeltoe.Configuration.CloudFoundry.ServiceBinding;
using Steeltoe.Configuration.Kubernetes.ServiceBinding;
using Steeltoe.Management.Endpoint;
using Steeltoe.Security.Authentication.Certificate;
using Steeltoe.Security.Authentication.OpenIdConnect;

//Environment.SetEnvironmentVariable("VCAP_APPLICATION", @"{
//      ""cf_api"": ""https://api.sys.dhaka.cf-app.com"",
//      ""limits"": {
//        ""fds"": 16384
//      },
//      ""application_name"": ""Steeltoe-SingleSignOn"",
//      ""application_uris"": [
//        ""Steeltoe-SingleSignOn.apps.dhaka.cf-app.com""
//      ],
//      ""name"": ""Steeltoe-SingleSignOn"",
//      ""space_name"": ""dev"",
//      ""space_id"": ""97b0f669-23b7-4661-8290-9593838a053d"",
//      ""organization_id"": ""1145c478-d4e2-4c87-8e00-1510946c3b13"",
//      ""organization_name"": ""thess"",
//      ""uris"": [
//        ""Steeltoe-SingleSignOn.apps.dhaka.cf-app.com""
//      ],
//      ""users"": null,
//      ""application_id"": ""33882b85-074f-4866-bb2b-ca5962b94a82""
//    }");
if (!Platform.IsCloudFoundry)
{
    Environment.SetEnvironmentVariable("VCAP_SERVICES", @"{
    ""p-identity"": [
            {
              ""label"": ""p-identity"",
              ""provider"": null,
              ""plan"": ""uaa"",
              ""name"": ""mySSOService"",
              ""tags"": [],
              ""instance_guid"": ""f0fd571f-4aaf-4807-b34f-3777b053de2f"",
              ""instance_name"": ""mySSOService"",
              ""binding_guid"": ""c2d303a8-8d4f-48ce-916a-74c0305e30b2"",
              ""binding_name"": null,
              ""credentials"": {
                ""auth_domain"": ""http://localhost:8080"",
                ""grant_types"": [
                  ""authorization_code"",
                  ""client_credentials""
                ],
                ""client_secret"": ""singlesignon_secret"",
                ""client_id"": ""steeltoe-singlesignon""
              },
              ""syslog_drain_url"": null,
              ""volume_mounts"": []
            }
          ]
        }");
}


var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddCloudFoundryServiceBindings()
    .AddKubernetesServiceBindings()
    .AddContainerIdentityCertificate(new Guid("a8fef16f-94c0-49e3-aa0b-ced7c3da6229"), new Guid("122b942a-d7b9-4839-b26e-836654b9785f"))
;

builder.Services.AddClientCertificateAuthorization(builder.Configuration);

/* Temporary */
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<OpenIdConnectOptions>, PostConfigureOpenIdConnectOptions>());
/* Temporary */

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Home/AccessDenied");
    })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters.NameClaimType = "user_name";

        // TODO: prescribe remote sign-out options
        //options.RemoteSignOutPath = new PathString("logout.do");
    });

builder.Services.AddSession();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("test.group", policy => policy.RequireClaim("scope", "test.group"));
    options.AddPolicy("test.group1", policy => policy.RequireClaim("scope", "test.group1"));
});

builder.Services.AddControllersWithViews();

builder.AddAllActuators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
