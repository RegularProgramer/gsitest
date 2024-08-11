namespace FrontendBlazor.Models
{
    public class ScrapQpolicyViewModel
    {
        public int IdPolicy { get; set; }
        public int PolicyTypeId { get; set; }
        public decimal PolicyPremium { get; set; }
        public DateTime PolicyDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int IdPolicyState { get; set; }
        public string IdUser { get; set; } = null!;
        public string InsuranceBrokerId { get; set; } = null!;

        public string? PolicyNumber { get; set; } = null!;

        



    }
}
