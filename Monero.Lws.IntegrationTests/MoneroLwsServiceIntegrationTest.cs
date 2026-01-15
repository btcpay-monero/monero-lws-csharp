using Monero.Lws.Common;
using Monero.Lws.IntegrationTests.Utils;

using Xunit;

namespace Monero.Lws.IntegrationTests;

public class MoneroLwsServiceIntegrationTest
{
    private static readonly string Address = TestUtils.Address;
    private static readonly string ViewKey = TestUtils.PrivateViewKey;
    private static readonly MoneroLwsService Lws = TestUtils.GetLwsService();

    [Fact]
    public async Task TestGetVersion()
    {
        var response = await Lws.GetVersion();
        Assert.False(string.IsNullOrEmpty(response.ServerType));
        Assert.False(string.IsNullOrEmpty(response.ServerVersion));
        Assert.False(string.IsNullOrEmpty(response.LastGitCommitHash));
        Assert.False(string.IsNullOrEmpty(response.LastGitCommitDate));
        Assert.False(string.IsNullOrEmpty(response.GitBranchName));
        Assert.False(string.IsNullOrEmpty(response.NetworkType));
        Assert.False(string.IsNullOrEmpty(response.MoneroVersionFull));
        Assert.True(response.BlockchainHeight > 0);
        Assert.True(response.Api > 0);
        Assert.True(response.MaxSubaddresses > 0);
        Assert.False(response.Testnet);
    }

    [Fact]
    public async Task GetDaemonStatus()
    {
        var response = await Lws.GetDaemonStatus();
        Assert.True(response.OutgoingConnectionsCount >= 0);
        Assert.True(response.IncomingConnectionsCount >= 0);
        Assert.True(response.Height >= 0);
        Assert.True(response.TargetHeight >= 0);
        Assert.False(string.IsNullOrEmpty(response.Network));
        Assert.False(string.IsNullOrEmpty(response.State));
    }

