using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CourseMarket.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
        new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    ];

    public static IEnumerable<IdentityResource> IdentityResources =>
    [];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("catalog_fullpermission", "Full permission for Catalog API"),
        new ApiScope("photo_stock_fullpermission", "Full permission for Photo Stock API"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    ];

    public static IEnumerable<Client> Clients =>
    [
        new Client
        {
            ClientName = "Asp.Net Core MVC", 
            ClientId = "WebMvcClient", 
            ClientSecrets = { new Secret("secret".Sha256())}, 
            AllowedGrantTypes = {GrantType.ClientCredentials}, 
            AllowedScopes = { "catalog_fullpermission", "photo_stock_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
        }
    ];
}
