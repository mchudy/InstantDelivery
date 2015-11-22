using InstantDelivery.Domain;
using InstantDelivery.Domain.Entities;
using InstantDelivery.Domain.Extensions;
using InstantDelivery.Services.Paging;
using System;
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

        public PageDTO<Employee> GetPage(PageQuery<Employee> query)
        {
            var result = context.Employees.AsQueryable();
            if (string.IsNullOrEmpty(query.SortProperty))
            {
                result = result.OrderBy(e => e.Id);
            }
            else if (query.SortDirection == ListSortDirection.Descending)
            {
                result = result.OrderByDescendingProperty(query.SortProperty);
            }
            else
            {
                result = result.OrderByProperty(query.SortProperty);
            }
            foreach (var filter in query.Filters)
            {
                result = result.Where(filter);
            }
            var pageCount = (int)Math.Ceiling(result.Count() / (double)query.PageSize);
            return new PageDTO<Employee>
            {
                PageCount = pageCount,
                PageCollection = result.Page(query.PageIndex, query.PageSize)
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
