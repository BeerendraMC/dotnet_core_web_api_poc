using DataAccess.Entities;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Tests
{
    public class MockDataDBInitializer
    {
        public void Seed(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Employees.AddRange(
                new Employee() { Name = "Beerendra M C", Gender = "Male", Email = "beerendramc@gmail.com" },
                new Employee() { Name = "Manju D R", Gender = "Male", Email = "manjudr@gmail.com" },
                new Employee() { Name = "Prathik N G", Gender = "Male", Email = "prathikng@gmail.com" },
                new Employee() { Name = "Rakesh C M", Gender = "Male", Email = "rakeshcm@gmail.com" }
            );
            context.SaveChanges();
        }

    }
}
