using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.DTO.UserManagementDTOs;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface ITweeterService
    {
		List<ListTweetersDTO> GetFavouriteTweetersByUserId(string userId);

		TweeterDTO GetTweeterById(long tweeterId);
		
		Tweeter CreateTweeter(TweeterDTO tweeter);

		Task UpdateTweeterById(long tweeterId);

		void DeleteTweeterById(long tweeterId);

		bool DbContainsTweeter(long tweeterId);
	}
}
