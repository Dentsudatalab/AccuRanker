namespace DentsuDataLab.AccuRanker.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;

    using Auth;

    using Extensions;

    using Http;

    public static class AccuRankerAuthenticationUtils
    {
        private const string AccuRankerAuthBaseUrl = "https://app.accuranker.com/oauth/authorize/";
        private const string AccuRankerCodeAuthBaseUrl = "https://app.accuranker.com/oauth/token/";

        public static string GetAuthenticationUrl(AuthValues authValues)
        {
            var url = new UriBuilder(AccuRankerAuthBaseUrl);
            var query = HttpUtility.ParseQueryString(url.Query);

            query["response_type"] = "code";
            query["client_id"] = authValues.ClientId;
            query["redirect_uri"] = authValues.RedirectUri;
            query["state"] = authValues.State;
            query["scope"] = "read";

            url.Query = query.ToString();

            return url.ToString();
        }

        public static async Task<ApiResponse<IdentityClient>> GetRefreshToken(AuthValues authValues)
        {
            var client = new HttpClient();

            var uriValues = new Dictionary<string, string>
            {
                {"grant_type", "authorization_code"},
                {"code", authValues.Code},
                {"client_id", authValues.ClientId},
                {"redirect_uri", authValues.RedirectUri}
            };

            var result = await client.PostFormResponse<IdentityClient>(AccuRankerCodeAuthBaseUrl, uriValues);

            return result;
        }

        public static async Task<ApiResponse<IdentityClient>> GetAccessToken(AuthValues authValues)
        {
            var client = new HttpClient();
            var requestParams = new Dictionary<string, string>
            {
                {"client_id", authValues.ClientId},
                {"refresh_token", authValues.RefreshToken},
                {"grant_type", "refresh_token"}
            };
            Console.WriteLine("Logging of getting accuranker access_token");

            var result = await client.PostFormResponse<IdentityClient>(AccuRankerCodeAuthBaseUrl, requestParams);

            return result;
        }
    }
}
