using System;
using FluentAssertions;
using PR.Domain.AggregatesModel.FriendRequestAggregate;
using PR.Domain.Exceptions;
using Xunit;

namespace PR.UnitTests.Domain;

public class FriendRequestTest
{
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = Guid.NewGuid().ToString();
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		
		//Act
		var result = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
		
		//Assert
		result.Should().NotBeNull();
		result.IsEqualTo(senderIdentityGuid, receiverIdentityGuid).Should().BeTrue();
		result.SenderIdentityGuid.Should().Be(senderIdentityGuid);
		result.ReceiverIdentityGuid.Should().Be(receiverIdentityGuid);
		result.Modifier.Should().Be(senderIdentityGuid);
		result.CreatedDate.Should().Subject.Should().NotBeNull();
		result.ModifiedDate.Should().Subject.Should().NotBeNull();
	}
	
	[Fact]
	public void Create_FriendRequest_SenderGuid_Failure()
	{
		//Arrange
		var senderIdentityGuid = string.Empty;
		var receiverIdentityGuid = Guid.NewGuid().ToString();
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		//Act
		Action act = () =>
		{
			var unused = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
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
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		
		//Act
		Action act = () =>
		{
			var unused = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
		}; 
		
		//Assert
		act.Should().Throw<PRDomainException>()
			.WithMessage(nameof(receiverIdentityGuid));
	}
}