using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontendBlazor.Models
{
	public class AspNetUserRoleViewModel
    {
        public string UserId { get; set; } = null!;

        public string RoleId { get; set; } = null!;
    }
}
