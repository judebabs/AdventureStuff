using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoSharingCloudWeb.Models;

namespace PhotoSharingCloudWeb.Repositories
{
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AdventureWorksContext _ctx;

        public EmployeeRepository()
        {
            _ctx = new AdventureWorksContext();
        }

        public List<Employee> GetEmployees()
        {
            return _ctx.Employees.ToList();
        }
        
        public Employee GetEmployeeById(int employeeId)
        {
            return _ctx.Employees.Find(employeeId);
        }

        public void UploadImage(byte[] image, string mimeType, int empNo)
        {
            var employee = _ctx.Employees.Find(empNo);

            if (employee != null)
            {
                employee.Photo = image;
                _ctx.SaveChanges();
            }
        }
    }
}