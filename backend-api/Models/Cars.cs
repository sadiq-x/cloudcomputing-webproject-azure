using System.Text.Json.Serialization;

namespace backend_api.Models
{
    public class CarsModels
    {
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("mark")]
    public string Mark { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("km")]
    public string Km { get; set; }

    [JsonPropertyName("year")]
    public string Year { get; set; }

    [JsonPropertyName("color")]
    public string Color { get; set; }
    }
    
}