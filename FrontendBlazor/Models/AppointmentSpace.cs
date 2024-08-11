namespace FrontendBlazor.Models
{
    public class AppointmentSpace
    {
        public int? idAppointmentSpace = null;
        public DateTime? appointmentDate { get; set; } = null;

        public int? Duration { get; set; } = null;

        public string? adminId { get; set; } = null;

        public int? Hour { get; set; } = null;

        public bool? status { get; set; } = false;

        public string? dayOfWeek { get; set; } = null;

        public int? DayId { get; set; } = null;

        public string? FormattedHour { get; set; } = null;
    }
}
