namespace FrontendBlazor.Models
{
    public class AppointmentViewModel
    {

        //public Guid AppointmentID { get; set; }
        public int IdAppointment { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentHour { get; set; }
        public string IdUser { get; set; } = null!;
        public string ClientCell { get; set; } = null!;

        public string? Place { get; set; } = null!;

        public string? Comment { get; set; } = null!;

        public string ClientMail { get; set; } = null!;
        public int StatusAppointmentId { get; set; }
    }
}
