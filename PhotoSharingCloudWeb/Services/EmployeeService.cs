using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotoSharingCloudWeb.Models;
using PhotoSharingCloudWeb.Repositories;

namespace PhotoSharingCloudWeb.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _employeeRepository.GetEmployeeById(employeeId);
        }

        public void UploadImage(byte[] image, string mimeType, int empNo)
        {
            _employeeRepository.UploadImage(image,mimeType, empNo);
        }
    }
}