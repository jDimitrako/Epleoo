using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
{
	public void Configure(EntityTypeBuilder<Friendship> builder)
	{
		builder.ToTable("friendships");

		builder.HasKey(f => new { f.SenderId, f.ReceiverId });

		builder.Ignore(f => f.DomainEvents);

		builder.HasKey(o => o.Id);

		/*
		builder.HasMany(f => f.Persons)
			.WithMany(p => p.Friendships)
			.UsingEntity(j => j.ToTable("PersonFriendships"));*/

		builder.HasOne(s => s.Sender)
			.WithMany(p => p.FriendshipsSent)
			.HasForeignKey(f => f.SenderId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasOne(s => s.Receiver)
			.WithMany(p => p.FriendshipsReceived)
			.HasForeignKey(f => f.ReceiverId)
			.OnDelete(DeleteBehavior.Restrict);


		//var navigation = builder.Metadata.FindNavigation(nameof(Friendship.FriendRequests));

		//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}