namespace Web.MainApp.HttpAggregator.Dto.Persons;

public class CreatePersonRequest
{
	public string Username { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string KnownAs { get; set; }
	public string Bio { get; set; }
}