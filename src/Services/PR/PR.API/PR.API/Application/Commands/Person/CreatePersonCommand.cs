using System.Runtime.Serialization;
using MediatR;

namespace PR.API.Application.Commands.Person;

public class CreatePersonCommand : IRequest<bool>
{
	[DataMember] public string PersonId { get; private set; }

	public CreatePersonCommand(string personId)
	{
		PersonId = personId;
	}
}