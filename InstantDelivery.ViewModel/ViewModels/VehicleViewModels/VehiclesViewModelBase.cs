//using System.ComponentModel;
//using System.Linq;
//using Caliburn.Micro;
//using InstantDelivery.Core.Entities;

//namespace InstantDelivery.ViewModel
//{
//    public abstract class VehiclesViewModelBase : Screen
//    {
//        private IQueryable<Vehicle> vehicles;

//        private int currentPage = 1;
//        private string brandFilter = string.Empty;
//        private string modelFilter = string.Empty;
//        private string registrationNumberFilter = string.Empty;
//        private VehicleSortingProperty? sortingProperty;

//        public IQueryable<Vehicle> Vehicles
//        {
//            get { return vehicles; }
//            set
//            {
//                vehicles = value;
//                NotifyOfPropertyChange();
//            }
//        }

//        public string BramdFilter
//        {
//            get { return brandFilter; }
//            set
//            {
//                brandFilter = value;
//                UpdateVehicles();
//            }
//        }

//        public string ModelFilter
//        {
//            get { return modelFilter; }
//            set
//            {
//                modelFilter = value;
//                UpdateVehicles();
//            }
//        }

//        protected abstract IQueryable<Employee> GetEmployees();

//        protected void UpdateEmployees()
//        {
//            var newEmployees = GetEmployees();
//            if (SortingProperty != null)
//            {
//                newEmployees = SortEmployees(newEmployees);
//            }
//            newEmployees = FilterEmployees(newEmployees);
//            Employees = newEmployees;
//        }

//        public int CurrentPage
//        {
//            get { return currentPage; }
//            set
//            {
//                currentPage = value;
//                NotifyOfPropertyChange();
//            }
//        }

//        public string EmailFilter
//        {
//            get { return emailFilter; }
//            set
//            {
//                emailFilter = value;
//                UpdateEmployees();
//            }
//        }

//        public EmployeeSortingProperty? SortingProperty
//        {
//            get { return sortingProperty; }
//            set
//            {
//                sortingProperty = value;
//                UpdateEmployees();
//            }
//        }

//        protected override void OnActivate()
//        {
//            base.OnActivate();
//            UpdateEmployees();
//        }

//        private IQueryable<Employee> SortEmployees(IQueryable<Employee> newEmployees)
//        {
//            if (SortingProperty == EmployeeSortingProperty.ByFirstName)
//            {
//                newEmployees = newEmployees.OrderBy(e => e.FirstName);
//                CurrentPage = 1;
//            }
//            else if (SortingProperty == EmployeeSortingProperty.ByLastName)
//            {
//                newEmployees = newEmployees.OrderBy(e => e.LastName);
//                CurrentPage = 1;
//            }
//            return newEmployees;
//        }

//        private IQueryable<Employee> FilterEmployees(IQueryable<Employee> newEmployees)
//        {
//            return newEmployees
//                .Where(e => FirstNameFilter == "" || e.FirstName.StartsWith(FirstNameFilter))
//                .Where(e => LastNameFilter == "" || e.LastName.StartsWith(LastNameFilter))
//                .Where(e => EmailFilter == "" || e.Email.StartsWith(EmailFilter));
//        }
//    }

//    public enum EmployeeSortingProperty
//    {
//        [Description("Po nazwisku")]
//        ByLastName,
//        [Description("Po imieniu")]
//        ByFirstName,
//    }
//}