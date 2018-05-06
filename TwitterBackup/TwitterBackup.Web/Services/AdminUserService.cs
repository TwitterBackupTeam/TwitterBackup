using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Web.Services
{
	public class AdminUserService : DatabaseService, IAdminUserService
	{
		private readonly IWorkSaver workSaver;
		private readonly UserManager<User> userManager;
		private readonly IRepository<User> userRepository;

		public AdminUserService(UserManager<User> userManager, IRepository<User> userRepository, 
								IAutoMapper autoMapper, IWorkSaver workSaver) : base(autoMapper, workSaver)
		{
			this.workSaver = workSaver ?? throw new ArgumentNullException(nameof(workSaver));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		public async Task<User> GetUserByUsernameAsync(string userName)
		=> await this.userRepository.All().FirstOrDefaultAsync(w => w.UserName == userName);

		public void DeleteUserByUserId(string userId)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("UserID cannot be null!");
			}
			if (userId == string.Empty)
			{
				throw new ArgumentException("UserID cannot be empty!");
			}

			var user = this.userRepository.All().FirstOrDefault(fd => fd.Id == userId);

			if (user == null)
			{
				throw new ArgumentNullException("No such user found!");
			}

			this.userRepository.Delete(user);
			this.workSaver.SaveChanges();
		}

		//public async Task<ICollection<UserDTO>> GetUsersInRolesAsync()
		//=> await this.workSaver.UserRepository.All().AsQueryable().ProjectTo<UserDTO>.ToListAsyns();
	}
}
