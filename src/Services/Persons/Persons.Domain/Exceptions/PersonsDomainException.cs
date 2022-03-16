namespace Persons.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class PersonsDomainException : Exception
{
	public PersonsDomainException()
	{ }

	public PersonsDomainException(string message)
		: base(message)
	{ }

	public PersonsDomainException(string message, Exception innerException)
		: base(message, innerException)
	{ }
}