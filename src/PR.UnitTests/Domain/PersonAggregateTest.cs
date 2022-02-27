using System;
using System.Linq;
using FluentAssertions;
using PR.Domain.AggregatesModel.PersonAggregate;
using Xunit;

namespace PR.UnitTests.Domain;

public class PersonAggregateTest
{
	public PersonAggregateTest()
	{ }

	[Fact]
	public void Create_Person_Success()
	{
		//Arrange
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = "Adven";
		
		//Act
		var fakePerson = new Person(identityGuid, username, firstName, lastName);
		
		//Assert
		fakePerson.Should().NotBeNull();
		fakePerson.IdentityGuid.Should().Be(identityGuid);
		fakePerson.FirstName.Should().Be(firstName);
		fakePerson.LastName.Should().Be(lastName);
		fakePerson.Username.Should().Be(username);
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
		Action act = () =>
		{
			var friendRequest = new Person(identityGuid, username, firstName, lastName);
		}; 
		
		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(identityGuid)).Message);
	}
	
	[Fact]
	public void Create_Person_Username_Failure()
	{
		//Arrange
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = "Dimitrako";
		var username = string.Empty;

		//Act
		Action act = () =>
		{
			var friendRequest = new Person(identityGuid, username, firstName, lastName);
		}; 
		
		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(username)).Message);
	}
	
	[Fact]
	public void Create_Person_FirstName_Failure()
	{
		//Arrange
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = string.Empty;
		var lastName = "Dimitrako";
		var username = "Adven";

		//Act
		Action act = () =>
		{
			var friendRequest = new Person(identityGuid, username, firstName, lastName);
		}; 
		
		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(firstName)).Message);
	}
	
	[Fact]
	public void Create_Person_LastName_Failure()
	{
		//Arrange
		var identityGuid = Guid.NewGuid().ToString();
		var firstName = "Dimitris";
		var lastName = string.Empty;
		var username = "Adven";

		//Act
		Action act = () =>
		{
			var friendRequest = new Person(identityGuid, username, firstName, lastName);
		}; 
		
		//Assert
		act.Should().Throw<ArgumentNullException>()
			.WithMessage(new ArgumentNullException(nameof(lastName)).Message);
	}
	
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		var personFirstName = "Dimitris";
		var personLastName = "Dimitrako";
		var username = "Adven";
		var fakePerson = new Person(senderIdentityGuid, username, personFirstName, personLastName);
		var fakePersonFriendRequestsCount = fakePerson.FriendRequests.Count();
		
		//Act
		var result = fakePerson.SendFriendRequest(receiverIdentityGuid);
		
		//Assert
		result.Should().NotBeNull();
		result.IsEqualTo(senderIdentityGuid, receiverIdentityGuid).Should().BeTrue();
		fakePerson.FriendRequests.Count().Should().BeGreaterThan(fakePersonFriendRequestsCount);
		fakePerson.FriendRequests.Count().Should().Be(fakePersonFriendRequestsCount + 1);
	}
}