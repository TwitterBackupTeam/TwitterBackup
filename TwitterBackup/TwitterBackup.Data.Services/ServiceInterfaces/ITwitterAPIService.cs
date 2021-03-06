﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface ITwitterAPIService
    {
        Task<ICollection<TweetDTO>> GetTweets(string screenName);

		Task<TweeterDTO[]> GetTweetersByScreenName(string screenName);

		Task<TweeterDTO> GetTweeterInfoById(long id);

	}
}
