using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly InstantDeliveryContext context;

        public EmployeeService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        public IQueryable<Employee> GetAll()
        {
            return context.Employees;
        }

        public void Reload(Employee employee)
        {
            context.Entry(employee).Reload();
        }

        public void RemoveEmployee(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
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
    }
}
