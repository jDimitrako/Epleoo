namespace Web.MainApp.HttpAggregator.Config;

public class UrlsConfig
{
    public static class PersonsOperations
    {
        public static string PersonBase() => "api/v1/persons/";
    }


    public string Persons { get; set; }
    public string Pr { get; set; }
    public string GrpcPersons { get; set; }
}
