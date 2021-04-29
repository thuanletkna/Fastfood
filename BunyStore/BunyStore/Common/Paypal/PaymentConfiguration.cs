using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyStore.Common.Paypal
{
    public class PaymentConfiguration
    {
        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PaymentConfiguration()
        {
            //var config = GetConfig();
            ClientId = "AVJiDZV4wWw5jVQBFDOB-VjD1YNPMKAuaX-6oBAIZsmpw1d4fUkjFX2TBnZXFjObPB6JzLrxZhk43mBi";
            ClientSecret = "EO-0Km7KvzMv48dQNrxu2le2XD6FNMDG3FBi1zZKQdRwXEIpgQWCDESg3GlmloFKqrM9QX3vLr_nMjvP";
        }

        // getting properties from the web.config
        public static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            // getting accesstocken from paypal                
            string accessToken = new OAuthTokenCredential
        (ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            // return apicontext object by invoking it with the accesstoken
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }
    }
}