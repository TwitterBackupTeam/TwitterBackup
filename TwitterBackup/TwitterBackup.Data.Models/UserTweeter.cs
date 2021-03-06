﻿using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Data.Models.Abstract;

namespace TwitterBackup.Data.Models
{
	public class UserTweeter : IDeletable
    {
		[Required]
		public string UserId { get; set; }
		public User User { get; set; }

		[Required]
		public long TweeterId { get; set; }
		public Tweeter Tweeter { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }
	}
}
