namespace Web.MainApp.HttpAggregator.Config;

public class UrlsConfig
{
    public class PersonsOperations
    {
        public static string GetPersons() => "/api/v1/persons/";
    }


    public string Persons { get; set; }


    public string GrpcPersons { get; set; }
}
