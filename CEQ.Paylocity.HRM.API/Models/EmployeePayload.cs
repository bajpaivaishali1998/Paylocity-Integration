using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CEQ.Paylocity.HRM.API.Models
{
    //internal class EmployeePayload
    //{

    //}
    //public class Payload
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Name { get; set; }
    //    public string Email { get; set; }
    //    public string Mobile { get; set; }
    //    public int EmployeeStatus { get; set; }
    //    public string EmployeeStatusName { get; set; }
    //    public string EmployeeCode { get; set; }
    //    public object ReportingManagerEmail { get; set; }
    //    public string Department { get; set; }
    //    public string Designation { get; set; }
    //    public object PresentAddress { get; set; }
    //    public object PermanentAddress { get; set; }
    //    public DateTime DateOfJoining { get; set; }
    //}

    //public class EmployeePayload
    //{
    //    public string Source { get; set; }
    //    public string Event { get; set; }
    //    public List<Payload> Payload { get; set; }
    //}
    public class Payload
    {

        public string companyId { get; set; }

        public string companyName { get; set; }

        public string employeeAddressLine1 { get; set; }

        public string employeeAddressLine2 { get; set; }

        public string employeeBadgeClockNumber { get; set; }

        public string employeeCity { get; set; }

        public string employeeCostCenter1 { get; set; }

        public string employeeCostCenter2 { get; set; }

        public string employeeCostCenter3 { get; set; }

        public string employeeEEOClass { get; set; }

        public string employeeFirstName { get; set; }

        public string employeeGender { get; set; }

        public DateTime employeeHireDate { get; set; }

        public int employeeId { get; set; }

        public string employeeJobTitle { get; set; }

        public string employeeLastName { get; set; }

        public string employeeMaritalStatus { get; set; }

        public string employeeMiddleInitial { get; set; }

        public string employeePayFrequency { get; set; }

        public string employeePayType { get; set; }

        public string employeePosition { get; set; }

        public string employeeState { get; set; }

        public string employeeSupervisor { get; set; }

        public string employeeSupervisorId { get; set; }

        public string employeeType { get; set; }

        public string employeeWorkEMailAddress { get; set; }

        public string employeeWorkPhone { get; set; }

        public string employeeZip { get; set; }

        public string employeeTaxForm { get; set; }

    }

}
