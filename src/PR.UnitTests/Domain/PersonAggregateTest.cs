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
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		var personFirstName = "Dimitris";
		var personLastName = "Dimitrako";
		var fakePerson = new Person(senderIdentityGuid, personFirstName, personLastName);
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