using System.Text.Json.Serialization;

namespace ASPNETCoreDemo.Models
{
    public class ValueModel
    {
        [JsonPropertyName("FullName")]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
