using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FrontendBlazor.Models
{
    public class TicketViewModel
    {
        public int IdTicket { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El campo Nombre no tiene un formato válido.")]
        public string? Name { get; set; }


        [Required(ErrorMessage = "El campo Correo es requerido.")]
        [EmailAddress(ErrorMessage = "El campo Correo no tiene un formato válido.")]
        public string? Mail { get; set; }

        [Required(ErrorMessage = "El campo Consulta es requerido.")]
        public string? QUERY { get; set; }

        public string TicketState { get; set; } = "Pendiente de Contestar";
    }
}
