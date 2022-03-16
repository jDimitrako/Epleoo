using System;
using FluentAssertions;
using Persons.Domain.AggregatesModel.FriendshipAggregate;
using Xunit;

namespace Persons.UnitTests.Domain;

public class FriendshipAggregateTest
{
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = 1;
		var receiverIdentityGuid = 2;
		
		//Act
		var result = new Friendship(senderIdentityGuid, receiverIdentityGuid);
		
		//Assert
		result.Should().NotBeNull();
	}
}