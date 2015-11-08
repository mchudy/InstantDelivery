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
                var owner = (from o in context.Employees
                             select o).FirstOrDefault(o => o.Id == employee.Id);
                var child = (from o in context.Vehicles
                             select o).FirstOrDefault(c => c.Id == selectedVehicle.Id);

            if (owner != null) owner.Vehicle = child;
            context.SaveChanges();
        }


        public IQueryable<Employee> GetAll()
        {
            return context.Employees;
        }

        public void Reload(Employee employee)
        {
            // jest bug, że jak sie zaznaczy i anuluje to wywala jakiś syf, help me POMOCY W BAZIE
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
