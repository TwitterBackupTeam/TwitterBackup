using System.Collections.Generic;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Web.Areas.Admin.Models.Users
{
	public class ListUsersViewModel
    {
		public IEnumerable<UserDTO> Users { get; set; }
	}
}
