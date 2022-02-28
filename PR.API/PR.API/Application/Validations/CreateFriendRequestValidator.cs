using FluentValidation;
using Microsoft.Extensions.Logging;
using PR.API.Application.Commands;

namespace PR.API.Application.Validations;

public class CreateFriendRequestValidator : AbstractValidator<CreateFriendRequestCommand>
{
	public CreateFriendRequestValidator(ILogger<CreateFriendRequestValidator> logger)
	{
		RuleFor(createFriend => createFriend.SenderIndentityGuid).NotEmpty().WithMessage("No SenderId found");
		RuleFor(createFriend => createFriend.ReceiverIndentityGuid).NotEmpty().WithMessage("No ReceiverId found");

		logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
	}
}