using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Model
{
    public class ChangePasswordDto : ValidationBase
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi składać się z co najmniej 6 znaków")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(6, ErrorMessage = "Hasło musi składać się z co najmniej 6 znaków")]
        [Compare(nameof(NewPassword), ErrorMessage = "Hasła muszą się zgadzać")]
        public string ConfirmNewPassword { get; set; }
    }
}
