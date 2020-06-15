using scorecard_web.Models;
using scorecard_web.Models.Constants;
using System.Collections.Generic;

namespace scorecard_web.Utils
{
    /// <summary>
    /// Utility for query data manipulation
    /// </summary>
    public static class QueryUtils
    {
        /// <summary>
        /// Returns a structured list of a query string in comma-separated key:value string
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<QueryTerm> GetQueryTerms(string query)
        {
            var queryTerms = new List<QueryTerm>();
            string[] strippedQuery = query.Split(CommonConstants.SEPARATOR_COMMA);
            foreach (string q in strippedQuery)
            {
                string[] term = q.Split(CommonConstants.SEPARATOR_DOUBLE_DOTS);
                QueryTerm queryTerm = new QueryTerm(term[0], term[1]);
                queryTerms.Add(queryTerm);
            }
            return queryTerms;
        }
    }
}
