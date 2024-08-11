using System.ComponentModel;

namespace FrontendBlazor.Models
{
	public class AspNetUserViewModel
    {
		public string Id { get; set; } = null!;
        [DisplayName("Nombre de Usuario")]
        public string? UserName { get; set; }
		public string? NormalizedUserName { get; set; }
        [DisplayName("Correo Electrónico")]
        public string? Email { get; set; }
		public string? NormalizedEmail { get; set; }
		public bool EmailConfirmed { get; set; }
		public string? PasswordHash { get; set; }
		public string? SecurityStamp { get; set; }
		public string? ConcurrencyStamp { get; set; }
        [DisplayName("Numero de Teléfono")]
        public string? PhoneNumber { get; set; }
		public bool PhoneNumberConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public DateTimeOffset? LockoutEnd { get; set; }
		public bool LockoutEnabled { get; set; }
		public int AccessFailedCount { get; set; }
        [DisplayName("Numero de Cédula")]
        public string? Identification { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public int? IdStatus { get; set; }
    }
}
