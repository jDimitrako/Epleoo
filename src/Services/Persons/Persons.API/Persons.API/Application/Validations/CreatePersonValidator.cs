using FluentValidation;
using Microsoft.Extensions.Logging;
using Persons.API.Application.Commands.Persons;

namespace Persons.API.Application.Validations;

public class CreatePersonValidator : AbstractValidator<CreatePersonCommand>
{
	public CreatePersonValidator(ILogger<CreatePersonValidator> logger)
	{
		RuleFor(createPerson => createPerson.Username).NotEmpty().WithMessage("No Username found");
		RuleFor(createPerson => createPerson.FirstName).NotEmpty().WithMessage("No FirstName found");
		RuleFor(createPerson => createPerson.LastName).NotEmpty().WithMessage("No LastName found");

		logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
	}
}