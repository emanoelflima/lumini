using Newtonsoft.Json;

namespace scorecard_web.Models
{
    /// <summary>
    /// Class representing the query terms of a search
    /// </summary>
    public class QueryTerm
    {
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        public QueryTerm()
        {
            //default constructor
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public QueryTerm(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
