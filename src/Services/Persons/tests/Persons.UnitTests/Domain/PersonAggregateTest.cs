using System;
using FluentAssertions;
using Persons.Domain.AggregatesModel.FriendshipAggregate;
using Xunit;

namespace Persons.UnitTests.Domain;

public class PersonAggregateTest
{
	[Fact]
	public void Create_Person_Success()
	{
		//Arrange
		var id = 1;
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = "Adven";

		//Act
		var fakePerson = new Person(id, identityGuid, username, firstName, lastName);

		//Assert
		fakePerson.Should().NotBeNull();
		fakePerson.Should().Be(id);
		fakePerson.IdentityGuid.Should().Be(identityGuid);
		fakePerson.FirstName.Should().Be(firstName);
		fakePerson.LastName.Should().Be(lastName);
		fakePerson.Username.Should().Be(username);
	}

	[Fact]
	public void Create_Person_IdentityGuid_Failure()
	{
		//Arrange
		var id = 1;
		var identityGuid = string.Empty;
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = "Adven";

		//Act
		var act = () =>
		{
			var unused = new Person(id, identityGuid, username, firstName, lastName);
		};

		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(identityGuid)).Message);
	}

	[Fact]
	public void Create_Person_Username_Failure()
	{
		//Arrange
		var id = 1;
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = string.Empty;

		//Act
		Action act = () =>
		{
			var unused = new Person(1, identityGuid, username, firstName, lastName);
		};

		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(username)).Message);
	}

	[Fact]
	public void Create_Person_FirstName_Failure()
	{
		//Arrange
		var id = 1;
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = string.Empty;
		var lastName = "Dimitrako";
		var username = "Adven";

		//Act
		Action act = () =>
		{
			var unused = new Person(id, identityGuid, username, firstName, lastName);
		};

		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(firstName)).Message);
	}

	[Fact]
	public void Create_Person_LastName_Failure()
	{
		//Arrange
		var id = 1;
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = string.Empty;
		var username = "Adven";

		//Act
		Action act = () =>
		{
			var unused = new Person(id, identityGuid, username, firstName, lastName);
		};

		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(lastName)).Message);
	}
}