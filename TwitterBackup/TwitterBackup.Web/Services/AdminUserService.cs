using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Web.Services
{
	public class AdminUserService : IAdminUserService
	{
		public Task<IEnumerable<UserDto>> AllAsync()
		{
			throw new NotImplementedException();
		}

		public void DeleteByUserId(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> SingleUserByUsernameAsync(string userName)
		{
			throw new NotImplementedException();
		}
	}
}
