using EmployeeRegistrationpageUsingMVC.Models;
using EmployeeRegistrationpageUsingMVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegistrationpageUsingMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ddlstate = new SelectList(db.tblstates, "stateid", "statename");
           
            List<EmployeeViewModel> listviewmodel = bindempdata();
            return View(listviewmodel);
        }
        dbEmployeeEntities db = new dbEmployeeEntities();

        //insert data Employee
        public  ActionResult Add_Employee_data(string txtemployee, DateTime txtdob, string rdogender, string txtaddress, int ddlstate, string chkhobbye)
        {
            string[] splitstr = chkhobbye.Split(',');
            List<string> liststr = new List<string>();
            //output parameter
            System.Data.Entity.Core.Objects.ObjectParameter idno = new System.Data.Entity.Core.Objects.ObjectParameter("idno", typeof(string));
            //procedure

            if (ddlstate > 0)
            {
                var Employee_ID = db.insertaempdataproc(txtemployee, txtdob, rdogender, txtaddress, ddlstate, idno).FirstOrDefault().idno;
                //Employeedata tbl = new Employeedata();
                //tbl.Employee_Name = txtemployee;
                //tbl.dob = txtdob;
                //tbl.gender = rdogender;
                //tbl.address = txtaddress;
                //tbl.state = ddlstate;
                //db.Employeedatas.Add(tbl);
                //db.SaveChanges();

                //based on empoyee id insert hobyies 
                foreach (string hoby in splitstr)
                {
                    tblHoby tblh = new tblHoby();
                    tblh.hobyname = hoby;
                    tblh.employeeid = Employee_ID;

                    db.tblHobies.Add(tblh);
                    db.SaveChanges();

                }
            }
            List<EmployeeViewModel> listviewmodel = bindempdata();

            return PartialView("_Employeepart", listviewmodel);
        }

        //Update data Employee
        public ActionResult Update_Employee_data(int empid,  string txtemployee, DateTime txtdob, string rdogender, string txtaddress, int ddlstate, string chkhobbye)
        {
            if (ddlstate > 0)
            {
                var empdata = db.Employeedatas.Where(x => x.Employee_ID == empid).FirstOrDefault();
                if (empdata != null)
                {
                    empdata.Employee_Name = txtemployee;
                    empdata.dob = txtdob;
                    empdata.gender = rdogender;
                    empdata.address = txtaddress;
                    empdata.state = ddlstate;

                    db.SaveChanges();
                }
                var tblhdel = db.tblHobies.Where(x => x.employeeid == empdata.Employee_ID).ToList();
                db.tblHobies.RemoveRange(tblhdel);
                db.SaveChanges();
                string[] splitstr = chkhobbye.Split(',');
                List<string> liststr = new List<string>();
                foreach (string hoby in splitstr)
                {

                    tblHoby tblh = new tblHoby();
                    tblh.hobyname = hoby;
                    tblh.employeeid = empdata.Employee_ID;

                    db.tblHobies.Add(tblh);
                    db.SaveChanges();

                }
            }
            List<EmployeeViewModel> listviewmodel = bindempdata();

            return PartialView("_Employeepart", listviewmodel);
        }



        //Fetch All  data for Employee
        public ActionResult ShowALLData()
        {
            List<EmployeeViewModel> listviewmodel = bindempdata();

            return PartialView("_Employeepart", listviewmodel);
        }

        private List<EmployeeViewModel> bindempdata()
        {
            List<EmployeeViewmodellist> listmo = new List<EmployeeViewmodellist>();
            List<EmployeeViewModel> listviewmodel = new List<EmployeeViewModel>();
            var result = db.showalldata();
            foreach (var item in result)
            {
                EmployeeViewModel vmodel = new EmployeeViewModel();
                vmodel.Employee_ID = item.Employee_ID;
                vmodel.Employee_Name = item.Employee_Name;
                vmodel.gender = item.gender;
                vmodel.state = item.state;
                vmodel.dob = item.dob;
                vmodel.statename = item.statename;
                vmodel.address = item.address;
                vmodel.gender = item.gender;
                vmodel.hobieslist = db.tblHobies.Where(x => x.employeeid == item.Employee_ID).ToList();
                listviewmodel.Add(vmodel);

            }

            return listviewmodel;
        }

        //Get all state
        public ActionResult GetALLStates(int emid)
        {
            var employeedata = db.Employeedatas.Where(x => x.Employee_ID == emid).FirstOrDefault();
            if (employeedata != null)
            {
                var statelist = db.tblstates.Select(x => new
                {
                    stateid = x.stateid,
                    statename = x.statename

                }).ToList();


                return Json(new { statelist = statelist, employeedata = employeedata, status = "Success" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { statelist = "", employeedata = "", status="Failed" }, JsonRequestBehavior.AllowGet);
            }
        }


        //delete employee data
        public ActionResult Delete_Employee_data(int empid)
        {
            var employeedata = db.Employeedatas.Where(x => x.Employee_ID == empid).FirstOrDefault();
            if (employeedata != null)
            {
                db.Employeedatas.Remove(employeedata);

                var tblhdel = db.tblHobies.Where(x => x.employeeid == empid).ToList();
                db.tblHobies.RemoveRange(tblhdel);
                db.SaveChanges();


            }
           
            List<EmployeeViewModel> listviewmodel = bindempdata();

            return PartialView("_Employeepart", listviewmodel);
        }
        
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();                           
        }
    }
}