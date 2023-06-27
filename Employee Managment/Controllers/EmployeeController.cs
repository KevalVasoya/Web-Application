using CommonModel.ViewModel;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Employee_Managment.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>The employee services</summary>
        public readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        // GET: Employee
        public ActionResult Index()
        {
            IList<EmployeeDetailsViewModel> employeedatalist = _employeeServices.GetAllData();
            return View(employeedatalist);
        }

        /// <summary>Creates this instance.</summary>
        public ActionResult Create()
        {
            ViewBag.Designations = new SelectList(Designation(), "Id", "Designation");
            return View();
        }

        /// <summary>Adds the employee.</summary>
        /// <param name="employee">The employee.</param>
        [HttpPost]
        public ActionResult AddEmployee(EmployeeDetailsViewModel employee)
        {
            try
            {
                if (employee.ProfilePictures.FileName != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(employee.ProfilePictures.FileName) + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + Path.GetExtension(employee.ProfilePictures.FileName);
                    string path = Server.MapPath("~/Image/");
                    employee.ProfilePictures.SaveAs(path + filename);
                    employee.ProfilePicture = filename;
                }
                _employeeServices.AddEmployee(employee);
                TempData["Success"] = "Employee Created Successfully";
                string message = "Created the record successfully";
                ViewBag.Message = message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to add employee. Error: " + ex.Message;
                return View();
            }
        }

        /// <summary>Edits the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        public ActionResult Edit(int id)
        {
            EmployeeDetailsViewModel emp = _employeeServices.GetEmployeeDetails(id);
            ViewBag.Designations = new SelectList(Designation(), "Id", "Designation");
            return View(emp);
        }

        /// <summary>Edits the emp details.</summary>
        /// <param name="employee">The employee.</param>
        [HttpPost]
        public ActionResult EditEmpDetails(EmployeeDetailsViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employee.ProfilePicture != null)
                    {
                        string filename = Path.GetFileNameWithoutExtension(employee.ProfilePictures.FileName) + DateTime.Now.ToString("ddMMyyyyhhmmssfff") + Path.GetExtension(employee.ProfilePictures.FileName);
                        string path = Server.MapPath("~/Image/");
                        employee.ProfilePictures.SaveAs(path + filename);
                        employee.ProfilePicture = filename;
                    }
                    _employeeServices.UpdateEmployee(employee);
                }
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        /// <summary>Designations this instance.</summary>
        [HttpGet]
        public IList<EmployeeDesignationViewModel> Designation()
        {
            return _employeeServices.GetDesignationData();
        }

        public ActionResult Delete(int id)
        {
            try
            {
                _employeeServices.DeleteEmployee(id);
                TempData["Success"] = "Employee Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Failed to delete employee. Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
