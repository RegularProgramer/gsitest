namespace FrontendBlazor.Models
{
    public class LogCambioViewModel
    {


        public int IdPolicy { get; set; }
        public int PolicyTypeId { get; set; }
        public decimal PolicyPremium { get; set; }
        public DateTime PolicyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IdPolicyState { get; set; }
        public string IdUser { get; set; } = null!;
        public string? TipoAccion { get; set; }
        public DateTime? FechaAccion { get; set; }
        public string? Reason { get; set; }
        public int? Qpolicy { get; set; }

        public string? State { get; set; } = null!;


        public string? modHow { get; set; } = null;
        public string? lastUser { get; set; } = null;

    }
}
