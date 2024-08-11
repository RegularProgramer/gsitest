namespace FrontendBlazor.Models
{
    public class InsuranceCompanyViewModel
    {

        public int InsuranceCompanyId { get; set; }

        public string InsuranceCompanyName { get; set; } = null!;

        public string InsuranceCompanyNumber { get; set; } = null!;

        public string InsuranceCompanyMail { get; set; } = null!;

        public string InsuranceCompanyDescription { get; set; } = null!;

        public List<PolicyClassViewModel> classes { get;   set; } = null!;

		public List<PolicyTypeViewModel> types { get; set; } = null!;

	}
}
