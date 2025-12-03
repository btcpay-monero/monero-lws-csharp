using Monero.Lws.Common;
using Monero.Lws.IntegrationTests.Utils;

namespace Monero.Lws.IntegrationTests;

public class MoneroLwsServiceIntegrationTest
{
    private static readonly string Address = TestUtils.Address;
    private static readonly string ViewKey = TestUtils.ViewKey;
    private static readonly MoneroLwsService Lws = TestUtils.GetLwsService();

    [Fact]
    public async Task TestGetVersion()
    {
        var response = await Lws.GetVersion();
        Assert.NotNull(response.ServerType);
        Assert.NotNull(response.ServerVersion);
        Assert.NotNull(response.LastGitCommitHash);
        Assert.NotNull(response.LastGitCommitDate);
        Assert.NotNull(response.GitBranchName);
        Assert.NotNull(response.NetworkType);
        Assert.NotEmpty(response.ServerType);
        Assert.NotEmpty(response.ServerVersion);
        Assert.NotEmpty(response.LastGitCommitHash);
        Assert.NotEmpty(response.LastGitCommitDate);
        Assert.NotEmpty(response.GitBranchName);
        Assert.NotEmpty(response.NetworkType);
        Assert.True(response.BlockchainHeight > 0);
        Assert.True(response.Api > 0);
        Assert.True(response.MaxSubaddresses > 0);
        Assert.False(response.Testnet);
    }
    
    [Fact]
    public async Task TestLogin()
    {
        var response = await Lws.Login(Address, ViewKey, true, false);
        Assert.True(response.StartHeight > 0);
        Assert.True(response.NewAddress);
        Assert.False(response.GeneratedLocally);
    }

    [Fact]
    public async Task TestGetAddressInfo()
    {
        var response = await Lws.GetAddressInfo(Address, ViewKey);
        Assert.True(response.StartHeight > 0);
        Assert.True(response.BlockchainHeight > 0);
        Assert.True(response.ScannedBlockHeight > 0);
        Assert.True(response.ScannedHeight > 0);
        Assert.Equal(0, response.TransactionHeight);
        Assert.Equal("0", response.LockedFunds);
        Assert.Equal("0", response.TotalReceived);
        Assert.Equal("0", response.TotalSent);
        Assert.Null(response.Rates);
    }

    [Fact]
    public async Task TestGetAddressTxs()
    {
        var response = await Lws.GetAddressTxs(Address, ViewKey);
        Assert.NotNull(response.TotalReceived);
        Assert.NotEmpty(response.TotalReceived);
        Assert.True(response.ScannedHeight > 0);
        Assert.True(response.ScannedBlockHeight > 0);
        Assert.True(response.StartHeight > 0);
        Assert.True(response.BlockchainHeight > 0);

        foreach (var tx in response.Transactions)
        {
            TestTransaction(tx);
        }
    }

    [Fact]
    public async Task TestGetRandomOuts()
    {
        var response = await Lws.GetRandomOuts(15, ["100000", "100000"]);
        Assert.NotEmpty(response.AmountOuts);
        foreach (var randomOutput in response.AmountOuts)
        {
            TestRandomOutput(randomOutput);
        }
    }

    [Fact]
    public async Task TestGetUnspentOuts()
    {
        var response = await Lws.GetUnspentOuts(Address, ViewKey, "0", 0, true);
        Assert.NotNull(response.PerByteFee);
        Assert.NotNull(response.Amount);
        Assert.NotNull(response.FeeMask);
        Assert.NotEmpty(response.PerByteFee);
        Assert.NotEmpty(response.Amount);
        Assert.NotEmpty(response.FeeMask);
        foreach (var output in response.Outputs)
        {
            TestOutput(output);
        }
    }

    [Fact]
    public async Task TestImportWallet()
    {
        var response = await Lws.ImportWallet(Address, ViewKey, 0);
        Assert.Null(response.PaymentAddress);
        Assert.Null(response.PaymentId);
        Assert.Null(response.ImportFee);
        Assert.True(response.NewRequest);
        Assert.False(response.RequestFulfilled);
        Assert.NotNull(response.Status);
        Assert.NotEmpty(response.Status);
    }

    [Fact]
    public async Task TestGetSubaddrs()
    {
        var response = await Lws.GetSubaddrs(Address, ViewKey);
        Assert.Null(response.NewSubaddrs);
        Assert.NotNull(response.AllSubaddrs);
        Assert.NotEmpty(response.AllSubaddrs);
        foreach (var entry in response.AllSubaddrs)
        {
            TestSubaddrsEntry(entry);
        }
    }

    [Fact]
    public async Task TestUpsertSubaddrs()
    {
        List<MoneroLwsSubaddrsEntry> subaddrs = [];
        var entry = new MoneroLwsSubaddrsEntry()
        {
            AccountIndex = 1,
        };
        entry.Ranges.Add(new MoneroLwsAddressIndexRange(0, 10));
        var response = await Lws.UpsertSubaddrs(Address, ViewKey, subaddrs, true);
        TestSubaddrs(response, true, true);
    }

    [Fact]
    public async Task TestProvisionSubaddrs()
    {
        var response = await Lws.ProvisionSubaddrs(Address, ViewKey, 0, 20, 1, 1, true);
        TestSubaddrs(response, true, true);
    }
    
    private static void TestTransaction(MoneroLwsTransaction? tx)
    {
        throw new NotImplementedException("TestTransaction(): not implemented");
    }

    private static void TestRandomOutput(MoneroLwsRandomOutput? output)
    {
        throw new NotImplementedException("TestRandomOutput(): not implemented");
    }

    private static void TestOutput(MoneroLwsOutput? output)
    {
        throw new NotImplementedException("TestOutput(): not implemented");
    }

    private static void TestSubaddrs(MoneroLwsSubaddrs? subaddrs, bool expectNew, bool expectAll)
    {
        if (subaddrs == null)
        {
            throw new Exception("Subaddrs is null");
        }

        if (expectNew)
        {
            Assert.NotNull(subaddrs.NewSubaddrs);
            Assert.NotEmpty(subaddrs.NewSubaddrs);
            TestSubaddrsEntries(subaddrs.NewSubaddrs);
        }

        if (expectAll)
        {
            Assert.NotNull(subaddrs.AllSubaddrs);
            Assert.NotEmpty(subaddrs.AllSubaddrs);
            TestSubaddrsEntries(subaddrs.AllSubaddrs);   
        }
    }

    private static void TestSubaddrsEntries(List<MoneroLwsSubaddrsEntry>? entries)
    {
        if (entries == null)
        {
            throw new Exception("Subaddrs entries are null");
        }

        foreach (var entry in entries)
        {
            TestSubaddrsEntry(entry);
        }
    }
    
    private static void TestSubaddrsEntry(MoneroLwsSubaddrsEntry? entry)
    {
        throw new NotImplementedException("TestSubaddrsEntry(): not implemented");
    }
    
}