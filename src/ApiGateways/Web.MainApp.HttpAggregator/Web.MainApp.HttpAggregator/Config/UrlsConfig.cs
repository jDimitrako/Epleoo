namespace Web.MainApp.HttpAggregator.Config;

public class UrlsConfig
{
    public static class PersonsOperations
    {
        public static string Base => "api/v1/persons/";
        public static string AcceptFriendRequest => "api/v1/friendrequests/{0}/accept";
    }

    public static class PrOperations
    {
        public static string Base => "api/v1/friendrequests/";
    }
    public string Persons { get; set; }
    public string Pr { get; set; }
    public string GrpcPersons { get; set; }
}
