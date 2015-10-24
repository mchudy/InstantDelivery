using Caliburn.Micro;

namespace InstantDelivery.ViewModel
{
    public class ShellViewModel : PropertyChangedBase
    {
        private string name = "John";

        public string FirstName
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange();
            }
        }

        public void Whatever()
        {
            FirstName = "SDLFJ:LFDJ";
        }
    }
}
