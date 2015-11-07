using InstantDelivery.Core.Entities;
using System.Linq;

namespace InstantDelivery.Services
{
    public interface IEmployeeService
    {
        void AddEmployee(Employee employee);
        IQueryable<Employee> GetAll();
        void Reload(Employee employee);
        void RemoveEmployee(Employee employee);
        void Save();
    }
}