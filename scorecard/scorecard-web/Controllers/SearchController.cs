using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using scorecard_web.Models;
using scorecard_web.Models.Constants;
using scorecard_web.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace scorecard_web.Controllers
{
    /// <summary>
    /// Controller for Search-like routes
    /// </summary>
    public class SearchController : Controller
    {
        private IConfiguration _configuration;

        public SearchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Default method for main route
        /// </summary>
        /// <returns></returns>
        [HttpGet("search")]
        public IActionResult Index()
        {
            SearchModel model = new SearchModel();
            return View(model);
        }

        /// <summary>
        /// Retrieves data from elasticsearch server with given default operator and query
        /// </summary>
        /// <param name="defaultOperator"></param>
        /// <param name="query">String with comma-separated list of key:value search terms</param>
        /// <returns></returns>
        [HttpGet("search/{defaultOperator}/{query}")]
        public IActionResult Index(string defaultOperator, string query)
        {
            List<QueryTerm> queryTerms = QueryUtils.GetQueryTerms(query);

            using (var client = new HttpClient())
            {
                query = query.Replace(CommonConstants.SEPARATOR_COMMA, CommonConstants.SEPARATOR_PLUS);

                string url = _configuration.GetSection("ElasticsearchScorecardIndexRoute").Value
                    + string.Format(CommonConstants.ROUTE_SCORECARD_SEARCH, defaultOperator, query);

                var uri = new Uri(url);

                var result = client.GetAsync(uri).GetAwaiter().GetResult();
                string data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var elasticResponse = JsonConvert.DeserializeObject<ElasticSearchResponse>(data);

                List<Source> searchResults = new List<Source>();

                foreach (InnerHit hit in elasticResponse.Hits.Hits)
                {
                    searchResults.Add(hit.Source);
                }

                RegisterSearch(queryTerms);

                SearchModel model = new SearchModel(searchResults, queryTerms, defaultOperator);

                return View(model);
            }
        }

        /// <summary>
        /// Error page view
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void RegisterSearch(List<QueryTerm> queryTerms)
        {
            using (var client = new HttpClient())
            {
                string url = _configuration.GetSection("LogstashScorecardSearchRoute").Value;

                var uri = new Uri(url);

                var content = new StringContent(
                    JsonConvert.SerializeObject(queryTerms),
                    Encoding.UTF8,
                    "application/json"
                );

                var result = client.PutAsync(uri, content).GetAwaiter().GetResult();
                string data = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
        }
    }
}
