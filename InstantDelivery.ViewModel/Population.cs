using PropertyChanged;

namespace InstantDelivery.ViewModel
{
    /// <summary>
    /// Pomocnicza klasa do konstrukcji wykresu
    /// </summary>
    [ImplementPropertyChanged]
    public class Population
    {
        public string Name { get; set; } = string.Empty;

        public int Count { get; set; }
    }
}
