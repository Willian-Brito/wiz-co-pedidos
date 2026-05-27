using System.Net.Http.Headers;

namespace WizCo.Tests.IntegrationTest.Auth;

public static class AuthHelper
{
    public static void Authenticate(HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                "TOKEN_FIXO_AQUI"
            );
    }
}