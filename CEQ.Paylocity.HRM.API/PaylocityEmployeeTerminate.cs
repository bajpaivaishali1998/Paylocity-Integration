using Azure.Identity;
using CEQ.Paylocity.HRM.API.LoggerInfo;
using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;

//using Microsoft.Graph;
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
            LogInfo errloginfo;
            string resString = string.Empty;

            string cnn = Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process);
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            CEQ.Paylocity.HRM.API.Models.Payload deserializedPayload = JsonConvert.DeserializeObject<CEQ.Paylocity.HRM.API.Models.Payload>(requestBody);

            //-----------Step 1-----CASE TO INVOKING THE AZURE FUNCTION---------------------------------//
            errloginfo = new LogInfo();
            errloginfo.PartitionKey = "Step : 1 HRStopEmployee v5.0";
            errloginfo.RowKey = Guid.NewGuid().ToString();
            errloginfo.Method_Name = "step1 invoke function";
            errloginfo.ReqInput = requestBody;
            ErrorLoggerInfo.InsertLogEntity("ExceptionsLog", errloginfo, cnn);

            var clientId = Environment.GetEnvironmentVariable("clientId");
            var tenantId = Environment.GetEnvironmentVariable("tenantId");
            var clientSecret = Environment.GetEnvironmentVariable("clientSecret");
            string[] scopes = { "https://graph.microsoft.com/.default" };
            ClientSecretCredential authProvider = new ClientSecretCredential(tenantId, clientId, clientSecret);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider, scopes);            

            Employee objUserInfo = new Employee();

            objUserInfo.WorkEMailAddress = deserializedPayload.employeeWorkEMailAddress;

            var email = objUserInfo.WorkEMailAddress.Split('@').LastOrDefault();
            if (email.ToString().ToLower() != "cloudeq.com".ToLower())
            {

                return new BadRequestObjectResult("Invalid Email Id");
            }
            _logger.LogInformation("Starting User User Termination..............");

            var userData = new User
            {
                EmployeeId = " "
            };

            try
            {
                var result = await graphClient.Users[objUserInfo.WorkEMailAddress].PatchAsync(userData);

                errloginfo = new LogInfo();
                errloginfo.PartitionKey = "Step : 4 Case : 2 User Deactivate Success";
                errloginfo.RowKey = Guid.NewGuid().ToString();
                errloginfo.Method_Name = "User Deactivated & EmployeeID Deleted Successfully.";
                errloginfo.ResOutput = "Deactivate Success : " + objUserInfo.WorkEMailAddress;
                errloginfo.LogMessage = "After successfull deactive!: " + result;
                ErrorLoggerInfo.InsertLogEntity("ExceptionsLog", errloginfo, cnn);
            }
            catch (Exception)
            {
                errloginfo = new LogInfo();
                errloginfo.PartitionKey = "Step : 4 Case : 2 User Deactivate Success";
                errloginfo.RowKey = Guid.NewGuid().ToString();
                errloginfo.Method_Name = "User Deactivated & EmployeeID Deleted Successfully.";
                errloginfo.ResOutput = "Deactivate Success : " + objUserInfo.WorkEMailAddress;
                errloginfo.LogMessage = "After successfull deactive!: ";
                ErrorLoggerInfo.InsertLogEntity("ExceptionsLog", errloginfo, cnn);
                throw;
            }


            Console.WriteLine("");

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
