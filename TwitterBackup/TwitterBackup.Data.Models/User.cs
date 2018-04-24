using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models
{
    public class User : IdentityUser
    {
        public ICollection<Tweet> FavouriteTweets { get; set; }
    }
}
