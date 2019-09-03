using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Employee> Add(Employee employee)
        {
            if(context != null)
            {
                await context.Employees.AddAsync(employee);
                await context.SaveChangesAsync();
                return employee;
            }
            return null;
        }

        public async Task<Employee> Delete(int id)
        {
            if(context != null)
            {
                Employee employee = await context.Employees.FindAsync(id);
                if (employee != null)
                {
                    context.Employees.Remove(employee);
                    await context.SaveChangesAsync();
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            if(context != null)
            {
                return await context.Employees.ToListAsync();
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int? Id)
        {
            if(context != null)
            {
                return await context.Employees.FindAsync(Id);
            }
            return null;
        }

        public async Task<Employee> Update(Employee employeeChanges)
        {
            if(context != null)
            {
                var employee = await context.Employees.FindAsync(employeeChanges.Id);
                if (employee != null)
                {
                    employee.Name = employeeChanges.Name is null ? employee.Name : employeeChanges.Name;
                    employee.Gender = employeeChanges.Gender is null ? employee.Gender : employeeChanges.Gender;
                    employee.Email = employeeChanges.Email is null ? employee.Email : employeeChanges.Email;
                    await context.SaveChangesAsync();
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
    }
}
