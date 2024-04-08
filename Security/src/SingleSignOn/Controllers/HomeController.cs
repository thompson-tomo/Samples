using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Steeltoe.Samples.SingleSignOn.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Steeltoe.Samples.SingleSignOn.Controllers
{
    public class HomeController : Controller
    {
        private string? backendBaseAddress;
        private readonly HttpClient jwtHttpClient;
        private readonly HttpClient mutualTLSHttpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IHttpClientFactory clientFactory, ILogger<HomeController> logger)
        {
            jwtHttpClient = clientFactory.CreateClient("default");
            mutualTLSHttpClient = clientFactory.CreateClient("mTLS");
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            backendBaseAddress = GetSamplesUrl(HttpContext);
            base.OnActionExecuting(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        #region SSO

        [Authorize(Policy = "test.group")]
        public IActionResult TestGroup()
        {
            ViewData["Message"] = "You have the 'test.group' permission.";
            return View();
        }


        [Authorize(Policy = "test.group1")]
        public IActionResult AnotherTestGroup()
        {
            ViewData["Message"] = "You have the 'test.group1' permission.";

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Manage()
        {
            ViewData["Message"] = "Manage accounts using UAA or CF command line.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        #endregion

        public async Task<IActionResult> InvokeJwtSample()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token != null)
            {
                jwtHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return View("InvokeService", await SendRequestToBackend(jwtHttpClient, $"{backendBaseAddress}/api/JwtAuthorization"));
            }
            else
            {
                return View("InvokeService", "No access token found in user session. Perhaps you need to login or set Authentication:Schemes:OpenIdConnect:SaveTokens to 'true'?");
            }
        }

        #region Client Certificate (Mutual TLS)

        public async Task<IActionResult> InvokeSameOrgSample()
        {
            return View("InvokeService", await SendRequestToBackend(mutualTLSHttpClient, $"{backendBaseAddress}/api/certificate/SameOrg"));
        }

        public async Task<IActionResult> InvokeSameSpaceSample()
        {
            return View("InvokeService", await SendRequestToBackend(mutualTLSHttpClient, $"{backendBaseAddress}/api/certificate/SameSpace"));
        }

        #endregion

        public IActionResult AccessDenied()
        {
            ViewData["Message"] = "Insufficient permissions.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private const string FrontendHostname = "steeltoe-frontend";
        private const string BackendHostname = "steeltoe-backend";
        private static string GetSamplesUrl(HttpContext httpContext)
        {
            var hostName = httpContext.Request.Host.Host;
            var indexOfHost = hostName.IndexOf(FrontendHostname, StringComparison.Ordinal);
            string? serviceHostname;
            if (indexOfHost >= 0)
            {
                var prefix = hostName.Substring(indexOfHost + 17, 0);
                var suffix = hostName.Substring(indexOfHost + 17, hostName.Length - indexOfHost - 17);
                serviceHostname = prefix + BackendHostname + suffix;
            }
            else
            {
                serviceHostname = "localhost:7184";
            }

            return "https://" + serviceHostname;
        }

        private async Task<string> SendRequestToBackend(HttpClient client, string requestUri)
        {
            string result;
            try
            {
                result = await client.GetStringAsync(requestUri);
            }
            catch (Exception e)
            {
                result = $"Request failed: {e.Message}, at: {requestUri}";
            }
            return result;
        }
    }
}
