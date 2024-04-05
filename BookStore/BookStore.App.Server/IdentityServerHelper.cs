using BookStore.App.Server;
using IdentityModel.Client;

public static class IdentityServerHelper
{
    public static async Task<TokenResponse> GetAccessToken()
    {
        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync(Configs.IdentityApi);
        var tokenResponse1 = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "client",
            ClientSecret = "secret",
            Scope = "productApi"
        });

        if (tokenResponse1.IsError)
        {
            throw new InvalidOperationException();
        }

        return tokenResponse1;
    }
}