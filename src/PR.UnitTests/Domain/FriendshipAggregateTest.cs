﻿using System;
using FluentAssertions;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using Xunit;

namespace PR.UnitTests.Domain;

public class FriendshipAggregateTest
{
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		
		//Act
		var result = new Friendship(senderIdentityGuid, receiverIdentityGuid);
		
		//Assert
		result.Should().NotBeNull();
		result.SenderIdentityGuid.Should().NotBeNull();
		result.ReceiverIdentityGuid.Should().NotBeNull();
	}
}