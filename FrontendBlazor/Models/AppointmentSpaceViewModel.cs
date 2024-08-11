namespace FrontendBlazor.Models
{
    public class AppointmentSpaceViewModel
    {
        public int IdAppointmentSpace { get; set; }
        public string IdUser { get; set; } = null!;
        public int? Duration { get; set; }
        public string? Schedule { get; set; }
    }
}
