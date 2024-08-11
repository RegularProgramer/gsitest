namespace FrontendBlazor.Models
{
    public class AppointmentDay
    {
        public int Id { get; set; }

        public string DayText { get; set; }

        public DateTime? Date { get; set; } = null;

        public List<AppointmentSpace>? Spaces { get; set; } = null;

        public List<AppointmentHour>? Hours { get; set; } = null;

    }
}
