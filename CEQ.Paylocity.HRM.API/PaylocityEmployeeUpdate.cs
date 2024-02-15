using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CEQ.Paylocity.HRM.API
{
    public class PaylocityEmployeeUpdate
    {
        public readonly ILogger<PaylocityEmployeeUpdate> _logger;

        public PaylocityEmployeeUpdate(ILogger<PaylocityEmployeeUpdate> logger)
        {
            _logger = logger;
        }

        [Function("PaylocityEmployeeUpdate")]
        public  async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
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
            objUserInfo.FirstName = deserializedPayload.employeeFirstName;
            objUserInfo.LastName = deserializedPayload.employeeLastName;
            objUserInfo.CompanyName = deserializedPayload.companyName;
            objUserInfo.CompanyId = deserializedPayload.companyId;
            objUserInfo.AddressLine1 = deserializedPayload.employeeAddressLine1;
            objUserInfo.AddressLine2 = deserializedPayload.employeeAddressLine2;
            objUserInfo.Gender = deserializedPayload.employeeGender;
            objUserInfo.JobTitle = deserializedPayload.employeeJobTitle;
            objUserInfo.Position = deserializedPayload.employeePosition;
            objUserInfo.WorkEMailAddress = deserializedPayload.employeeWorkEMailAddress;
            objUserInfo.PayFrequency = deserializedPayload.employeePayFrequency;
            objUserInfo.WorkPhone = deserializedPayload.employeeWorkPhone;
            objUserInfo.PayType = deserializedPayload.employeePayType;
            objUserInfo.City = deserializedPayload.employeeCity;
            objUserInfo.CostCenter1 = deserializedPayload.employeeCostCenter1;
            objUserInfo.CostCenter2 = deserializedPayload.employeeCostCenter2;
            objUserInfo.CostCenter3 = deserializedPayload.employeeCostCenter3;
            objUserInfo.HireDate = deserializedPayload.employeeHireDate;
            objUserInfo.BadgeClockNumber = deserializedPayload.employeeBadgeClockNumber;
            objUserInfo.EEOClass = deserializedPayload.employeeEEOClass;
            objUserInfo.MaritalStatus = deserializedPayload.employeeMaritalStatus;
            objUserInfo.MiddleInitial = deserializedPayload.employeeMiddleInitial;
            objUserInfo.State = deserializedPayload.employeeState;
            objUserInfo.Supervisor = deserializedPayload.employeeSupervisor;
            objUserInfo.SupervisorId = deserializedPayload.employeeSupervisorId;
            objUserInfo.TaxForm = deserializedPayload.employeeTaxForm;
            objUserInfo.Zip = deserializedPayload.employeeZip;





            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
