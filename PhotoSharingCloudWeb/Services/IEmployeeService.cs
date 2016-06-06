using System.Collections.Generic;
using PhotoSharingCloudWeb.Models;

namespace PhotoSharingCloudWeb.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
        void UploadImage(byte[] image, string mimeType, int empNo);
    }
}