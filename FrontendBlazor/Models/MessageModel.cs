using MimeKit;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontendBlazor.Models
{
    public class MessageModel
    {



        //public List<MailboxAddress>? To { get; set; } = null;
        [DisplayName("Titulo"),Required(ErrorMessage ="El titulo es nesesario para enviar el mensaje")]
        public string Subject { get; set; }

        [DisplayName("Contenido"), Required(ErrorMessage = "El contenido es nesesario para enviar el mensaje")
            ,MinLength(100, ErrorMessage = "Es nesesario que el mensaje contengo 100 o mas caracteres")]
        public string Content { get; set; }

        public List<KeyValuePair<string, string>>? PlaceHolders { get; set; } = null;

        [DisplayName("Plantilla"), Required(ErrorMessage = "Seleccionar una plantilla es nesesario para enviar el mensaje")
          ]
        public string EmailTemplate { get; set; }

        [DisplayName("Plantilla"), Required(ErrorMessage = "Debera de seleccionar almenos una dirección de correo")
         ]
        public List<string>? Address { get; set; } = null;
             

        public List<AspNetUserViewModel>? aspNetUserViewModels { get; set; } = null;
        public List<TemplateViewModel>? Templates { get; set; } = null;


        public Boolean AllUsers { get; set; }

        public string? ContentPreview { get; set; } = null;

    }
}