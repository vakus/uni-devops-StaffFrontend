﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.AuthorizationProxy
{
    public class AuthorizationProxyRemote : IAuthorizationProxy
    {
        private IHttpClientFactory _client { get; }
        private IConfigurationSection config { get; }
        public AuthorizationProxyRemote(IHttpClientFactory client, IConfiguration config)
        {
            this._client = client;
            this.config = config.GetSection("AuthorizationMicroservice");
        }

        public async Task<AuthorizationLoginResult> Login(string username, string password)
        {
            var client = _client.CreateClient();

            string domain = config.GetValue<string>("domain");

            var discoveryServer = await client.GetDiscoveryDocumentAsync(domain);
            var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryServer.TokenEndpoint,
                ClientId = config.GetValue<string>("clientid"),
                ClientSecret = config["secret"],
                UserName = username,
                Password = password
            });

            if (token.IsError)
            {
                throw new SystemException("Invalid username or password.");
            }

            var userInfo = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryServer.UserInfoEndpoint,
                Token = token.AccessToken
            });

            if (userInfo.IsError)
            {
                throw new SystemException("Invalid username or password.");
            }

            var claimsIdentity = new ClaimsIdentity(userInfo.Claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var tokensToStore = new AuthenticationToken[]
            {
                    new AuthenticationToken{Name = "access_token", Value=token.AccessToken}
            };

            var authProperties = new AuthenticationProperties();
            authProperties.StoreTokens(tokensToStore);

            return new AuthorizationLoginResult() { 
                claimsPrincipal = claimsPrincipal,
                authProperties = authProperties
            };
        }
    }
}