namespace scorecard_web.Models
{
    /// <summary>
    /// Class representing the elasic search response container
    /// </summary>
    public class ElasticSearchResponse
    {
        public SearchHits Hits { get; set; }
    }
}
