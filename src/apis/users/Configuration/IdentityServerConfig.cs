﻿using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace CCubAPI.Configuration
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api.read"),
                new ApiScope("api.write"),
            };


        public static IEnumerable<Client> Clients =>
    new Client[]
    {
        new Client
        {
            ClientId = "client123",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("secret@123".Sha256()) },
            AllowedScopes = { "api.read" }
        },
    };

        //public static IEnumerable<Client> GetClients()
        //{
        //    return new List<Client>
        //{
        //    new Client
        //    {
        //        ClientId = "mytestid",
        //        ClientSecrets = { new Secret("secret@123".Sha256()) },
        //        AllowedGrantTypes = GrantTypes.ClientCredentials,
        //        AllowedScopes = { "api.read" }
        //    }
        //};
        //}

        public static IEnumerable<ApiResource> ApiResources =>
    new ApiResource[]
    {
        new ApiResource("myApi")
        {
            Scopes = new List<string>{ "Api.read","Api.write" },
            ApiSecrets = new List<Secret>{ new Secret("secret".Sha256()) }
        }
    };

        public static List<TestUser> TestUsers =>
  new List<TestUser>
  {
        new TestUser
        {
            SubjectId = "123",
            Username = "Gowtham",
            Password = "Test@123",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Gowtham K"),
                new Claim(JwtClaimTypes.GivenName, "Gowtham"),
                new Claim(JwtClaimTypes.FamilyName, "Kumar"),
              //  new Claim(JwtClaimTypes.WebSite, "https://gowthamcbe.com/"),
            }
        }
    };
    }
}
