using System.Collections.Generic;

namespace scorecard_web.Models
{
    /// <summary>
    /// Class representing the "hits" part of the elasticsearch response 
    /// </summary>
    public class SearchHits
    {
        public List<InnerHit> Hits { get; set; }
    }
}