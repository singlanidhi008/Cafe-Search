namespace CafeSearchBackEnd.Models
{
    public class PlaceInfo
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Type { get; set; }
    }

    public class PlaceResult
    {
        public string? Name { get; set; }
        public string? FormattedAddress { get; set; }
        public List<string>? Types { get; set; }
    }

    public class GooglePlacesApiResponse
    {
        public List<PlaceResult>? Results { get; set; }
    }

}
