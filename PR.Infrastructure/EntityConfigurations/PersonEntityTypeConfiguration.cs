using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PR.Domain.AggregatesModel.PersonAggregate;

namespace PR.Infrastructure.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.ToTable("persons", PRDbContext.DEFAULT_SCHEMA);

		builder.HasKey(p => p.Id);

		builder.Ignore(p => p.DomainEvents);

		builder.Property(p => p.Id)
			.UseHiLo("personseq", PRDbContext.DEFAULT_SCHEMA);

		builder.Property(p => p.IdentityGuid)
			.HasMaxLength(200)
			.IsRequired();

		builder.HasIndex("IdentityGuid")
			.IsUnique();

		builder.Property(p => p.FirstName);
		builder.Property(p => p.LastName);
		builder.Property(p => p.Username);

		builder.HasMany(p => p.FriendRequests)
			.WithOne()
			.HasForeignKey("PersonId")
			.OnDelete(DeleteBehavior.Cascade);

		var navigation = builder.Metadata.FindNavigation(nameof(Person.FriendRequests));

		navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}