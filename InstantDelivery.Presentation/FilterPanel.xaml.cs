using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InstantDelivery
{
    /// <summary>
    /// Interaction logic for EmployeesFilterPanel.xaml
    /// </summary>
    public partial class FilterPanel : UserControl
    {
        public static readonly DependencyProperty xName_Edit;
        public static readonly DependencyProperty xName_Remove;
        public static readonly DependencyProperty xName_Prev;
        public static readonly DependencyProperty xName_Next;

        public static readonly DependencyProperty Edit_Delegate;
        public static readonly DependencyProperty Remove_Delegate;
        public static readonly DependencyProperty PrevPage_Delegate;
        public static readonly DependencyProperty NextPage_Delegate;

        public string xNameEdit
        {
            get { return (string)GetValue(xName_Edit); }
            set { SetValue(xName_Edit, value); }
        }

        public string xNameRemove
        {
            get { return (string)GetValue(xName_Remove); }
            set { SetValue(xName_Remove, value); }
        }

        public string xNamePrev
        {
            get { return (string)GetValue(xName_Prev); }
            set { SetValue(xName_Prev, value); }
        }

        public string xNameNext
        {
            get { return (string)GetValue(xName_Next); }
            set { SetValue(xName_Next, value); }
        }

        public Binding EditDelegate
        {
            get { return (Binding)GetValue(Edit_Delegate); }
            set { SetValue(Edit_Delegate, value); }
        }

        public Binding RemoveDelegate
        {
            get { return (Binding)GetValue(Remove_Delegate); }
            set { SetValue(Remove_Delegate, value); }
        }

        public Binding PrevPageDelegate
        {
            get { return (Binding)GetValue(PrevPage_Delegate); }
            set { SetValue(PrevPage_Delegate, value); }
        }

        public Binding NextPageDelegate
        {
            get { return (Binding)GetValue(NextPage_Delegate); }
            set { SetValue(NextPage_Delegate, value); }
        }

        static FilterPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterPanel),
              new FrameworkPropertyMetadata(typeof(FilterPanel)));

            xName_Edit = DependencyProperty.Register("Edit.x:Name",
               typeof(string), typeof(FilterPanel), new UIPropertyMetadata(null));

            xName_Remove = DependencyProperty.Register("Remove.x:Name",
               typeof(string), typeof(FilterPanel), new UIPropertyMetadata(null));

            xName_Prev = DependencyProperty.Register("Prev.x:Name",
               typeof(string), typeof(FilterPanel), new UIPropertyMetadata(null));

            xName_Edit = DependencyProperty.Register("Next.x:Name",
               typeof(string), typeof(FilterPanel), new UIPropertyMetadata(null));

            Edit_Delegate = DependencyProperty.Register("Edit.IsEnabled",
               typeof(Binding), typeof(FilterPanel), new UIPropertyMetadata(null));

            Remove_Delegate = DependencyProperty.Register("Remove.IsEnabled",
               typeof(Binding), typeof(FilterPanel), new UIPropertyMetadata(null));

            PrevPage_Delegate = DependencyProperty.Register("PrevPage.IsEnabled",
               typeof(Binding), typeof(FilterPanel), new UIPropertyMetadata(null));

            NextPage_Delegate = DependencyProperty.Register("NextPage.IsEnabled",
               typeof(Binding), typeof(FilterPanel), new UIPropertyMetadata(null));
        }
    }
}
