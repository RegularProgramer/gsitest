using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FrontendBlazor.Models
{
    public class QPolicyViewModel
    {


        public int IdPolicy { get; set; }

        [DisplayName("Tipo de poliza")]
        public int PolicyTypeId { get; set; }

        [DisplayName("Costo de la poliza"), DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true), DataType(DataType.Currency), RegularExpression(@"^\d{1,8}(\.\d{1,2})?$", ErrorMessage = "El campo debe tener máximo 8 dígitos antes del punto decimal y máximo 2 dígitos después del punto decimal.")]
        public decimal PolicyPremium { get; set; }

        [DisplayName("Fecha de Inicio")]
        public DateTime PolicyDate { get; set; }

        [DisplayName("Fecha de expiracion")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Estado de la poliza")]
        public int IdPolicyState { get; set; }

        [DisplayName("Cedula Cliente"), AllowNull]
         public string? Identification { get; set; } = null!;

                [DisplayName("Numero de Póliza")]
        [DataType(DataType.Text), StringLength(14, MinimumLength = 7, ErrorMessage = "La longitud del campo debe ser de 7 a 14 caracteres.")]        
                public string? PolicyNumber { get; set; } 
                [DisplayName("Moneda")]
        [DataType(DataType.Text),StringLength(maximumLength:20)]
                public string? Currency { get; set; } = null!;

        public IEnumerable<DniViewModel>? dniViewModels { get; set; } = null!;
        public DateTime? modStamp { get; set; } = null!;

                public string? policyState { get; set; } = null!;
                public string? policyType { get; set; } = null!;
                public string? policyLine { get; set; } = null!;

        public int? Pagination { get; set; } = null!;


        public string? modHow { get; set; } = null;
        public string? lastUser { get; set; } = null;

        public IEnumerable<PolicyStateViewModel>? states { get; set; } = null;

        public IEnumerable<InsuranceCompanyViewModel>? InsuranceCompanys { get; set; } = null;
                public IEnumerable<LogCambioViewModel>? History { get; set; } = null;

                public PolicyClassViewModel? policyClass2 { get; set; } = null;
                public InsuranceCompanyViewModel? InsuranceCompany { get; set; } = null;
                public PolicyStateViewModel? PolicyState2 { get; set; } = null;

                public PolicyTypeViewModel? PolicyType2 { get; set; } = null;

                public GoodViewModel? Good { get; set; } = null;

        public bool? Found { get; set; } = null; 


        }
}
