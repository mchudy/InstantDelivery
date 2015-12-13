using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model
{
    /// <summary>
    /// Obiekt DTO reprezentujący dane potrzebne do zmiany hasła.
    /// </summary>
    public class ChangePasswordDto : ValidationBase
    {
        /// <summary>
        /// Aktualne hasło
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Nowe hasło
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi składać się z co najmniej 6 znaków")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Potwierdzenie nowego hasła
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi składać się z co najmniej 6 znaków")]
        [Compare(nameof(NewPassword), ErrorMessage = "Hasła muszą się zgadzać")]
        public string ConfirmNewPassword { get; set; }
    }
}
