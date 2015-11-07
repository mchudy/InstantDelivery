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

        public void ChangeEmployeesVehicle(Employee employee, Vehicle selectedVehicle)
        {
            employee.Vehicle = selectedVehicle;
            // context.SaveChanges(); ? chcę sejwnąć dopiero jak sie kliknie confirm
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
