using System;

namespace Persons.API.Application.Queries;

public class PersonViewModel
{
	public record Person
	{
		public int Id { get; set; }
		public string IdentityGuid { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string KnownAs { get; set; }
		public string Bio { get; set; }
	} 
}