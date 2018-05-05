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
			this.workSaver = workSaver ?? throw new ArgumentNullException();
		}

		public async Task<ICollection<UserDto>> GetAllUsersAsync()
		=> await this.workSaver.UserRepository.All().ProjectTo<UserDto>.ToListAsync();

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
