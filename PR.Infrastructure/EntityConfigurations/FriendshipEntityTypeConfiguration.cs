using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
{
	public void Configure(EntityTypeBuilder<Friendship> builder)
	{
		builder.ToTable("friendships");

		builder.HasKey(f => f.Id);

		builder.Ignore(f => f.DomainEvents);

		builder.Property(f => f.Id)
			.UseHiLo("friendshipseq");

		builder.Property(f => f.SenderIdentityGuid);
		builder.Property(f => f.ReceiverIdentityGuid);

		//var navigation = builder.Metadata.FindNavigation(nameof(Friendship.FriendRequests));

		//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}