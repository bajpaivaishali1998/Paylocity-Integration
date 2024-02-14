using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CEQ.Paylocity.HRM.API
{
    public class PaylocityEmployeeAdd
    {
        public readonly ILogger<PaylocityEmployeeAdd> _logger;

        public PaylocityEmployeeAdd(ILogger<PaylocityEmployeeAdd> logger)
        {
            _logger = logger;
        }

        [Function("PaylocityEmployeeAdd")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var clientId = Environment.GetEnvironmentVariable("clientId");
            var tenantId = Environment.GetEnvironmentVariable("tenantId");
            var clientSecret = Environment.GetEnvironmentVariable("clientSecret");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string resString = string.Empty;

            Payload deserializedPayload = JsonConvert.DeserializeObject<Payload>(requestBody);

            Employee objUserInfo = new Employee();
            objUserInfo.employeeFirstName = deserializedPayload.employeeFirstName;
            objUserInfo.employeeLastName = deserializedPayload.employeeLastName;
            objUserInfo.companyName = deserializedPayload.companyName;
            objUserInfo.companyId = deserializedPayload.companyId;
            objUserInfo.employeeAddressLine1 = deserializedPayload.employeeAddressLine1;
            objUserInfo.employeeAddressLine2 = deserializedPayload.employeeAddressLine2;
            objUserInfo.employeeGender = deserializedPayload.employeeGender;
            objUserInfo.employeeJobTitle = deserializedPayload.employeeJobTitle;
            objUserInfo.employeePosition = deserializedPayload.employeePosition;
            objUserInfo.employeeWorkEMailAddress= deserializedPayload.employeeWorkEMailAddress;
            objUserInfo.employeePayFrequency = deserializedPayload.employeePayFrequency;
            objUserInfo.employeeWorkPhone = deserializedPayload.employeeWorkPhone;
            objUserInfo.employeePayType = deserializedPayload.employeePayType;
            objUserInfo.employeeCity = deserializedPayload.employeeCity;
            objUserInfo.employeeCostCenter1 = deserializedPayload.employeeCostCenter1;
            objUserInfo.employeeCostCenter2 = deserializedPayload.employeeCostCenter2;
            objUserInfo.employeeCostCenter3 = deserializedPayload.employeeCostCenter3;
            objUserInfo.employeeHireDate = deserializedPayload.employeeHireDate;
            objUserInfo.employeeBadgeClockNumber = deserializedPayload.employeeBadgeClockNumber;
            objUserInfo.employeeEEOClass = deserializedPayload.employeeEEOClass;
            objUserInfo.employeeMaritalStatus = deserializedPayload.employeeMaritalStatus;
            objUserInfo.employeeMiddleInitial= deserializedPayload.employeeMiddleInitial;
            objUserInfo.employeeState= deserializedPayload.employeeState;
            objUserInfo.employeeSupervisor= deserializedPayload.employeeSupervisor;
            objUserInfo.employeeSupervisorId= deserializedPayload.employeeSupervisorId;
            objUserInfo.employeeTaxForm= deserializedPayload.employeeTaxForm;
            objUserInfo.employeeZip= deserializedPayload.employeeZip;


            return new OkObjectResult("Welcome to Azure Functions!");
        }

    }
}
