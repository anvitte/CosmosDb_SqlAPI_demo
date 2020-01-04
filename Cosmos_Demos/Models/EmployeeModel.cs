using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos_Demos.Models
{
    public class EmployeeModel
    {
        [JsonProperty(PropertyName ="id")]
        public string Id { get; set; }       
        public string EmployeeId { get; set; }
        public string  EmployeeName { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public string Band { get; set; }
        public double Salary { get; set; }
        public string Location { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string  City { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string CountryCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
   