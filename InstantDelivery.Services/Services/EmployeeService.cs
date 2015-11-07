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
            foreach (var package in employee.Packages
                .Where(p => p.Status == PackageStatus.InDelivery))
            {
                package.Status = PackageStatus.New;
            }
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
