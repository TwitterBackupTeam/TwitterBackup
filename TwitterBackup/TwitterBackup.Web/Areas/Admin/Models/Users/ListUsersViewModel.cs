using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Web.Areas.Admin.Models.Users
{
	public class ListUsersViewModel
    {
		public ICollection<UserDTO> Users { get; set; }
	}
}
