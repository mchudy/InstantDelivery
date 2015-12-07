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

namespace InstantDelivery.Controls
{
    /// <summary>
    /// Interaction logic for PackagesFilterPanel.xaml
    /// </summary>
    public partial class PackagesFilterPanel : UserControl
    {


        public static readonly DependencyProperty IsEnabledFitlerEmployeeIdProperty= DependencyProperty.Register("IsEnabledFitlerEmployeeId",typeof(bool),typeof(PackagesFilterPanel),new FrameworkPropertyMetadata(true));

        public bool IsEnabledFitlerEmployeeId
        {
            get { return (bool)GetValue(IsEnabledFitlerEmployeeIdProperty); }
            set { SetValue(IsEnabledFitlerEmployeeIdProperty, value); }
        }


        public PackagesFilterPanel()
        {
            InitializeComponent();


        }
    }
}
