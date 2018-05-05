using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Web.Areas.Admin.Models.Users
{
	public class UpdateUserRoleViewModel
    {
		public class AddUserToRoleViewModel
		{
			[Required]
			public long UserId { get; set; }

			[Required]
			public string Role { get; set; }
		}
	}
}
