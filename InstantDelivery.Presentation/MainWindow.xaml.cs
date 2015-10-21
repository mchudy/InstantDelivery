using InstantDelivery.Core;
using InstantDelivery.Core.Entities;
using System;
using System.Windows;

namespace InstantDelivery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var c = new InstantDeliveryContext())
            {
                c.Employees.Add(new Employee
                {
                    FirstName = "dafadf"
                });
                c.SaveChanges();
                foreach (var em in c.Employees)
                {
                    Console.WriteLine(em.FirstName);
                }
            }
        }
    }
}
