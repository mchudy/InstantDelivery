using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System;
using System.Linq;

namespace InstantDelivery.Services
{
    public class EmployeeService : IDisposable
    {
        private readonly InstantDeliveryContext context = new InstantDeliveryContext();

        public IQueryable<Employee> GetAll()
        {
            return context.Employees;
        }

        public int Total => context.Employees.Count();

        public void Reload(Employee employee)
        {
            context.Entry(employee).Reload();
        }

        public void RemoveEmployee(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        public IQueryable<Employee> Page(int pageNumber, int pageSize)
        {
            return context.Employees
                .OrderBy(e => e.Id)
                .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);
        }

        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
