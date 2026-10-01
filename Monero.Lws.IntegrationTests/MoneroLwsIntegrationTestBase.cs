using Monero.Lws.IntegrationTests.Utils;

namespace Monero.Lws.IntegrationTests;

public abstract class MoneroLwsIntegrationTestBase
{
    protected static readonly List<WalletInfo> Wallets;

    protected static readonly MoneroLwsService Lws;

    static MoneroLwsIntegrationTestBase()
    {
        Lws = TestUtils.GetLwsService();
        Wallets = TestUtils.Config.Wallets;
    }
}