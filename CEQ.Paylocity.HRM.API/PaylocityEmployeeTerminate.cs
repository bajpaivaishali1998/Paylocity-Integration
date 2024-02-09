using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CEQ.Paylocity.HRM.API
{
    public class PaylocityEmployeeTerminate
    {
        public readonly ILogger<PaylocityEmployeeTerminate> _logger;

        public PaylocityEmployeeTerminate(ILogger<PaylocityEmployeeTerminate> logger)
        {
            _logger = logger;
        }


        [Function("PaylocityEmployeeTerminate")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var clientId = Environment.GetEnvironmentVariable("clientId");
            var tenantId = Environment.GetEnvironmentVariable("tenantId");
            var clientSecret = Environment.GetEnvironmentVariable("clientSecret");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string resString = string.Empty;

            EmployeePayload RequestInfo = JsonConvert.DeserializeObject<EmployeePayload>(requestBody);

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
