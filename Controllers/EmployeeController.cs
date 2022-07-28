using Microsoft.AspNetCore.Mvc;
using Sample_MachineTest.DatabaseConnection;
using Sample_MachineTest.DatabaseConnection.DatabaseClasses;
using Sample_MachineTest.Models;

namespace Sample_MachineTest.Controllers
{
    public class EmployeeController : Controller
    {

        SampleContext sampleContext;
        public EmployeeController(SampleContext context)
        {
            this.sampleContext = context;
        }

        [HttpGet]
        public IActionResult Employee(string name)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var modelData = new EmployeeViewModel()
            {
                LoginName = name,
                EmployeeData = sampleContext.Employee.ToList(),
            };
            return View(modelData);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employeeData = new Employee()
                {
                    FullName = employeeViewModel.FullName,
                    Age = employeeViewModel.Age,
                    DepartmentId = employeeViewModel.DepartmentId,
                };
                sampleContext.Employee.Add(employeeData);
                sampleContext.SaveChanges();
                return RedirectToAction("Employee", "Employee", new { @name = HttpContext.Session.GetString("name") });
            }
            ModelState.AddModelError("", "Something went wrong.");
            return View(new EmployeeViewModel() { LoginName = HttpContext.Session.GetString("name") });
        }
    }
}
