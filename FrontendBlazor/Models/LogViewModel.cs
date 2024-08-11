namespace FrontendBlazor.Models
{
    public class LogViewModel
    {

        public int LogId { get; set; }

        public string IdUser { get; set; } = null!;

        public string ActionMake { get; set; } = null!;

        public string TableReference { get; set; } = null!;

        public DateTime LogTimestamp { get; set; }

        public int? modObject { get; set; } = null!;

        public string? userId { get; set; } = null;

        public string? UserName { get; set; } = null!;
    }
}
