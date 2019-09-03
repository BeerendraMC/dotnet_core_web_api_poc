using Business.Services;
using DataAccess.Entities;
using DataAccess.Models;
using EmployeeManagement.Controllers;
using EmployeeManagement.Tests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementTests
{
    public class EmployeeControllerUnitTests
    {
        private EmployeeRepository employeeRepository;
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString = "server=HMECD001612\\SQLEXPRESS;database=EmployeeDB;Trusted_Connection=true";

        static EmployeeControllerUnitTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public EmployeeControllerUnitTests()
        {
            var context = new AppDbContext(dbContextOptions);
            MockDataDBInitializer db = new MockDataDBInitializer();
            db.Seed(context);

            employeeRepository = new EmployeeRepository(context);
        }

        [Fact]
        public async Task GetEmployeeById_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empId = 2;

            //Act  
            var data = await controller.GetEmployee(empId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task GetEmployeeById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var postId = 6;

            //Act  
            var data = await controller.GetEmployee(postId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        //[Fact]
        //public async Task GetEmployeeById_Return_BadRequestResult()
        //{
        //    //Arrange  
        //    var controller = new EmployeeController(employeeRepository);
        //    int? postId = null;

        //    //Act  
        //    var data = await controller.Get(postId);

        //    //Assert  
        //    Assert.IsType<BadRequestResult>(data);
        //}

        [Fact]
        public async Task GetEmployeeById_MatchResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var postId = 1;

            //Act  
            var data = await controller.GetEmployee(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var emp = okResult.Value.Should().BeAssignableTo<Employee>().Subject;

            Assert.Equal("Beerendra M C", emp.Name);
            Assert.Equal("beerendramc@gmail.com", emp.Email);
        }

        [Fact]
        public async Task GetAllEmployees_Return_OkResult()
        {
            //Arrange
            var controller = new EmployeeController(employeeRepository);

            //Act
            var data = await controller.GetEmps();

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        //[Fact]
        //public async Task GetAllEmployees_Return_NotFoundResult()
        //{
        //    //Arrange
        //    var controller = new EmployeeController(employeeRepository);

        //    //Act
        //    var data = await controller.GetEmps();
        //    data = null;

        //    //Assert
        //    Assert.IsType<NotFoundResult>(data);
        //}

        [Fact]
        public async Task GetAllEmployees_MatchResult()
        {
            //Arrange 
            var controller = new EmployeeController(employeeRepository);

            //Act
            var data = await controller.GetEmps();

            //Assert
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var employees = okResult.Value.Should().BeAssignableTo<List<Employee>>().Subject;

            Assert.Equal("Beerendra M C", employees[0].Name);
            Assert.Equal("beerendramc@gmail.com", employees[0].Email);

            Assert.Equal("Manju D R", employees[1].Name);
            Assert.Equal("manjudr@gmail.com", employees[1].Email);
        }

        [Fact]
        public async Task AddEmployee_Return_OkResult()
        {
            //Arrange
            var controller = new EmployeeController(employeeRepository);
            var emp = new Employee() { Name = "Suresh", Gender = "Male", Email = "suresh@gmail.com" };

            //Act
            var data = await controller.Post(emp);

            //Assert
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async Task AddEmployee_Return_BadRequest()
        {
            //Arrange
            var controller = new EmployeeController(employeeRepository);
            var emp = new Employee() { Name = "This is a big name which is greater than 20 characters", Gender = "Male", Email = "big@gmail.com" };

            //Act
            var data = await controller.Post(emp);

            //Assert
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task AddEmployee_MatchResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var emp = new Employee() { Name = "Thanu", Gender = "Female", Email = "thanu@gmail.com" };

            //Act  
            var data = await controller.Post(emp);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            // var result = okResult.Value.Should().BeAssignableTo<PostViewModel>().Subject;  

            Assert.Equal(emp, okResult.Value);
        }

        [Fact]
        public async Task UpdateEmployee_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empid = 2;

            //Act  
            var existingEmp = await controller.GetEmployee(empid);
            var okResult = existingEmp.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Employee>().Subject;

            result.Name = "Manjunath";

            var updatedData = await controller.Put(result);

            //Assert  
            Assert.IsType<OkObjectResult>(updatedData);
        }

        [Fact]
        public async void UpdateEmployee_Return_BadRequest()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empid = 2;

            //Act  
            var existingEmp = await controller.GetEmployee(empid);
            var okResult = existingEmp.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Employee>().Subject;

            result.Name = "Test Name More Than 20 Characteres";

            var data = await controller.Put(result);

            //Assert  
            Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async void UpdateEmployee_Return_NotFound()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empid = 2;

            //Act  
            var existingEmp = await controller.GetEmployee(empid);
            var okResult = existingEmp.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Employee>().Subject;

            result.Id = 6;

            var data = await controller.Put(result);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void DeleteEmployee_Return_OkResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empid = 2;

            //Act  
            var data = await controller.Delete(empid);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void DeleteEmployee_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new EmployeeController(employeeRepository);
            var empid = 5;

            //Act  
            var data = await controller.Delete(empid);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        //[Fact]
        //public async Task DeleteEmployee_Return_BadRequestResult()
        //{
        //    //Arrange  
        //    var controller = new EmployeeController(employeeRepository);
        //    int? empid = null;

        //    //Act  
        //    var data = await controller.Delete(empid);

        //    //Assert  
        //    Assert.IsType<BadRequestResult>(data);
        //}
    }
}
