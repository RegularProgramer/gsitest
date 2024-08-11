
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net.Mail;
using System.Net;

namespace FrontendBlazor.Helpers.ServiceEmail
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendCancellationConfirmationEmail(string clientMail, DateTime appointmentDate, int appointmentHour)
        {


            var smtpSettings = _config.GetSection("SmtpSettings");
            var smtpServer = smtpSettings["Server"];
            var smtpPort = int.Parse(smtpSettings["Port"]);
            var smtpUsername = smtpSettings["Username"];
            var smtpPassword = smtpSettings["Password"];

            // Leer la plantilla HTML desde un archivo
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailTemplates", "CancellationConfirmation.html");
            string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

            // Reemplazar los marcadores de posición con los valores reales
            string appointmentHourString = appointmentHour.ToString();
            emailBody = emailBody.Replace("{ClientMail}", clientMail)
                                 .Replace("{AppointmentDate}", appointmentDate.ToString("yyyy-MM-dd"))
                                 .Replace("{AppointmentHour}", appointmentHourString);

            // Configurar y enviar correo electrónico
            using (var client = new SmtpClient(smtpServer, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                var message = new MailMessage();
                message.From = new MailAddress("correopruebadani1@gmail.com");
                message.To.Add("daniel.rave0903@gmail.com");
                message.To.Add(clientMail);
                message.Subject = "Confirmación de Cancelación de Cita";
                message.Body = emailBody;
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
            }
        }

    }
}
