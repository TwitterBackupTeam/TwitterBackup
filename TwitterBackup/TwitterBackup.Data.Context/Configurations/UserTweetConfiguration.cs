using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Context.Configurations
{
	public class UserTweetConfiguration : IEntityTypeConfiguration<UserTweet>
	{
		public void Configure(EntityTypeBuilder<UserTweet> builder)
		{
			builder.HasKey(e => new { e.UserId, e.TweetId });

			builder.HasOne(e => e.User)
				.WithMany(a => a.UserTweets)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.Tweet)
				.WithMany(a => a.UserTweets)
				.HasForeignKey(e => e.TweetId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
