using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AVIdentityServer2
{
    public class Configuration
    { //declare clients which will use this authentification
        internal class Clients
        {
            public static IEnumerable<Client> Get()
            {
                return new List<Client> {
                    //av client app to test
                     new Client
                    {
                         ClientId = "av_client_app",
                         ClientName = "AVClientApplication",

                         AllowedGrantTypes = GrantTypes.Implicit,

                         //automatically created and handled by an upcoming piece of middleware
                         RedirectUris = { "http://localhost:5011/signin-oidc" },
                         PostLogoutRedirectUris = { "http://localhost:5011/api/values" },

                         AllowedScopes =
                         {
                             IdentityServerConstants.StandardScopes.OpenId,
                             IdentityServerConstants.StandardScopes.Profile,
                             IdentityServerConstants.StandardScopes.Email,
                             "av_api.read",
                             "av_api.write",
                             "role"
                         }
                     }
                };
            }
        }

        //declare resources
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        //declare api
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource{
                    Name ="av_api",
                    DisplayName="AVApi",
                    UserClaims =new List<string> {"role"},
                    Scopes=new List<Scope>
                    {
                         new Scope("av_api.read"),
                         new Scope("av_api.write")
                    }
                }
            };
        }

        //declare users
        internal class Users
        {
            public static List<TestUser> Get()
            {
                return new List<TestUser> {
                  new TestUser {
                        //list of branches?
                        //db name
                        //server name 
                        //dealer id 
                     SubjectId = "5BE86359-073C-434B-AD2D-A3932222DABE",
                     Username = "alexandra",
                     Password = "password",
                     Claims = new List<Claim>
                     {
                         //dealer id 
                        new Claim(JwtClaimTypes.Name, "Alexandra D"),
                        new Claim(JwtClaimTypes.GivenName, "Alexandra"),
                        new Claim(JwtClaimTypes.FamilyName, "D"),
                        new Claim(JwtClaimTypes.Email, "alexandra@email.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "Admin"),
                        new Claim(JwtClaimTypes.Role, "Geek"),
                        new Claim(JwtClaimTypes.WebSite, "http://alexandra.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }", IdentityServerConstants.ClaimValueTypes.Json)
                     }
                  },
                   new TestUser { 
                     SubjectId = "7B586359",
                     Username = "john",
                     Password = "password",
                     Claims = new List<Claim>
                     {
                        //as claims
                        //Branch= 45121,
                        //DbName= "conn name", 
                        //ServerName "dealer1"
                        //DealerId= 124,
                        new Claim(JwtClaimTypes.Name, "John Smith"),
                        new Claim(JwtClaimTypes.GivenName, "John"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.Email, "john@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.Role, "User"),
                        new Claim(JwtClaimTypes.WebSite, "http://john.com"),
                        new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One ', 'locality': 'Heig', 'postal_code': 69778, 'country': 'Ireland' }", IdentityServerConstants.ClaimValueTypes.Json)
                     }
                   }
                };
            }
        }
    }
}

