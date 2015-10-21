using InstantDelivery.Core.Entities;
using System.Data.Entity;

namespace InstantDelivery.Core
{
    public class InstantDeliveryInitializer : DropCreateDatabaseAlways<InstantDeliveryContext>
    {
        protected override void Seed(InstantDeliveryContext context)
        {

            context.Employees.Add(new Employee
            {
                FirstName = "Johnny",
                LastName = "Rambo",
                Sex = Sex.Man
            });
            context.SaveChanges();
        }
    }
}
