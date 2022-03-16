using FluentValidation;
using Microsoft.Extensions.Logging;
using Persons.API.Application.Commands.Persons;

namespace Persons.API.Application.Validations;

public class CreatePersonRequestValidator : AbstractValidator<CreatePersonRequestCommand>
{
	public CreatePersonRequestValidator(ILogger<CreatePersonRequestValidator> logger)
	{
		RuleFor(createPerson => createPerson.IdentityGuid).NotEmpty().WithMessage("No IdentityGuid found");
		RuleFor(createPerson => createPerson.Username).NotEmpty().WithMessage("No Username found");
		RuleFor(createPerson => createPerson.FirstName).NotEmpty().WithMessage("No FirstName found");
		RuleFor(createPerson => createPerson.LastName).NotEmpty().WithMessage("No LastName found");

		logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
	}
}