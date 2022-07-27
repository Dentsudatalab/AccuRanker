namespace DentsuDataLab.AccuRanker.Services.Authorization
{
    using System;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using Utility;
    using Utility.Auth;
    using Utility.Http;

    public class AccuRankerAuthorizationService
    {
        private readonly AccuRankerClientStore _clientStore;

        public AccuRankerAuthorizationService(AccuRankerClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        /// <summary>
        /// Checks whether or not the client with the given refresh token is logged in.
        /// </summary>
        /// <param name="token">The refresh token to check.</param>
        /// <returns>A bool representing the logged in state.</returns>
        public virtual bool IsClientLoggedIn(string token)
        {
            return _clientStore.Client != null && _clientStore.Client.ExpiresAt > DateTime.Now &&
                   _clientStore.Client.RefreshToken == token;
        }

        /// <summary>
        /// Returns a copy of the current client.
        /// </summary>
        /// <param name="token">The refresh token of the client.</param>
        /// <returns>A copy of the <see cref="IdentityClient"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the client is not logged in.</exception>
        public virtual IdentityClient GetClient(string token)
        {
            if (!IsClientLoggedIn(token))
                throw new InvalidOperationException(nameof(IsClientLoggedIn));

            var serializedClient = JsonConvert.SerializeObject(_clientStore.Client);

            return JsonConvert.DeserializeObject<IdentityClient>(serializedClient);
        }

        /// <summary>
        /// Authorizes the client with the given <see cref="AuthValues"/>.
        /// </summary>
        /// <param name="authValues">The values to use for authorization.</param>
        public virtual async Task<IdentityClient> AuthorizeClient(AuthValues authValues)
        {
            if (!IsClientLoggedIn(authValues.RefreshToken))
            {
                var clientInfo = await GetClientInfo(authValues);

                if (clientInfo.HasValue)
                    _clientStore.Client = new IdentityClient
                    {
                        AccessToken = clientInfo.Value.AccessToken,
                        ExpiresIn = clientInfo.Value.ExpiresIn,
                        TokenType = clientInfo.Value.TokenType,
                        RefreshToken = clientInfo.Value.RefreshToken,
                        Scope = clientInfo.Value.Scope
                    };
                Console.WriteLine("Logging of setting accuranker client:" + clientInfo.Value.AccessToken);
            }


            return _clientStore.Client;
        }

        private async Task<ApiResponse<IdentityClient>> GetClientInfo(AuthValues authValues)
        {
            var clientResponse = await AccuRankerAuthenticationUtils.GetAccessToken(authValues);

            if (clientResponse.HasValue)
                clientResponse.Value.ExpiresIn = 3600;

            return clientResponse;
        }
    }
}
