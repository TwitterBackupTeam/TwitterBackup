using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface IAdminUserService
	{
		Task<IEnumerable<UserDto>> AllAsync();

		Task<User> SingleUserByUsernameAsync(string userName);

		void DeleteByUserId(string userId);
	}
}
