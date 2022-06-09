namespace Web.MainApp.HttpAggregator.Dto;

public class CreatePersonApiRequest
{
	public string IdentityGuid { get; set; }
	public string Username { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string KnownAs { get; set; }
	public string Bio { get; set; }
}