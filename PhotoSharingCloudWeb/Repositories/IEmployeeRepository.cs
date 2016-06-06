using System.Collections.Generic;
using PhotoSharingCloudWeb.Models;

namespace PhotoSharingCloudWeb.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int employeeId);
        void UploadImage(byte[] image, string mimeType, int empNo);
    }
}