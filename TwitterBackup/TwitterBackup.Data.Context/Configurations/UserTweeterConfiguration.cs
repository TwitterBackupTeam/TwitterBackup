using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Context.Configurations
{
	public class UserTweeterConfiguration : IEntityTypeConfiguration<UserTweeter>
	{
		public void Configure(EntityTypeBuilder<UserTweeter> builder)
		{
			builder.HasKey(e => new { e.TweeterId, e.UserId });

			builder.HasOne(e => e.Tweeter)
				.WithMany(a => a.UserTweeters)
				.HasForeignKey(e => e.TweeterId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(e => e.User)
				.WithMany(a => a.UserTweeters)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
