using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CourseMarket.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
    [
        new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
        new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
        new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
        new ApiResource("resource_discount"){Scopes={"discount_fullpermission"}},
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    ];

    public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(){ Name = "roles", DisplayName = "Roles", Description = "User roles", UserClaims = new []{"role"}}
    };



    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new ApiScope("catalog_fullpermission", "Full permission for Catalog API"),
        new ApiScope("photo_stock_fullpermission", "Full permission for Photo Stock API"),
        new ApiScope("basket_fullpermission", "Full permission for Basket API"),
        new ApiScope("discount_fullpermission", "Full permission for Discount API"),
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
        },
        new Client
        {
            ClientName = "Asp.Net Core MVC",
            ClientId = "WebMvcClientForUser",
            AllowOfflineAccess = true,
            ClientSecrets = { new Secret("secret".Sha256())},
            AllowedGrantTypes = {GrantType.ResourceOwnerPassword},
            AllowedScopes = {
                "basket_fullpermission",
                "discount_fullpermission",
                IdentityServerConstants.StandardScopes.Email, 
                IdentityServerConstants.StandardScopes.OpenId, 
                IdentityServerConstants.StandardScopes.Profile, 
                IdentityServerConstants.StandardScopes.OfflineAccess, 
                IdentityServerConstants.LocalApi.ScopeName, "roles" 
            },
            AccessTokenLifetime = 1 * 60 * 60,
            RefreshTokenExpiration = TokenExpiration.Absolute,
            AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
            RefreshTokenUsage = TokenUsage.ReUse,
        }
    ];
}
