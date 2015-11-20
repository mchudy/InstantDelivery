using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Domain.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InstantDelivery.Services
{
    /// <summary>
    /// Warstwa serwisu pracowników
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly InstantDeliveryContext context;
        /// <summary>
        /// Konstruktor warstwy serwisu
        /// </summary>
        /// <param name="context"></param>
        public EmployeeService(InstantDeliveryContext context)
        {
            this.context = context;
        }

        public int Count()
        {
            return context.Employees.Count();
        }

        /// <summary>
        /// Zmienia pojazd przypisany do pracownika
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="selectedVehicle"></param>
        public void ChangeEmployeesVehicle(Employee employee, Vehicle selectedVehicle)
        {
            var owner = context.Employees.FirstOrDefault(o => o.Id == employee.Id);
            var vehicle = selectedVehicle == null ? null :
                            context.Vehicles.FirstOrDefault(c => c.Id == selectedVehicle.Id);
            if (owner != null)
            {
                owner.Vehicle = vehicle;
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Zwraca wszystkich pracowników z bazy danych
        /// </summary>
        /// <returns></returns>
        public IQueryable<Employee> GetAll()
        {
            return context.Employees;
        }

        public IList<Employee> GetPage(int pageIndex, int pageSize)
        {
            return context.Employees
                .OrderBy(e => e.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public IList<Employee> GetPage(int pageIndex, int pageSize, string firstNameFilter, string lastNameFilter, string emailFilter, string sortProperty,
            ListSortDirection? sortDirection)
        {
            var result = context.Employees.AsQueryable();
            if (string.IsNullOrEmpty(sortProperty))
            {
                result = result.OrderBy(e => e.Id);
            }
            else if (sortDirection == ListSortDirection.Descending)
            {
                result = result.OrderByDescendingProperty(sortProperty);
            }
            else
            {
                result = result.OrderByProperty(sortProperty);
            }
            return result
                    .Where(e => firstNameFilter == "" || e.FirstName.StartsWith(firstNameFilter))
                    .Where(e => lastNameFilter == "" || e.LastName.StartsWith(lastNameFilter))
                    .Where(e => emailFilter == "" || e.Email.StartsWith(emailFilter))
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
        }

        /// <summary>
        /// Wczytuje dane pracownika z bazy danych, ignorując wprowadzone zmiany
        /// </summary>
        /// <param name="employee"></param>
        public void Reload(Employee employee)
        {
            context.Entry(employee).Reload();
        }

        /// <summary>
        /// Usuwa pracownika z bazy danych
        /// </summary>
        /// <param name="employee"></param>
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

        /// <summary>
        /// Dodaje pracownika do bazy danych
        /// </summary>
        /// <param name="employee"></param>
        public void AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        /// <summary>
        /// Zapisuje aktualne zmiany
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
