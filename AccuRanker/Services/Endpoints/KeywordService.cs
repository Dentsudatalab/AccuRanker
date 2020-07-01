namespace DentsuDataLab.AccuRanker.Services.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Authorization;

    using Models.Domains;
    using Models.Keywords;

    using Utility;
    using Utility.Fields;

    public class KeywordService : BaseAccuRankerService
    {
        public KeywordService(HttpClient httpClient, AccuRankerAuthorizationService authService) : base(
            httpClient,
            authService)
        {
        }

        public async Task<IEnumerable<Keyword>> GetKeywords(
            Domain domain,
            DateTime from,
            DateTime to,
            int pageSize = 1000,
            params Field[] fields)
        {
            return await GetKeywords(domain, from, to, fields, pageSize);
        }

        public async Task<IEnumerable<Keyword>> GetKeywords(
            Domain domain,
            DateTime from,
            DateTime to,
            IEnumerable<Field> fields,
            int pageSize = 1000)

        {
            return await GetKeywords(domain.Id, from, to, fields, pageSize);
        }

        public virtual async Task<IEnumerable<Keyword>> GetKeywords(
            long domainId,
            DateTime from,
            DateTime to,
            IEnumerable<Field> fields,
            int pageSize = 1000)
        {
            await AuthorizeClient(AuthValues);

            var endpoint = new AccuRankerQueryBuilder($"domains/{domainId}/keywords/")
                .WithFields(fields)
                .WithDateRange(from, to);

            var keywords = await GetAllPages<Keyword>(endpoint, pageSize);

            return keywords;
        }
    }
}