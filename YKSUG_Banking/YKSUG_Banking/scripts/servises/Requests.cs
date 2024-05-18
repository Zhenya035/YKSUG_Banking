using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YKSUG_Banking.scripts.api;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.entity.Response;

namespace YKSUG_Banking.scripts.servises
{
    public static class Requests
    {
        const string ServerName = "https://liceumserver.onrender.com";
        
        public static async Task<AuthenticationResponse> SendLogin(AuthenticationRequest request)
        {
            var authUrl = $"{ServerName}/sign/in";

            var tmpResponse = await SendRequest.PostRequest(authUrl, request, null);

            return JsonConvert.DeserializeObject<AuthenticationResponse>(tmpResponse);
        }

        public static async Task<AccountMainInfo> GetAccount(string name, string token)
        {
            var getAccountUrl = $"{ServerName}/accounts/{name}";

            var json = await SendRequest.SendGetRequest(getAccountUrl, token);
            MainPage.account = JsonConvert.DeserializeObject<AccountMainInfo>(json);

            if (MainPage.account.Card != null) return MainPage.account;
            
            var cardUrl = $"{ServerName}/accounts/{name}/cards";
            await SendRequest.PostRequest(cardUrl, null, token);

            var card = await SendRequest.SendGetRequest(cardUrl, token);
            MainPage.account.Card = JsonConvert.DeserializeObject<CardMainInfo>(card);

            return MainPage.account;
        }

        public static async Task<string> SendTransaction(TransactionRequest request)
        {
            var transactionUrl = $"{ServerName}/accounts/transactions/transaction";

            return await SendRequest.PostRequest(transactionUrl, request, MainPage.authResponse.Token);
        }

        public static async Task<List<BonusMainData>> ShowAllBonuses()
        {
            var getAllBonusesUrl = $"{ServerName}/bonuses/all";

            var json = await SendRequest.SendGetRequest(getAllBonusesUrl, MainPage.authResponse.Token);
            var bonuses = JsonConvert.DeserializeObject<List<BonusMainData>>(json);

            return bonuses;
        }

        public static async Task<List<TransactionMainInfo>> ShowLastServices()
        {
            var getTransactionUrl = $"{ServerName}/accounts/transactions/{MainPage.account.Card.CardNumber}";

            var json = await SendRequest.SendGetRequest(getTransactionUrl, MainPage.authResponse.Token);
            var transactions = JsonConvert.DeserializeObject<List<TransactionMainInfo>>(json);

            transactions.Reverse();

            return transactions;
        }

        public static async Task<BuyBonusResponse> BuyBonusPostRequest(BuyBonusRequest request)
        {
            var buyBonusUrl = $"{ServerName}/accounts/buy-bonus";

            var tmpResponse = await SendRequest.PostRequest(buyBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<BuyBonusResponse>(tmpResponse);
        }

        public static async Task<AdminResponse> CheckTokenPostRequest(AdminCheckBonusTokenRequest request)
        {
            var checkBonusUrl = $"{ServerName}/admins/check";

            var tmpResponse = await SendRequest.PostRequest(checkBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }

        public static async Task<AdminResponse> CreateBonusPostRequest(BonusMainData request)
        {
            var createBonusUrl = $"{ServerName}/admins/create-bonus";

            var tmpResponse = await SendRequest.PostRequest(createBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }

        public static async Task<AdminResponse> GiveTransactionPostRequest(AdminTransactionRequest request)
        {
            var createBonusUrl = $"{ServerName}/admins/add";

            var tmpResponse = await SendRequest.PostRequest(createBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }

        public static async Task<AdminResponse> TakeTransactionPostRequest(AdminTransactionRequest request)
        {
            var createBonusUrl = $"{ServerName}/admins/get";

            var tmpResponse = await SendRequest.PostRequest(createBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }

        public static async Task<List<AccountMainInfo>> ShowAllAccounts()
        {
            var getAllAccountsUrl = $"{ServerName}/admins/get-accounts";

            var json = await SendRequest.SendGetRequest(getAllAccountsUrl, MainPage.authResponse.Token);
            return JsonConvert.DeserializeObject<List<AccountMainInfo>>(json);
        }

        public static async Task<AdminResponse> DeleteBonusPostRequest(BonusMainData request)
        {
            var createBonusUrl = $"{ServerName}/admins/{request.name}";

            var tmpResponse = await SendRequest.SendDeleteRequest(createBonusUrl, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }

        public static async Task<AdminResponse> EditBonusPutRequest(BonusMainData request)
        {
            var createBonusUrl = $"{ServerName}/admins/{request.Id}";

            var tmpResponse = await SendRequest.SendPutRequest(createBonusUrl, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
    }
}