using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using PhotoSharingCloudWeb.Models;
using PhotoSharingCloudWeb.Services;
using PhotoSharingCloudWeb.ViewModels;

namespace PhotoSharingCloudWeb.Controllers
{
    public class EmployeeController : ApiController
    {
        private AdventureWorksContext db = new AdventureWorksContext();

        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        // GET: api/EmployeesTest
        public List<EmployeeViewModel> GetEmployees()
        {
            var employees = new List<EmployeeViewModel>();

            var config = new MapperConfiguration(x => x.CreateMap<Employee, EmployeeViewModel>());

            var mapper = config.CreateMapper();

            _employeeService.GetEmployees().ForEach(
                    x => employees.Add(
                           mapper.Map<EmployeeViewModel>(x)
                        ));
            return employees;
        }

        // GET: api/EmployeesTest/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/EmployeesTest/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.BusinessEntityID)
            {
                return BadRequest();
            }

            db.Entry(employee).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeesTest
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Employees.Add(employee);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.BusinessEntityID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.BusinessEntityID }, employee);
        }

        // DELETE: api/EmployeesTest/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> UploadImage(int id, HttpPostedFile image)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var picArray = new byte[image.ContentLength];

            await image.InputStream.ReadAsync(picArray, 0, image.ContentLength);

            employee.Photo = picArray;

            db.Entry(employee).Property(x => x.Photo).IsModified = true;

            await db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.BusinessEntityID == id) > 0;
        }
    }
}