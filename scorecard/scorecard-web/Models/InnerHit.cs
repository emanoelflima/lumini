using Newtonsoft.Json;

namespace scorecard_web.Models
{
    /// <summary>
    /// Class representing the hits.hits part of the elasticsearch response
    /// </summary>
    public class InnerHit
    {
        [JsonProperty(PropertyName = "_source")]
        public Source Source { get; set; }
    }
}