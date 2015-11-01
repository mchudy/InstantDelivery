using System;
using System.Collections.Generic;
using System.Linq;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Core.Repositories
{
    public class EmployeesRepository : IDisposable
    {
        private InstantDeliveryContext context = new InstantDeliveryContext();
        public int Total => context.Employees.Count();

        public IList<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public void Reload(Employee employee)
        {
            context.Entry(employee).Reload();
        }

        public void Remove(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        //TODO to powinno być chyba jakieś extension method, zeby mozna bylo podpiac do kazdego zapytania
        public IList<Employee> Page(int pageNumber, int pageSize)
        {
            return context.Employees.OrderBy(e => e.EmployeeId)
                                    .Skip(pageSize * (pageNumber - 1))
                                    .Take(pageSize).ToList();
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
