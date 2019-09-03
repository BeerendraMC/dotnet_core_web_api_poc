using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("employee")]
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet(), Route("getEmp/{id:int}")]
        public async Task<IActionResult> GetEmployee(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                var emp = await employeeRepository.GetEmployee(id);
                if (emp == null)
                {
                    return NotFound();
                }

                return Ok(emp);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet(), Route("getAllEmp")]
        public async Task<IActionResult> GetEmps()
        {
            try
            {
                var employees = await employeeRepository.GetAllEmployee();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost(), Route("addEmp")]
        public async Task<IActionResult> Post([FromBody]Employee employee)
        {
            try
            {
                var response = await employeeRepository.Add(employee);
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut(), Route("updateEmp")]
        public async Task<IActionResult> Put([FromBody]Employee employee)
        {
            try
            {
                var response = await employeeRepository.Update(employee);
                if(response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete(), Route("deleteEmp/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await employeeRepository.Delete(id);
                if(response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}