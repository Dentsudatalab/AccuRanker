namespace DentsuDataLab.AccuRanker.Services.Endpoints
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Authorization;

    using Extensions;

    using Models.Accounts;

    using Utility;
    using Utility.Fields;
    using Utility.Http;

    public class AccountService : BaseAccuRankerService
    {
        public AccountService(HttpClient httpClient, AccuRankerAuthorizationService authService) : base(
            httpClient,
            authService)
        {
        }

        public async Task<IEnumerable<Account>> GetAccounts(int pageSize = 1000, params Field[] fields)
        {
            return await GetAccounts(fields, pageSize);
        }

        public async Task<IEnumerable<Account>> GetAccounts(IEnumerable<Field> fields, int pageSize = 1000)
        {
            await AuthorizeClient(AuthValues);

            var endpoint = new AccuRankerQueryBuilder("accounts/")
                .WithFields(fields);

            var accounts = await GetAllPages<Account>(endpoint, pageSize);

            return accounts;
        }

        public async Task<Account> GetAccount(int id)
        {
            await AuthorizeClient(AuthValues);

            var endpoint = new AccuRankerQueryBuilder($"accounts/{id}/")
                .Build();

            var groups = await HttpClient.GetApiResponse<Account>(endpoint);

            if (groups != null && groups.HasValue)
                return groups.Value;

            throw new ApiException(groups?.Error);
        }
    }
}
