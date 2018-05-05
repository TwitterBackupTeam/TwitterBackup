using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Web.Areas.Admin.Models.Users
{
	public class ListUsersAndRolesViewModel
    {
		public ICollection<UserDTO> Users { get; set; }

		public ICollection<SelectListItem> Roles { get; set; }
	}
}
