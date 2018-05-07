using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Web.Services
{
	public class AdminUserService : IAdminUserService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly UserManager<User> userManager;

		public AdminUserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
		}

		public async Task<User> GetUserByUsernameAsync(string userName)
		=> await this.unitOfWork.UsersRepository.All().FirstOrDefaultAsync(w => w.UserName == userName);

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

			var user = this.unitOfWork.UsersRepository.All().FirstOrDefault(fd => fd.Id == userId);

			if (user == null)
			{
				throw new ArgumentNullException("No such user found!");
			}

			this.unitOfWork.UsersRepository.Delete(user);
			this.unitOfWork.SaveChanges();
		}
	}
}
