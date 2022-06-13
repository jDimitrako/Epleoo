namespace Web.MainApp.HttpAggregator.Config;

public class UrlsConfig
{
    public static class PersonsOperations
    {
        public static string Base => "api/v1/persons/";
    }

    public static class PrOperations
    {
        public static string Base => "api/v1/friendrequests";
        public static string AcceptFriendRequest => Base + "/{0}/accept";
        public static string GetFriends => "api/v1/persons/{0}";
        

    }
    public string Persons { get; set; }
    public string Pr { get; set; }
    public string GrpcPersons { get; set; }
}
