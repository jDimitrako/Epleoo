using System.Text.Json.Serialization;

namespace Web.MainApp.HttpAggregator.Dto.Persons;

public class PersonData
{
	[JsonPropertyName("id")]
	public int Id { get; set; }
	[JsonPropertyName("identityGuid")]
	public string IdentityGuid { get; set; }
	[JsonPropertyName("username")]
	public string Username { get; set; }
	[JsonPropertyName("firstName")]
	public string FirstName { get; set; }
	[JsonPropertyName("lastName")]
	public string LastName { get; set; }
	[JsonPropertyName("knownAs")]
	public string KnownAs { get; set; }
	[JsonPropertyName("bio")]
	public string Bio { get; set; }
}