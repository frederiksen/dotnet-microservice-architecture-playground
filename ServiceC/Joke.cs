using System.Text.Json.Serialization;

namespace ServiceC
{
    class JokeValue
    {
        [JsonPropertyNameAttribute("id")]
        public int Id { get; set; }
        [JsonPropertyNameAttribute("joke")]
        public string Joke { get; set; }
    }

    class Joke
    {
        [JsonPropertyNameAttribute("type")]
        public string Type { get; set; }
        [JsonPropertyNameAttribute("value")]
        public JokeValue Value { get; set; }
    }
}
