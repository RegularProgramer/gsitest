namespace FrontendBlazor.Models
{
    public class GoodViewModel
    {
        public int IdGood { get; set; }

        public string IdUser { get; set; } = null!;
        public int IdPolicy { get; set; }
        public int GoodTypeId { get; set; }
        public string InsuranceBrokerId { get; set; } = null!;

        public string GoodDescription { get; set; } = null!;
        public DateTime? GoodDate { get; set; } = null;


        public IEnumerable<GoodTypeViewModel>? types { get; set; } = null;


    }
}
