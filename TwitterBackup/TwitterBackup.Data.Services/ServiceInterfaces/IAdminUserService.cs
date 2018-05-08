using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface IAdminUserService
	{
		Task<User> GetUserByUsernameAsync(string screenName);

		void DeleteUserByUserId(string userId);

		Task<IEnumerable<UserDTO>> GetAllUsersAsync();

		Task<User> GetUserByIdAsync(string userName);
	}
}
