using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployee(int? Id);
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employeeChanges);
        Task<Employee> Delete(int id);
    }
}
