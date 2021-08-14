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
        public ActionResult Add_Employee_data(string txtemployee, DateTime txtdob, string rdogender, string txtaddress, int ddlstate, string chkhobbye)
        {
            string[] splitstr = chkhobbye.Split(',');
            List<string> liststr = new List<string>();

            Employeedata tbl = new Employeedata();
            tbl.Employee_Name = txtemployee;
            tbl.dob = txtdob;
            tbl.gender = rdogender;
            tbl.address = txtaddress;
            tbl.state = ddlstate;
            db.Employeedatas.Add(tbl);
            db.SaveChanges();

            foreach (string hoby in splitstr)
            {
                tblHoby tblh = new tblHoby();
                tblh.hobyname = hoby;
                tblh.employeeid = tbl.Employee_ID;

                db.tblHobies.Add(tblh);
                db.SaveChanges();

            }
            List<EmployeeViewModel> listviewmodel = bindempdata();

            return PartialView("_Employeepart", listviewmodel);
        }

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
                vmodel.statename = item.statename;
                vmodel.address = item.address;
                vmodel.gender = item.gender;
                vmodel.hobieslist = db.tblHobies.Where(x => x.employeeid == item.Employee_ID).ToList();
                listviewmodel.Add(vmodel);

            }

            return listviewmodel;
        }


        public ActionResult GetALLStates(int emid)
        {
            var stateid = db.Employeedatas.Where(x => x.Employee_ID == emid).FirstOrDefault().state;
            var statelist = db.tblstates.Select(x => new
            {
                stateid = x.stateid,
                statename = x.statename

            }).ToList();


            return Json(new { statelist = statelist, statidss = stateid}, JsonRequestBehavior.AllowGet);

        
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