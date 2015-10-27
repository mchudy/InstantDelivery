using InstantDelivery.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstantDelivery.Core.Repositories
{
    public class EmployeesRepository : IDisposable
    {
        private InstantDeliveryContext context = new InstantDeliveryContext();

        public IList<Employee> GetAll()
        {
            return context.Employees.ToList();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
