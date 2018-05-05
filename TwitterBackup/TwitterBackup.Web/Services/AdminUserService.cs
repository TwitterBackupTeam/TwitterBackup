using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Web.Services
{
	public class AdminUserService : IAdminUserService
	{
		private readonly IWorkSaver workSaver;
		private readonly UserManager<User> userManager;
		private readonly IAutoMapper mapper; 

		public AdminUserService(IWorkSaver workSaver, UserManager<User> userManager, IAutoMapper mapper)
		{
			this.workSaver = workSaver ?? throw new ArgumentNullException();
			this.userManager = userManager;
			this.mapper = mapper;
		}

		public Task<User> GetUserByUsernameAsync(string userName)
		{
			throw new NotImplementedException();
		}

		public void DeleteUserByUserId(string userId)
		{
			throw new NotImplementedException();
		}

		//public async Task<ICollection<UserDTO>> GetUsersInRolesAsync()
		//=> await this.workSaver.UserRepository.All().AsQueryable().ProjectTo<UserDTO>.ToListAsyns();
	}
}
