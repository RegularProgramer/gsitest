namespace FrontendBlazor.Helpers.ServiceEmail
{
    public interface IEmailService
    {
        Task SendCancellationConfirmationEmail(string clientMail, DateTime appointmentDate, int appointmentHour);
    }

}
