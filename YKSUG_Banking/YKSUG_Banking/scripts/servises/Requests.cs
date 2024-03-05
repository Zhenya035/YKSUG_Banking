using System.Collections.Generic;
using System.Net.Cache;
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
        public static async Task<AuthenticationResponse> SendLogin(AuthenticationRequest request)
        {
            const string AUTH_URL = "https://yksug-banking-system.onrender.com/sign/in";

            string tmpResponse = await SendRequest.PostRequest(AUTH_URL, request, null);

            return JsonConvert.DeserializeObject<AuthenticationResponse>(tmpResponse);
        }

        public static async Task<AccountMainInfo> GetAccount(AccountMainInfo account, string name, string token)
        {
            string GET_ACCOUNT_URL = $"https://yksug-banking-system.onrender.com/accounts/{name}";

            string json = await SendRequest.SendGetRequest(GET_ACCOUNT_URL, token);
            account = JsonConvert.DeserializeObject<AccountMainInfo>(json);

            if (account.Card == null)
            {
                string CARD_URL = $"https://yksug-banking-system.onrender.com/accounts/{name}/cards";
                await SendRequest.PostRequest(CARD_URL, null, token);

                string card = await SendRequest.SendGetRequest(CARD_URL, token);
                account.Card = JsonConvert.DeserializeObject<CardMainInfo>(card);
            }

            return account;
        }

        public static async Task<string> SendTransaction(TransactionRequest request)
        {
            const string TRANSACTION_URL = "https://yksug-banking-system.onrender.com/accounts/transactions/transaction";
            
            return await SendRequest.PostRequest(TRANSACTION_URL, request, MainPage.authResponse.Token);
        }
        
        public static async Task<List<BonusMainData>> ShowAllBonuses()
        {
            string GET_ALL_BONUSES_URL = "https://yksug-banking-system.onrender.com/bonuses/all";

            string json = await SendRequest.SendGetRequest(GET_ALL_BONUSES_URL, MainPage.authResponse.Token);
            List<BonusMainData> bonuses = JsonConvert.DeserializeObject<List<BonusMainData>>(json);

            return bonuses;
        }
        
        public static async Task<List<TransactionMainInfo>> ShowLastServices()
        {
            string GET_TRANSACTION_URL = $"https://yksug-banking-system.onrender.com/accounts/transactions/{MainPage.account.Card.CardNumber}";

            string json = await SendRequest.SendGetRequest(GET_TRANSACTION_URL, MainPage.authResponse.Token);
            List<TransactionMainInfo> transactions = JsonConvert.DeserializeObject<List<TransactionMainInfo>>(json);

            transactions.Reverse();
            
            return transactions;
        }
        
        public static async Task<BuyBonusResponse> BuyBonusPostRequest(BuyBonusRequest request)
        {
            const string BUY_BONUS_URL = "https://yksug-banking-system.onrender.com/accounts/buy-bonus";

            string tmpResponse = await SendRequest.PostRequest(BUY_BONUS_URL, request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<BuyBonusResponse>(tmpResponse);
        }
        
        public static async Task<AdminResponse> CheckTokenPostRequest(AdminCheckBonusTokenRequest request)
        {
            string CHECK_BONUS_URL = $"https://yksug-banking-system.onrender.com/admins/check";

            string tmpResponse = await SendRequest.PostRequest(CHECK_BONUS_URL , request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
        
        public static async Task<AdminResponse> CreateBonusPostRequest(BonusMainData request)
        {
            const string CREATE_BONUS_URL = "https://yksug-banking-system.onrender.com/admins/create-bonus";

            string tmpResponse = await SendRequest.PostRequest(CREATE_BONUS_URL , request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
        
        public static async Task<AdminResponse> GiveTransactionPostRequest(AdminTransactionRequest request)
        {
            const string CREATE_BONUS_URL = "https://yksug-banking-system.onrender.com/admins/add";

            string tmpResponse = await SendRequest.PostRequest(CREATE_BONUS_URL , request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
        
        public static async Task<AdminResponse> TakeTransactionPostRequest(AdminTransactionRequest request)
        {
            const string CREATE_BONUS_URL = "https://yksug-banking-system.onrender.com/admins/get";

            string tmpResponse = await SendRequest.PostRequest(CREATE_BONUS_URL , request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
        
        public static async Task<List<AccountMainInfo>> ShowAllAccounts()
        {
            string GET_ALL_ACCOUNTS_URL = "https://yksug-banking-system.onrender.com/admins/get-accounts";

            string json = await SendRequest.SendGetRequest(GET_ALL_ACCOUNTS_URL, MainPage.authResponse.Token);
            return JsonConvert.DeserializeObject<List<AccountMainInfo>>(json);
        }
        
        public static async Task<AdminResponse> DeleteBonusPostRequest(BonusMainData request)
        {
            string CREATE_BONUS_URL = $"https://yksug-banking-system.onrender.com/admins/{request.name}";

            string tmpResponse = await SendRequest.SendDeleteRequest(CREATE_BONUS_URL , MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
        
        public static async Task<AdminResponse> EditBonusPutRequest(BonusMainData request)
        {
            string CREATE_BONUS_URL = $"https://yksug-banking-system.onrender.com/admins/{request.Id}";

            string tmpResponse = await SendRequest.SendPutRequest(CREATE_BONUS_URL , request, MainPage.authResponse.Token);

            return JsonConvert.DeserializeObject<AdminResponse>(tmpResponse);
        }
    }
}