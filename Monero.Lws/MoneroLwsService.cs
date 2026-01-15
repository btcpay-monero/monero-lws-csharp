using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Monero.Lws.Common;
using Monero.Lws.Request;
using Monero.Lws.Response;

namespace Monero.Lws;

public class MoneroLwsService(
    Uri uri,
    string lwsPath,
    string adminPath,
    string username,
    string password,
    string? auth = null,
    HttpClient? client = null)
{
    private HttpClient _httpClient = client ?? new HttpClient();
    private string _username = username;
    private string _password = password;

    private async Task<TResponse> SendCommandAsync<TResponse>(string method, CancellationToken cts = default)
    {
        return await SendCommandAsync<MoneroLwsRequest, TResponse>(method, new MoneroLwsRequest(), cts);
    }

    private async Task<TResponse> SendCommandAsync<TRequest, TResponse>(string method, TRequest data,
        CancellationToken cts = default)
    {
        var jsonSerializer = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var body = JsonSerializer.Serialize(data, jsonSerializer);
        var path = (data is MoneroLwsAdminRequest) ? adminPath : lwsPath;
        var httpRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(uri, $"{path}/{method}"),
            Content = new StringContent(
                body,
                Encoding.UTF8, "application/json")
        };
        httpRequest.Headers.Accept.Clear();
        httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.Default.GetBytes($"{_username}:{_password}")));

        HttpResponseMessage rawResult = await _httpClient.SendAsync(httpRequest, cts);
        int statusCode = (int)rawResult.StatusCode;

        if (statusCode != 200)
        {
            throw new MoneroLwsApiException(statusCode, rawResult.ReasonPhrase ?? "Unknown Error");
        }

        var rawJson = await rawResult.Content.ReadAsStringAsync(cts);

        TResponse? response;
        try
        {
            response = JsonSerializer.Deserialize<TResponse>(rawJson, jsonSerializer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(rawJson);
            throw;
        }

        if (response == null)
        {
            throw new MoneroLwsApiException(500, "Response is null");
        }

        return response;
    }

    public string GetUri()
    {
        return uri.AbsoluteUri;
    }

    public string GetUsername()
    {
        return _username;
    }

    public string GetPassword()
    {
        return _password;
    }

    public void SetCredentials(string username, string password)
    {
        if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(password))
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new MoneroLwsApiException(-1, "username cannot be empty because password is not empty");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new MoneroLwsApiException(-1, "password cannot be empty because username is not empty");
            }

            _httpClient = new HttpClient(
                new HttpClientHandler
                {
                    Credentials = new NetworkCredential(username, password)
                }
            );
        }
        else
        {
            _httpClient = new HttpClient(new HttpClientHandler());
        }

        _username = username;
        _password = password;
    }

    /// <summary>
    /// Get LWS server version.
    /// </summary>
    /// <returns></returns>
    public async Task<MoneroLwsDaemonStatus> GetDaemonStatus()
    {
        return await SendCommandAsync<MoneroLwsDaemonStatus>("daemon_status");
    }

    /// <summary>
    /// Get LWS server version.
    /// </summary>
    /// <returns></returns>
    public async Task<MoneroLwsVersion> GetVersion()
    {
        return await SendCommandAsync<MoneroLwsVersion>("get_version");
    }

    /// <summary>
    /// Returns the minimal set of information needed to calculate a wallet balance,
    /// including the balance of subaddresses. The server cannot calculate when a spend occurs without the spend key,
    /// so a list of candidate spends is returned.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <returns></returns>
    public async Task<MoneroLwsGetAddressInfoResponse> GetAddressInfo(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsGetAddressInfoResponse>("get_address_info", req);
    }

    /// <summary>
    /// Returns information needed to show transaction history.
    /// The server cannot calculate when a spend occurs without the spend key,
    /// so a list of candidate spends is returned.
    /// The response should show a wallet's entire history, including transactions to and from subaddresses.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <returns></returns>
    public async Task<MoneroLwsGetAddressTxsResponse> GetAddressTxs(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsGetAddressTxsResponse>("get_address_txs", req);
    }

    /// <summary>
    /// Selects random outputs to use in a ring signature of a new transaction.
    /// </summary>
    /// <param name="count">Mixin (name is historical).</param>
    /// <param name="amounts">XMR amounts that need mixing.</param>
    /// <returns></returns>
    public async Task<MoneroLwsGetRandomOutsResponse> GetRandomOuts(long count, List<string> amounts)
    {
        var req = new MoneroLwsGetRandomOutsRequest()
        {
            Count = count,
            Amounts = amounts
        };

        return await SendCommandAsync<MoneroLwsGetRandomOutsRequest, MoneroLwsGetRandomOutsResponse>("get_random_outs",
            req);
    }

    /// <summary>
    /// Returns a list of received outputs to the wallet, including to subaddresses.
    /// The client must determine when the output was actually spent.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <param name="amount">XMR send amount.</param>
    /// <param name="mixin">Minimum mixin for source output.</param>
    /// <param name="useDust">Return all available outputs.</param>
    /// <param name="dustThreshold">Ignore outputs below this amount.</param>
    /// <returns></returns>
    public async Task<MoneroLwsGetUnspentOutsResponse> GetUnspentOuts(string address, string viewKey, string amount,
        int mixin, bool useDust, string? dustThreshold = null)
    {
        var req = new MoneroLwsGetUnspentOutsRequest
        {
            Address = address,
            ViewKey = viewKey,
            Amount = amount,
            Mixin = mixin,
            UseDust = useDust,
            DustThreshold = dustThreshold
        };

        return await SendCommandAsync<MoneroLwsGetUnspentOutsRequest, MoneroLwsGetUnspentOutsResponse>(
            "get_unspent_outs", req);
    }

    /// <summary>
    /// Request an account scan from specific height.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <param name="fromHeight">Height to rescan.</param>
    /// <returns></returns>
    public async Task<MoneroLwsImportWalletResponse> ImportWallet(string address, string viewKey, long fromHeight)
    {
        var req = new MoneroLwsImportWalletRequest()
        {
            Address = address,
            ViewKey = viewKey,
            FromHeight = fromHeight
        };

        return await SendCommandAsync<MoneroLwsImportWalletRequest, MoneroLwsImportWalletResponse>(
            "import_wallet_request", req);
    }

    /// <summary>
    /// Check for the existence of an account or create a new one.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <param name="createAccount">Try to create new account.</param>
    /// <param name="generatedLocally">Indicate that the address is new.</param>
    /// <returns></returns>
    public async Task<MoneroLwsLoginResponse> Login(string address, string viewKey, bool createAccount,
        bool generatedLocally)
    {
        var req = new MoneroLwsLoginRequest()
        {
            Address = address,
            ViewKey = viewKey,
            CreateAccount = createAccount,
            GeneratedLocally = generatedLocally
        };

        return await SendCommandAsync<MoneroLwsLoginRequest, MoneroLwsLoginResponse>("login", req);
    }

    /// <summary>
    /// Submit raw transaction to be relayed to monero network.
    /// </summary>
    /// <param name="tx">Raw transaction bytes, in format used by daemon p2p comms.</param>
    /// <returns></returns>
    public async Task<MoneroLwsStatusResponse> SubmitRawTx(string tx)
    {
        var req = new MoneroLwsSubmitRawTxRequest()
        {
            Tx = tx
        };

        return await SendCommandAsync<MoneroLwsSubmitRawTxRequest, MoneroLwsStatusResponse>("submit_raw_tx", req);
    }

    /// <summary>
    /// Returns all subaddresses provisioned for a wallet.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <returns></returns>
    public async Task<MoneroLwsSubaddrs> GetSubaddrs(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest()
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsSubaddrs>("get_subaddrs", req);
    }

    /// <summary>
    /// Upsert subaddresses at the specified major and minor indexes. This endpoint is idempotent.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <param name="subaddrs">Subaddresses to upsert.</param>
    /// <param name="getAll">Whether to include all subaddresses in response.</param>
    /// <returns></returns>
    public async Task<MoneroLwsSubaddrs> UpsertSubaddrs(string address, string viewKey,
        List<MoneroLwsSubaddrsEntry> subaddrs, bool getAll)
    {
        var req = new MoneroLwsUpsertSubaddrsRequest()
        {
            Address = address,
            ViewKey = viewKey,
            Subaddrs = subaddrs,
            GetAll = getAll
        };

        return await SendCommandAsync<MoneroLwsUpsertSubaddrsRequest, MoneroLwsSubaddrs>("upsert_subaddrs", req);
    }

    /// <summary>
    /// Provision subaddresses at specified indexes. No two clients should ever receive
    /// the same newly provisioned subaddresses when calling this endpoint; the server
    /// should guarantee that newly provisioned subaddresses are fresh.
    /// </summary>
    /// <param name="address">Standard address of the wallet.</param>
    /// <param name="viewKey">View key bytes.</param>
    /// <param name="majIndex">Subaddress major index.</param>
    /// <param name="minIndex">Subaddress minor index.</param>
    /// <param name="majCount">Number of major subaddresses to provision.</param>
    /// <param name="minCount">Number of minor subaddresses to provision.</param>
    /// <param name="getAll">Whether to include all subaddresses in response.</param>
    /// <returns></returns>
    public async Task<MoneroLwsSubaddrs> ProvisionSubaddrs(string address, string viewKey, long majIndex, long minIndex,
        long majCount, long minCount, bool getAll)
    {
        var req = new MoneroLwsProvisionSubaddrsRequest()
        {
            Address = address,
            ViewKey = viewKey,
            MajIndex = majIndex,
            MinIndex = minIndex,
            MajCount = majCount,
            MinCount = minCount,
            GetAll = getAll
        };

        return await SendCommandAsync<MoneroLwsProvisionSubaddrsRequest, MoneroLwsSubaddrs>("provision_subaddrs", req);
    }

    #region Administration

    /// <summary>
    /// Accepts create or import requests in the incoming queue.
    /// </summary>
    /// <param name="type"><c>create</c> or <c>import</c>.</param>
    /// <param name="addresses">Account addresses to accept.</param>
    /// <returns></returns>
    public async Task<MoneroLwsUpdateResponse> AcceptRequests(string type, List<string> addresses)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                RequestType = type,
                Addresses = addresses
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsUpdateResponse>("accept_requests", req);
    }

    /// <summary>
    /// Reject account creation or import from the incoming queue.
    /// </summary>
    /// <param name="type"><c>create</c> or <c>import</c>.</param>
    /// <param name="addresses">Account addresses to reject.</param>
    /// <returns></returns>
    public async Task<MoneroLwsUpdateResponse> RejectRequests(string type, List<string> addresses)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                RequestType = type,
                Addresses = addresses
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsUpdateResponse>("reject_requests", req);
    }

    /// <summary>
    /// Add account for view-key scanning.
    /// </summary>
    /// <param name="address">Account primary address.</param>
    /// <param name="viewKey">Account private view key.</param>
    /// <returns></returns>
    public async Task<MoneroLwsUpdateResponse> AddAccount(string address, string viewKey)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                Address = address,
                Key = viewKey
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsUpdateResponse>("add_account", req);
    }

    /// <summary>
    /// Change an account status to <c>active</c>, <c>inactive</c> or <c>hidden</c>.
    /// The <c>active</c> state is the normal state: the account is being scanned and returned by the API.
    /// The <c>inactive</c> state is still returned by the API, but is no longer being scanned.
    /// The <c>hidden</c> is the current way to <i>delete</i> an account: it is not scanned nor returned by the API.
    /// Accounts cannot currently be deleted due to internal DB requirements.
    /// </summary>
    /// <param name="status">Account status to apply (<c>active</c>, <c>inactive</c> or <c>hidden</c>).</param>
    /// <param name="addresses">Account addresses.</param>
    /// <returns></returns>
    public async Task<MoneroLwsUpdateResponse> ModifyAccountStatus(string status, List<string> addresses)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                Addresses = addresses,
                Status = status
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsUpdateResponse>("modify_account_status", req);
    }

    /// <summary>
    /// Request a listing of all active accounts in the database.
    /// </summary>
    /// <returns></returns>
    public async Task<MoneroLwsListAccountsResponse> ListAccounts()
    {
        var req = new MoneroLwsAdminRequest
        {
            Auth = auth
        };
        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsListAccountsResponse>("list_accounts", req);
    }

    /// <summary>
    /// Return the listing of all pending new account requests and all requests to import from genesis block requests.
    /// </summary>
    /// <returns></returns>
    public async Task<MoneroLwsListRequestsResponse> ListRequests()
    {
        var req = new MoneroLwsAdminRequest
        {
            Auth = auth
        };
        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsListRequestsResponse>("list_requests", req);
    }

    /// <summary>
    /// Rescan specific account(s) from the specified height.
    /// </summary>
    /// <param name="height">Blockchain height.</param>
    /// <param name="addresses">Addresses to rescan.</param>
    /// <returns></returns>
    public async Task<MoneroLwsUpdateResponse> Rescan(long height, List<string> addresses)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                Addresses = addresses,
                Height = height
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsUpdateResponse>("rescan", req);
    }

    /// <summary>
    /// Validate account keys.
    /// </summary>
    /// <param name="viewPublicHex">Account view public key as hex.</param>
    /// <param name="spendPublicHex">Account spend public key as hex.</param>
    /// <param name="viewKeyHex">Account private view key as hex.</param>
    /// <returns></returns>
    public async Task<MoneroLwsValidateResponse> Validate(string viewPublicHex, string spendPublicHex,
        string viewKeyHex)
    {
        var req = new MoneroLwsAdminRequest
        {
            Params = new MoneroLwsAdminParams
            {
                ViewPublicHex = viewPublicHex,
                SpendPublicHex = spendPublicHex,
                ViewKeyHex = viewKeyHex
            },
            Auth = auth
        };

        return await SendCommandAsync<MoneroLwsAdminRequest, MoneroLwsValidateResponse>("validate", req);
    }

    #endregion
}