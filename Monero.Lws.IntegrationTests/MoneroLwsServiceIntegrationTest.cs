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
        if (response.NewAddress)
        {
            Assert.NotNull(response.GeneratedLocally);
            if (response.StartHeight != null)
            {
                Assert.True(response.StartHeight > 0);
            }
        }
        else
        {
            Assert.Null(response.StartHeight);
        }
    }
    
    [Fact]
    public async Task TestGetAddressInfo()
    {
        var response = await Lws.GetAddressInfo(Address, ViewKey);
        Assert.True(response.StartHeight >= 0);
        Assert.True(response.BlockchainHeight > 0);
        Assert.True(response.ScannedBlockHeight > 0);
        Assert.True(response.ScannedHeight > 0);
        Assert.True(response.TransactionHeight > 0);
        Assert.False(string.IsNullOrEmpty(response.LockedFunds));
        Assert.False(string.IsNullOrEmpty(response.TotalReceived));
        Assert.False(string.IsNullOrEmpty(response.TotalSent));
        
        if (response.TotalSent != "0")
        {
            Assert.NotNull(response.SpentOutputs);
            Assert.NotEmpty(response.SpentOutputs);
            TestSpends(response.SpentOutputs);
        }
        
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
        if (!response.TotalReceived.Equals("0"))
        {
            Assert.NotNull(response.Transactions);
            Assert.NotEmpty(response.Transactions);
            foreach (var tx in response.Transactions)
            {
                TestTransaction(tx);
            }   
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
        Assert.True(response.PerByteFee > 0);
        Assert.True(response.FeeMask > 0);
        Assert.False(string.IsNullOrEmpty(response.Amount));
        Assert.Equal(4, response.Fees.Count);
        long lastFee = 0;
        foreach (var fee in response.Fees)
        {
            Assert.True(fee > lastFee);
            lastFee = fee;
        }
        
        foreach (var output in response.Outputs)
        {
            TestOutput(output);
        }
    }

    [Fact]
    public async Task TestImportWallet()
    {
        var response = await Lws.ImportWallet(Address, ViewKey, 0);
        if (string.IsNullOrEmpty(response.ImportFee) || response.ImportFee.Equals("0"))
        {
            Assert.True(string.IsNullOrEmpty(response.PaymentAddress));
            Assert.True(string.IsNullOrEmpty(response.PaymentId));
        }
        else
        {
            Assert.False(string.IsNullOrEmpty(response.PaymentAddress));
            Assert.False(string.IsNullOrEmpty(response.PaymentId));
        }
        
        Assert.NotNull(response.Status);

        if (response.NewRequest)
        {
            Assert.False(response.RequestFulfilled);
            Assert.Equal("Accepted, waiting for approval", response.Status);
        }

        if (response.RequestFulfilled)
        {
            Assert.NotNull(response.Status);
        }
        else if (!response.NewRequest)
        {
            Assert.Equal("Waiting for Approval", response.Status);
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
        entry.Ranges.Add([2, 10]);
        subaddrs.Add(entry);
        var response = await Lws.UpsertSubaddrs(Address, ViewKey, subaddrs, true);
        TestSubaddrs(response, true, true);
    }

    [Fact]
    public async Task TestProvisionSubaddrs()
    {
        var response = await Lws.ProvisionSubaddrs(Address, ViewKey, 0, 20, 1, 1, true);
        TestSubaddrs(response, true, true);
    }
    
    [Fact]
    public async Task TestGetSubaddrs()
    {
        var response = await Lws.GetSubaddrs(Address, ViewKey);
        Assert.Empty(response.NewSubaddrs);
        Assert.NotNull(response.AllSubaddrs);
        Assert.NotEmpty(response.AllSubaddrs);
        foreach (var entry in response.AllSubaddrs)
        {
            TestSubaddrsEntry(entry);
        }
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

    private static void TestSpends(List<MoneroLwsSpend>? spends)
    {
        Assert.NotNull(spends);
        foreach (var spend in spends)
        {
            TestSpend(spend);
        }
    }
    
    private static void TestSpend(MoneroLwsSpend? spend)
    {
        Assert.NotNull(spend);
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
        Assert.NotNull(entry);
        Assert.True(entry.AccountIndex >= 0);
        TestIndexRanges(entry.Ranges);
    }

    private static void TestIndexRanges(List<List<long>>? ranges)
    {
        Assert.NotNull(ranges);
        Assert.NotEmpty(ranges);
        foreach (var indexRange in ranges)
        {
            TestIndexRange(indexRange);
        }
    }

    private static void TestIndexRange(List<long>? indexRange)
    {
        Assert.NotNull(indexRange);
        Assert.Equal(2, indexRange.Count);
        var lowerBound = indexRange[0];
        var upperBound = indexRange[1];
        
        Assert.True(lowerBound >= 0);
        Assert.True(upperBound >= 0);
        Assert.True(lowerBound <= upperBound);
    }
    
}