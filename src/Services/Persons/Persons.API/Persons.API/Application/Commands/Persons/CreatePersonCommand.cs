﻿using System;
using System.Runtime.Serialization;
using MediatR;

namespace Persons.API.Application.Commands.Persons;

public class CreatePersonCommand : IRequest<bool>
{
	[DataMember] public string IdentityGuid { get; }
	[DataMember] public string Username { get; }
	[DataMember] public string FirstName { get; }
	[DataMember] public string LastName { get; }
	[DataMember] public string KnownAs { get; }
	[DataMember] public string Bio { get; }

	public CreatePersonCommand(string identityGuid, string username, string firstName, string lastName, string knownAs, string bio)
	{
		IdentityGuid = identityGuid;
		Username = username;
		FirstName = firstName;
		LastName = lastName;
		KnownAs = knownAs;
		Bio = bio;
	}
}