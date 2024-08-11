using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontendBlazor.Models
{
	public class AspNetRoleViewModel
    {
        public string Id { get; set; } = null!;

        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username can only contain letters and digits.")]
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

    }
}
