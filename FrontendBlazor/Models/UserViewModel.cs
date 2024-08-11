namespace FrontendBlazor.Models
{
    public class UserViewModel
    {
        public string IdUser { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Dni { get; set; } = null!;
        public int DniType { get; set; }
        public string Email { get; set; } = null!;
        public string Number { get; set; } = null!;
        public int UserRole { get; set; }
        public int? AddressId { get; set; }

    }
}
