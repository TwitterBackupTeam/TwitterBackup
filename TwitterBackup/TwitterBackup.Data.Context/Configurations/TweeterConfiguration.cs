using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Context.Configurations
{
	public class TweeterConfiguration : IEntityTypeConfiguration<Tweeter>
	{
		public void Configure(EntityTypeBuilder<Tweeter> builder)
		{
			builder.HasMany(m => m.TweetCollection)
				.WithOne(m => m.Followee)
				.HasForeignKey(fk => fk.FolloweeId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
