using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Web.Services
{
	public class AdminUserService : IAdminUserService
	{
		private readonly IWorkSaver workSaver;

		public AdminUserService(IWorkSaver workSaver)
		{
			if (workSaver == null)
			{
				throw new ArgumentNullException();
			}

			this.workSaver = workSaver;
		}

		//public async Task<ICollection<UserDto>> GetAllUsersAsync()
		//=> await this.workSaver.
		// add users repo

		public Task<User> GetUserByUsernameAsync(string userName)
		{
			throw new NotImplementedException();
		}

		public void DeleteUserByUserId(string userId)
		{
			throw new NotImplementedException();
		}

	}
}