    [Fact]
    public async Task TestLogin()
    {
        var response = await Lws.Login(Address, ViewKey, true, true);
        if (response.NewAddress)
        {
            Assert.True(response.GeneratedLocally);
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
    public async Task TestAcceptRequests()
    {
        var requests = await Lws.ListRequests();
        List<string> addressesToCreate = [];

        foreach (var createRequest in requests.Create)
        {
            addressesToCreate.Add(createRequest.Address);
        }

        var created = await Lws.AcceptRequests("create", addressesToCreate);

        Assert.Equal(addressesToCreate.Count, created.UpdatedAddresses.Count);

        List<string> addressesToImport = [];

        foreach (var importRequest in requests.Import)
        {
            addressesToImport.Add(importRequest.Address);
        }

        var imported = await Lws.AcceptRequests("import", addressesToImport);

        Assert.Equal(addressesToImport.Count, imported.UpdatedAddresses.Count);
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

    [Fact(Skip = "No random outs to select from fakechain")]
    public async Task TestGetRandomOuts()
    {
        var response = await Lws.GetRandomOuts(15, ["100000", "100000"]);
        TestOutputs(response.AmountOuts);
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

        TestOutputs(response.Outputs);
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

    [Fact]
    public async Task TestRescan()
    {
        var response = await Lws.Rescan(0, [Address]);
        Assert.NotEmpty(response.UpdatedAddresses);
        Assert.Single(response.UpdatedAddresses);
        Assert.Equal(Address, response.UpdatedAddresses.First());
    }

    [Fact]
    public async Task TestValidate()
    {
        var response = await Lws.Validate(TestUtils.PublicViewKey, TestUtils.PublicSpendKey, TestUtils.PrivateViewKey);

        if (response.Error != null)
        {
            Assert.Fail($"{response.Error.Field}: {response.Error.Details}");
        }

        Assert.False(string.IsNullOrEmpty(response.Address));
        Assert.Equal(TestUtils.Address, response.Address);
    }

    [Fact]
    public async Task TestListAccounts()
    {
        var response = await Lws.ListAccounts();
        TestAccounts(response.Active);
        TestAccounts(response.Inactive);
        TestAccounts(response.Hidden);
    }

    [Fact]
    public async Task TestListRequests()
    {
        var response = await Lws.ListRequests();
        TestAccounts(response.Create, true);
        TestAccounts(response.Import, true);
    }

    [Fact]
    public async Task TestAddAccount()
    {
        var address = "43a1cERdj8rT1513tmMUY5MBcWbt1hgSo2fgLhoRrkYTPXejjRjU9y2WCjYfdZMLfZ6LKVc7YRGJMdtxD3x9Dtjc6fFjH9q";
        var viewKey = "4f8d491198f5219b80a03ae2be337b37ace5a4626c67e80a68beb7d0e3eaaa08";

        var accounts = await Lws.ListAccounts();
        var found = accounts.Active.Find(x => x.Address.Equals(address));
        if (found != null)
        {
            // account already added
            return;
        }

        var response = await Lws.AddAccount(address, viewKey);
        Assert.NotEmpty(response.UpdatedAddresses);
        Assert.Single(response.UpdatedAddresses);
        Assert.Equal(address, response.UpdatedAddresses.First());
    }

    [Fact]
    public async Task TestModifyAccountStatus()
    {
        // deactivate account
        var response = await Lws.ModifyAccountStatus("inactive", [TestUtils.Address]);
        Assert.NotEmpty(response.UpdatedAddresses);
        Assert.Single(response.UpdatedAddresses);
        Assert.Equal(TestUtils.Address, response.UpdatedAddresses.First());
        // wait for lws to catch up
        Thread.Sleep(5000);
        // reactivate account
        response = await Lws.ModifyAccountStatus("active", [TestUtils.Address]);
        Assert.NotEmpty(response.UpdatedAddresses);
        Assert.Single(response.UpdatedAddresses);
        Assert.Equal(TestUtils.Address, response.UpdatedAddresses.First());
    }

    private static void TestTransaction(MoneroLwsTransaction? tx)
    {
        Assert.NotNull(tx);
        Assert.True(tx.Id >= 0);
        Assert.False(string.IsNullOrEmpty(tx.Hash));
        Assert.False(string.IsNullOrEmpty(tx.TotalReceived));
        Assert.False(string.IsNullOrEmpty(tx.TotalSent));
        Assert.True(tx.Mixin >= 0);
        if (tx.PaymentId != null)
        {
            Assert.NotEmpty(tx.PaymentId);
        }

        if (tx.Mempool)
        {
            Assert.Null(tx.Height);
            Assert.Null(tx.Timestamp);
            Assert.Equal(0, tx.UnlockTime);
        }
        else
        {
            Assert.NotNull(tx.Height);
            Assert.NotNull(tx.Timestamp);
            Assert.True(tx.UnlockTime >= 0);
        }

        if (tx.TotalSent != "0")
        {
            TestSpends(tx.SpentOutputs);
        }
    }

    private static void TestOutputs(List<MoneroLwsRandomOutput>? outputs)
    {
        Assert.NotNull(outputs);
        foreach (var output in outputs)
        {
            TestOutput(output);
        }
    }

    private static void TestOutput(MoneroLwsRandomOutput? output)
    {
        Assert.NotNull(output);
        Assert.False(string.IsNullOrEmpty(output.GlobalIndex));
        Assert.False(string.IsNullOrEmpty(output.PublicKey));
        Assert.False(string.IsNullOrEmpty(output.Rct));
    }

    private static void TestOutputs(List<MoneroLwsOutput>? outputs)
    {
        Assert.NotNull(outputs);
        foreach (var output in outputs)
        {
            TestOutput(output);
        }
    }

    private static void TestOutput(MoneroLwsOutput? output)
    {
        Assert.NotNull(output);
        Assert.True(output.TxId >= 0);
        Assert.True(output.Index >= 0);
        Assert.True(output.GlobalIndex >= 0);
        Assert.True(output.Height >= 0);
        Assert.False(string.IsNullOrEmpty(output.PublicKey));
        Assert.False(string.IsNullOrEmpty(output.Amount));
        Assert.False(string.IsNullOrEmpty(output.Rct));
        Assert.False(string.IsNullOrEmpty(output.TxHash));
        Assert.False(string.IsNullOrEmpty(output.TxPrefixHash));
        Assert.False(string.IsNullOrEmpty(output.TxPubKey));
        Assert.False(string.IsNullOrEmpty(output.Timestamp));

        if (output.SpendKeyImages.Count > 0)
        {
            foreach (var keyImage in output.SpendKeyImages)
            {
                Assert.NotEmpty(keyImage);
            }
        }

        TestAddressMeta(output.Recipient);
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
        Assert.False(string.IsNullOrEmpty(spend.Amount));
        Assert.False(string.IsNullOrEmpty(spend.KeyImage));
        Assert.False(string.IsNullOrEmpty(spend.TxPubKey));
        Assert.True(spend.OutIndex >= 0);
        Assert.True(spend.Mixin >= 0);
        TestAddressMeta(spend.Sender);
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

    private static void TestAddressMeta(MoneroLwsAddressMeta? addressMeta)
    {
        Assert.NotNull(addressMeta);
        Assert.True(addressMeta.MajIndex >= 0);
        Assert.True(addressMeta.MinIndex >= 0);
    }

    private static void TestAccount(MoneroLwsAccount? account, bool request)
    {
        Assert.NotNull(account);
        Assert.False(string.IsNullOrEmpty(account.Address));
        if (request)
        {
            Assert.True(account.StartHeight >= 0);
            return;
        }

        Assert.True(account.ScanHeight >= 0);
        Assert.True(account.AccessTime >= 0);
    }

    private static void TestAccounts(List<MoneroLwsAccount>? accounts, bool request)
    {
        Assert.NotNull(accounts);
        foreach (var account in accounts)
        {
            TestAccount(account, request);
        }
    }

    private static void TestAccounts(List<MoneroLwsAccount>? accounts)
    {
        TestAccounts(accounts, false);
    }
}