using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.FriendRequestAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class FriendRequestStatusEntityTypeConfiguration : IEntityTypeConfiguration<FriendRequestStatus>
{
	public void Configure(EntityTypeBuilder<FriendRequestStatus> builder)
	{
		builder.ToTable("friendRequestStatus");
		builder.HasKey(f => f.Id);
		builder.Property(f => f.Id)
			.HasDefaultValue(1)
			.ValueGeneratedNever()
			.IsRequired();
		builder.Property(o => o.Name)
			.HasMaxLength(200)
			.IsRequired();
	}
}