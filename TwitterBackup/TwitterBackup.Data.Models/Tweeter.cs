using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Data.Models
{
    public class Tweeter
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }
    }
}
