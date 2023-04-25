using Microsoft.AspNetCore.Mvc;
using MvcCrud.Data;
using MvcCrud.Models;
using MvcCrud.Models.Domain;
using Microsoft.EntityFrameworkCore;
namespace MvcCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MydemoContext mydemo;

        public EmployeeController(MydemoContext Mydemo)
        {
            this.mydemo = Mydemo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employess = await mydemo.employe.ToListAsync();
            return View(employess); 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewmodel addEmployeeRequest)
        {
           var emp = new Employee
            {
                Id = Guid.NewGuid(),
                FName = addEmployeeRequest.FName,
                lName = addEmployeeRequest.lName,
                Email = addEmployeeRequest.Email,
                Phone = addEmployeeRequest.Phone,
                DOB = addEmployeeRequest.DOB
            };
            await mydemo.employe.AddAsync(emp);
            await mydemo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await mydemo.employe.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var editEmployeeRequest = new EditEmployeeViewmodel()
            {
                Id = employee.Id,
                FName = employee.FName,
                lName = employee.lName,
                Email = employee.Email,
                Phone = employee.Phone,
                DOB = employee.DOB
            };
            return await Task.Run(()=>View("Edit",editEmployeeRequest));
 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeViewmodel editEmpModel ,Guid id)
        {
            if(id!=editEmpModel.Id)
            {
                return BadRequest();
            }
            var employee= await mydemo.employe.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Id=editEmpModel.Id;
            employee.lName=editEmpModel.lName;
            employee.Email=editEmpModel.Email;
            employee.Phone=editEmpModel.Phone;
            employee.FName=editEmpModel.FName;
            employee.DOB = editEmpModel.DOB;
            mydemo.Entry(employee).State= EntityState.Modified;
            await mydemo.SaveChangesAsync();
            return RedirectToAction("Index");



        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await mydemo.employe.FindAsync(id);
            if (employee == null)
            {
                return NotFound();

            }
            mydemo.employe.Remove(employee);
            await mydemo.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    
    }

}
