namespace Microsoft.eShopOnContainers.Services.Ordering.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class PRDomainException : Exception
{
	public PRDomainException()
	{ }

	public PRDomainException(string message)
		: base(message)
	{ }

	public PRDomainException(string message, Exception innerException)
		: base(message, innerException)
	{ }
}