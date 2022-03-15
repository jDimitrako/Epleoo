using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class FriendRequestEntityTypeConfiguration : IEntityTypeConfiguration<FriendRequest>
{
	public void Configure(EntityTypeBuilder<FriendRequest> builder)
	{
		builder.ToTable("FriendRequests");

		builder.HasKey(f => f.Id);

		builder.Ignore(f => f.DomainEvents);
		
		builder.Property(f => f.Id)
			.UseHiLo("friendrequestseq");
		
		builder.Property(f => f.SenderPersonId);
		builder.Property(f => f.ReceiverPersonId);
		builder.Property(f => f.CreatedDate);
		builder.Property(f => f.Modifier);
		builder.Property(f => f.ModifiedDate);
		builder
			.Property<int>("_friendRequestStatusId")
			// .HasField("_friendRequestStatusId")
			.UsePropertyAccessMode(PropertyAccessMode.Field)
			.HasColumnName("FriendRequestStatusId")
			.IsRequired();
		
		builder.HasOne(f => f.FriendRequestStatus)
			.WithMany()
			// .HasForeignKey("OrderStatusId");
			.HasForeignKey("_friendRequestStatusId");
	}
}