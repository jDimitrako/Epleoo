using System;
using System.Linq;
using System.Threading.Tasks;
using PR.Domain.AggregatesModel.PersonAggregate;

namespace PR.API.Application.Queries.Persons;

public class PersonsQueries : IPersonsQueries
{
	private readonly IPersonRepository _repository;

	public PersonsQueries(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<PersonRequestResponse> GetPerson(string personIdentityGuid)
	{
		try
		{
			var person = await _repository.FindWithFriendShipsNoTrackingAsync(personIdentityGuid);

			if (person == null) return null;

			return new PersonRequestResponse(person.Id, person.IdentityGuid,
				person.FriendshipsSent.Select(f =>
						new FriendResponse(f.ReceiverId, _repository.FindByIdAsync(f.ReceiverId).Result!.IdentityGuid))
					.ToList(),
				person.FriendshipsReceived.Select(f =>
					new FriendResponse(f.SenderId,
						_repository.FindByIdAsync(f.SenderId).Result!.IdentityGuid)).ToList());
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}