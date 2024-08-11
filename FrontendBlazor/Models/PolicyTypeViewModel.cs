namespace FrontendBlazor.Models
{
    public class PolicyTypeViewModel
    {

		public int PolicyTypeId { get; set; }
		public int InsuraceCId { get; set; }
		public DateTime CreatedDate { get; set; }
		public string PolicyDateRange { get; set; } = null!;
		public string PolicyCostRange { get; set; } = null!;
		public int PolicyClassId { get; set; }
		public string? AcceptedCurrency { get; set; }
		public int? PolicyLineId { get; set; }
		public string PolicyCod { get; set; } = null!;
		public string Validity { get; set; } = null!;
		public string ContractType { get; set; } = null!;
		public string? PolicyName { get; set; }
		public string? Aply { get; set; }


		public PolicyClassViewModel? PolicyClasses { get; set; } = null; 

    }
}
