using System.Linq;
using InstantDelivery.Core.Entities;

namespace InstantDelivery.Services
{
    public interface IEmployeeService
    {
        int Total { get; }

        void AddEmployee(Employee employee);
        void Dispose();
        IQueryable<Employee> GetAll();
        IQueryable<Employee> Page(int pageNumber, int pageSize);
        void Reload(Employee employee);
        void RemoveEmployee(Employee employee);
        void Save();
    }
}