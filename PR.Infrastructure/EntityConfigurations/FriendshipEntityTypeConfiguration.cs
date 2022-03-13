using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.FriendshipAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
{
	public void Configure(EntityTypeBuilder<Friendship> builder)
	{
		builder.ToTable("friendships");

		builder.HasKey(f => new { f.SenderId, f.ReceiverId });

		builder.Ignore(f => f.DomainEvents);

		builder.HasKey(o => o.Id);

		builder.HasOne(s => s.Sender)
			.WithMany()
			.HasForeignKey(f => f.Sender.Id)
			.OnDelete(DeleteBehavior.NoAction);

		builder.HasOne(s => s.Receiver)
			.WithMany()
			.HasForeignKey(f => f.Receiver.Id)
			.OnDelete(DeleteBehavior.NoAction);

		
		//var navigation = builder.Metadata.FindNavigation(nameof(Friendship.FriendRequests));

		//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}