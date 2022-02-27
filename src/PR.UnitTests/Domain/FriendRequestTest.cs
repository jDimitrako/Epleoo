using System;
using FluentAssertions;
using Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;
using PR.Domain.AggregatesModel.PersonAggregate;
using Xunit;

namespace PR.UnitTests.Domain;

public class FriendRequestTest
{
	public FriendRequestTest()
	{ }
	
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		
		//Act
		var result = new FriendRequest(senderIdentityGuid, receiverIdentityGuid);
		
		//Assert
		result.Should().NotBeNull();
		result.IsEqualTo(senderIdentityGuid, receiverIdentityGuid).Should().BeTrue();
		result.SenderIdentityGuid.Should().Be(senderIdentityGuid);
		result.ReceiverIdentityGuid.Should().Be(receiverIdentityGuid);
	}
	
	[Fact]
	public void Create_FriendRequest_SenderGuid_Failure()
	{
		//Arrange
		var senderIdentityGuid = string.Empty;
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		
		//Act
		Action act = () =>
		{
			var friendRequest = new FriendRequest(senderIdentityGuid, receiverIdentityGuid);
		}; 
		
		//Assert
		act.Should().Throw<PRDomainException>()
			.WithMessage(nameof(senderIdentityGuid));
	}
	
	[Fact]
	public void Create_FriendRequest_ReceiverGuid_Failure()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = string.Empty;
		
		//Act
		Action act = () =>
		{
			var friendRequest = new FriendRequest(senderIdentityGuid, receiverIdentityGuid);
		}; 
		
		//Assert
		act.Should().Throw<PRDomainException>()
			.WithMessage(nameof(receiverIdentityGuid));
	}
}