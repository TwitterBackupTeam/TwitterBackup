using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface IAdminUserService
	{
		Task<ICollection<UserDto>> GetAllUsersAsync();

		Task<User> GetUserByUsernameAsync(string userName);

		void DeleteUserByUserId(string userId);
	}
}
