using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persons.Domain.AggregatesModel.PersonAggregate;

namespace Persons.Infrastructure.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
	public void Configure(EntityTypeBuilder<Person> builder)
	{
		builder.ToTable("persons");

		builder.HasKey(p => p.Id);

		builder.Ignore(p => p.DomainEvents);

		builder.Property(p => p.Id)
			.UseHiLo("personseq");

		builder.Property(p => p.IdentityGuid)
			.HasMaxLength(200)
			.IsRequired();

		builder.HasIndex("IdentityGuid")
			.IsUnique();

		builder.Property(p => p.FirstName);
		builder.Property(p => p.LastName);
		builder.Property(p => p.Username);


		//var navigation = builder.Metadata.FindNavigation(nameof(Person.FriendRequests));

		//navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
	}
}