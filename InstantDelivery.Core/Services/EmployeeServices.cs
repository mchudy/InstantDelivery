using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstantDelivery.Core.Entities;
using InstantDelivery.Core;
namespace InstantDelivery.Core.Services
{
    public class EmployeeServices
    {
        private IQueryable<Employee> employees;
        public IQueryable<Employee> Employees
        {
            get
            {
                SelectAllEmployees();
                return employees;
            }
            set
            {
                employees = value;
            }
        }

        public List<Employee> EmployeesList
        {
            get
            {
                using (var context = new InstantDeliveryContext())
                {
                    // too wszystko jest roboczo bo nie wiem jak to do konca pisać
                    // btw do grida nie mozna podpiąć IQueryable bo jest XmlParseException
                    return context.Set<Employee>().ToList();
                }
            }
            set
            {
            }
        }
        

        private void SelectAllEmployees()
        {
            using (var context = new InstantDeliveryContext())
            {
                employees = context.Set<Employee>();
            }
        }
    }
}
