using Newtonsoft.Json;

namespace usipav_cockpit.Helpers
{
    public class HttpResponse
    {
        [JsonProperty("content", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? Content { get; set; }

        [JsonProperty("message", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Message { get; set; }
    }
}
