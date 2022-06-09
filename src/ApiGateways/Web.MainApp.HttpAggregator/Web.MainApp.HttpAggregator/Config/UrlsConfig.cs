namespace Web.MainApp.HttpAggregator.Config;

public class UrlsConfig
{
    public class PersonsOperations
    {
        public static string CreatePerson() => "api/v1/persons/";
    }


    public string Persons { get; set; }
    public string Pr { get; set; }
    public string GrpcPersons { get; set; }
}
