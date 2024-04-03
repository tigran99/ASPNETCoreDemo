using System.Text.Json;
using System.Text.Json.Nodes;

namespace IdentityServer.API.Models
{
    public class TokenGenerationRequest
    {
        public string Email { get; set; }

        public int UserId { get; set; }

        public Dictionary<string, JsonElement> CustomClaims { get; set; }
    }

    public class CustomClaim
    {
        public bool Admin { get; set; }
    }
}
