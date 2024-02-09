using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEQ.Paylocity.HRM.API.Models
{
    //internal class EmployeePayload
    //{

    //}
    public class Payload
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int EmployeeStatus { get; set; }
        public string EmployeeStatusName { get; set; }
        public string EmployeeCode { get; set; }
        public object ReportingManagerEmail { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public object PresentAddress { get; set; }
        public object PermanentAddress { get; set; }
        public DateTime DateOfJoining { get; set; }
    }

    public class EmployeePayload
    {
        public string Source { get; set; }
        public string Event { get; set; }
        public List<Payload> Payload { get; set; }
    }

}
