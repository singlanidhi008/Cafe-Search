using System.Net.Http;
using System.Threading.Tasks;
using CafeSearchBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("api/places")]
public class PlacesController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PlacesController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("{placeName}")]
    public async Task<IActionResult> GetCafes(string placeName)
    {
        // Replace 'YOUR_API_KEY' with your actual Google Places API key
        string apiKey = "AIzaSyARpaXcgK8cDfevIQJAoxW29SvHEaQFtr8";
        string apiUrl = $"https://maps.googleapis.com/maps/api/place/textsearch/json?key={apiKey}&query={placeName}&type=cafe";

        var httpClient = _httpClientFactory.CreateClient();

        // Make a request to the Google Places API
        var response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GooglePlacesApiResponse>(content);

            var cafes = result.Results
                .Where(r => r.Types.Contains("cafe"))
                .Select(r => new PlaceInfo
                {
                    Name = r.Name,
                    Address = r.FormattedAddress,
                    Type = "cafe"
                })
                .ToList();

            return Ok(cafes);

        }

        return BadRequest("Error fetching cafes from the API");
    }
}
