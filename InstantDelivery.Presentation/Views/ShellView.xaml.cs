using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using InstantDelivery.ViewModel;
using MahApps.Metro.Controls.Dialogs;

namespace InstantDelivery.Views
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        //public SplashScreenViewModel(/*ISchoolTestMakerServiceProxy serviceProxy*/)
        //{
        //    //this.serviceProxy = serviceProxy;
        //}

            //TODO move on server?
        public bool Login(string userName, string password)
        {
            return false;//serviceProxy.Connect(userName, password);
        }
        // nie wiem czy to sie wogole nadaje, mahapp niby ma cos takiego, ale jak przegladalem tą przykladową aplikacje to myslalem
        // ze oslepne.
        async private void WindowLoaded(object sender,RoutedEventArgs e)
        {
            this.Loaded -= WindowLoaded;
            bool dialogResult = true;
            LoginDialogData loginDialogData;        
            do
            {
                loginDialogData = await this.ShowLoginAsync
                    (
                        "Zaloguj się",
                        "",
                        new LoginDialogSettings
                        {
                            ColorScheme = MetroDialogColorScheme.Accented,
                            UsernameWatermark = "Nazwa użytkownika...",
                            PasswordWatermark = "Hasło...",
                            NegativeButtonVisibility = Visibility.Visible,
                            
                        }
                    );
                if (loginDialogData == null)
                {
                    dialogResult = false;
                    break;
                }
            }
            while (!(await AttemptLogin(loginDialogData.Username, loginDialogData.Password)));
            CloseWindow(dialogResult);
        }

        private Task<bool> AttemptLogin(string userName, string password)
        {
            return Task.Run<bool>(() => Login(userName, password));
        }

        private void CloseWindow(bool dialogResult)
        {
            ((ShellViewModel)this.DataContext).TryClose(false);
        }
    }
}
