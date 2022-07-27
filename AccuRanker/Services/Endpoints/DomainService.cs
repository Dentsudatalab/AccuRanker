namespace DentsuDataLab.AccuRanker.Services.Endpoints
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Authorization;

    using Extensions;

    using Models.Domains;

    using Utility;
    using Utility.Fields;
    using Utility.Http;

    public class DomainService : BaseAccuRankerService
    {
        public DomainService(HttpClient httpClient, AccuRankerAuthorizationService authService) : base(
            httpClient,
            authService)
        {
        }

        public virtual async Task<IEnumerable<Domain>> GetDomains(int pageSize = 1000, params Field[] fields)
        {
            return await GetDomains(fields, pageSize);
        }

        public virtual async Task<IEnumerable<Domain>> GetDomains(IEnumerable<Field> fields, int pageSize = 1000)
        {
            await AuthorizeClient(AuthValues);

            var endpoint = new AccuRankerQueryBuilder("domains/")
                .WithFields(fields);

            var domains = await GetAllPages<Domain>(endpoint, pageSize);

            return domains;
        }

        public async Task<Domain> GetDomain(long id, params Field[] fields)
        {
            return await GetDomain(id, (IEnumerable<Field>)fields);
        }

        public async Task<Domain> GetDomain(long id, IEnumerable<Field> fields)
        {
            await AuthorizeClient(AuthValues);

            var endpoint = new AccuRankerQueryBuilder($"domains/{id}/")
                .WithFields(fields)
                .Build();
            Console.WriteLine("Logging of endpoint:" + endpoint);

            var domain = await HttpClient.GetApiResponse<Domain>(endpoint);

            if (domain != null && domain.HasValue)
                return domain.Value;

            throw new ApiException(domain?.Error);
        }
    }
}
