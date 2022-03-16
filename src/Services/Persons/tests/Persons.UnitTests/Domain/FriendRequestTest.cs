using System;
using FluentAssertions;
using Persons.Domain.AggregatesModel.FriendRequestAggregate;
using Persons.Domain.Exceptions;
using Xunit;

namespace Persons.UnitTests.Domain;

public class FriendRequestTest
{
	[Fact]
	public void Create_FriendRequest_Success()
	{
		//Arrange
		var senderIdentityGuid = 1;
		var receiverIdentityGuid = 2;
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		
		//Act
		var result = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
		
		//Assert
		result.Should().NotBeNull();
		result.IsEqualTo(senderIdentityGuid, receiverIdentityGuid).Should().BeTrue();
		result.SenderPersonId.Should().Be(senderIdentityGuid);
		result.ReceiverPersonId.Should().Be(receiverIdentityGuid);
		result.Modifier.Should().Be(senderIdentityGuid);
		result.CreatedDate.Should().Subject.Should().NotBeNull();
		result.ModifiedDate.Should().Subject.Should().NotBeNull();
	}
	
	[Fact]
	public void Create_FriendRequest_SenderGuid_Failure()
	{
		//Arrange
		var senderIdentityGuid = 0;
		var receiverIdentityGuid = 1;
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		//Act
		Action act = () =>
		{
			var unused = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
		}; 
		
		//Assert
		act.Should().Throw<PersonsDomainException>()
			.WithMessage(nameof(senderIdentityGuid));
	}
	
	[Fact]
	public void Create_FriendRequest_ReceiverGuid_Failure()
	{
		//Arrange
		var senderIdentityGuid = 1;
		var receiverIdentityGuid = 0;
		var friendRequestStatusId = FriendRequestStatus.AwaitingConfirmation.Id;
		
		//Act
		Action act = () =>
		{
			var unused = new FriendRequest(senderIdentityGuid, receiverIdentityGuid, friendRequestStatusId);
		}; 
		
		//Assert
		act.Should().Throw<PersonsDomainException>()
			.WithMessage(nameof(receiverIdentityGuid));
	}
}