using Monero.Lws.IntegrationTests.Utils;

using Xunit;

namespace Monero.Lws.IntegrationTests;

public class LwsAccountsFixture : IAsyncLifetime
{
    public async ValueTask InitializeAsync()
    {
        var lws = TestUtils.GetLwsService();

        foreach (var wallet in TestUtils.Config.Wallets)
        {
            var response = await lws.Login(wallet.PrimaryAddress, wallet.PrivateViewKey, true, true);

            if (response.NewAddress)
            {
                Assert.True(response.GeneratedLocally);
            }
        }
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}