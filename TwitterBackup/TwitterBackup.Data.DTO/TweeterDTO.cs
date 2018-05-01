namespace TwitterBackup.Data.DTO
{
    public class TweeterDTO
    {
        public long Id { get; set; }

        public string ScreenName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }
    }
}
