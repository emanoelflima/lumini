using scorecard_web.Models.Enums;
using System;
using System.Collections.Generic;

namespace scorecard_web.Models
{
    /// <summary>
    /// Class representing the model of the Search page
    /// </summary>
    public class SearchModel
    {
        public List<Source> SearchResults { get; set; }
        public List<QueryTerm> QueryTerms { get; set; }
        public SearchOperatorEnum DefaultOperator { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SearchModel()
        {
            QueryTerms = new List<QueryTerm>()
            {
                new QueryTerm()
            };
            DefaultOperator = SearchOperatorEnum.AND;
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="searchResults"></param>
        /// <param name="queryTerms"></param>
        /// <param name="defaultOperator"></param>
        public SearchModel(List<Source> searchResults, List<QueryTerm> queryTerms, string defaultOperator)
        {
            SearchResults = searchResults;
            QueryTerms = queryTerms;
            DefaultOperator = Enum.Parse<SearchOperatorEnum>(defaultOperator);
        }
    }
}
