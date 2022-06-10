using System;
using FluentAssertions;
using PR.Domain.AggregatesModel.PersonAggregate;
using Xunit;

namespace PR.UnitTests.Domain;

public class PersonAggregateTest
{
	[Fact]
	public void Create_Person_Success()
	{
		//Arrange
		var identityGuid = Guid.NewGuid().ToString();

		//Act
		var fakePerson = new Person(identityGuid);

		//Assert
		fakePerson.Should().NotBeNull();
		fakePerson.IdentityGuid.Should().Be(identityGuid);
	}

	[Fact]
	public void Create_Person_IdentityGuid_Failure()
	{
		//Arrange
		var identityGuid = string.Empty;
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = "Adven";

		//Act
		var act = () =>
		{
			var unused = new Person(identityGuid);
		};

		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(identityGuid)).Message);
	}
}