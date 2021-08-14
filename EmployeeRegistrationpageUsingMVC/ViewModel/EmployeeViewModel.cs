using EmployeeRegistrationpageUsingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeRegistrationpageUsingMVC.ViewModel
{
    public class EmployeeViewModel
    {
        public int Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public Nullable<int> state { get; set; }
        public string statename { get; set; }
        public IEnumerable<tblHoby> hobieslist { get; set; }

    }
    public class EmployeeViewmodellist
    {
        IEnumerable<EmployeeViewModel> employeelist { get; set; }
    }
}