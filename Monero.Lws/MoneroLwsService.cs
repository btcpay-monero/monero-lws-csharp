using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Monero.Lws.Common;
using Monero.Lws.Request;
using Monero.Lws.Response;

namespace Monero.Lws;

public class MoneroLwsService(Uri uri, string username, string password, HttpClient? client = null)
{
    private HttpClient _httpClient = client ?? new HttpClient();
    private string _username = username;
    private string _password = password;
    
    private async Task<TResponse> SendCommandAsync<TRequest, TResponse>(string method, TRequest data,
        CancellationToken cts = default)
    {
        var jsonSerializer = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var httpRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(uri, $"/{method}"),
            Content = new StringContent(
                JsonSerializer.Serialize(data, jsonSerializer),
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

            _httpClient = new HttpClient(new HttpClientHandler { Credentials = new NetworkCredential(username, password) });
        }
        else
        {
            _httpClient = new HttpClient(new HttpClientHandler());
        }

        _username = username;
        _password = password;
    }
    
    public async Task<MoneroLwsGetAddressInfoResponse> GetAddressInfo(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsGetAddressInfoResponse>("get_address_info", req);
    }

    public async Task<MoneroLwsGetAddressTxsResponse> GetAddressTxs(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsGetAddressTxsResponse>("get_address_txs", req);
    }

    public async Task<MoneroLwsGetRandomOutsResponse> GetRandomOuts(long count, List<string> amounts)
    {
        var req = new MoneroLwsGetRandomOutsRequest()
        {
            Count = count,
            Amounts = amounts
        };

        return await SendCommandAsync<MoneroLwsGetRandomOutsRequest, MoneroLwsGetRandomOutsResponse>("get_random_outs", req);
    }

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

        return await SendCommandAsync<MoneroLwsGetUnspentOutsRequest, MoneroLwsGetUnspentOutsResponse>("get_unspent_outs", req);
    }

    public async Task<MoneroLwsImportRequestResponse> ImportWallet(string address, string viewKey)
    {
        var req = new MoneroLwsWalletRequest()
        {
            Address = address,
            ViewKey = viewKey
        };

        return await SendCommandAsync<MoneroLwsWalletRequest, MoneroLwsImportRequestResponse>("import_request", req);
    }

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

    public async Task<MoneroLwsStatusResponse> SubmitRawTx(string tx)
    {
        var req = new MoneroLwsSubmitRawTxRequest()
        {
            Tx = tx
        };

        return await SendCommandAsync<MoneroLwsSubmitRawTxRequest, MoneroLwsStatusResponse>("submit_raw_tx", req);
    }
}