using Persons.Domain.SeedWork;

namespace Persons.Domain.AggregatesModel.PersonAggregate;

public class Person : Entity, IAggregateRoot
{
	public override int Id
	{
		get => _id;
	}

	private int _id;
	public string IdentityGuid { get; private set; }
	public string Username { get; }
	public string FirstName { get; }
	public string LastName { get; }
	public string KnownAs { get; }
	public string Bio { get; }

	public Person()
	{
	}

	public Person(string identityGuid, string username, string firstName, string lastName, string knownAs, string bio) : this()
	{
		IdentityGuid = identityGuid;
		Username = !string.IsNullOrEmpty(username) ? username : throw new ArgumentNullException(nameof(username));
		FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException(nameof(firstName));
		LastName = !string.IsNullOrEmpty(lastName) ? lastName : throw new ArgumentNullException(nameof(lastName));
		KnownAs = knownAs;
		Bio = bio;
	}
	
	public void SetIdentityGuid(string newIdentityGuid)
	{
		IdentityGuid = newIdentityGuid;
	}
}