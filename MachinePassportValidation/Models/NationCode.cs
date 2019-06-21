using Newtonsoft.Json;

namespace PassportValidation.Models
{
    public class NationCode
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("alpha3")]
        public string Alpha3Code { get; set; }
    }
}