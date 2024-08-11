
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FrontendBlazor.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }

        [DataType(DataType.Password), Required , DisplayName("Digite la nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password), Required, DisplayName("Vuelva a digitar la contraseña")]
        public string ConfirmNewPassword { get; set; }


        public bool IsSuccess { get; set; }


    }
}