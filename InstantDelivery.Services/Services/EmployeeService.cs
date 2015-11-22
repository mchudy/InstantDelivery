using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Domain.Extensions;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

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
        //TODO: [IMPORTANT] Context factory injection, necessary for async support
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

        //TODO: consider introducing an abstraction over filters and sorting criteria
        public PageDTO<Employee> GetPage(int pageIndex, int pageSize, Expression<Func<Employee, bool>> filter,
            string sortProperty, ListSortDirection? sortDirection)
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
            result = result.Where(filter);
            return new PageDTO<Employee>
            {
                PageCount = (int)Math.Ceiling(result.Count() / (double)pageSize),
                PageCollection = result.Page(pageIndex, pageSize)
            };
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
