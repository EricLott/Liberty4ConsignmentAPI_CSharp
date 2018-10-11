using LibertyConsignmentAPI.Auth;
using RestSharp;
using System;
using System.Net;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace LibertyConsignmentAPI
{
    public class GetNewItemCall
    {
        public string ClientID { get; set; }

        public ItemResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                if (ClientID == null)
                {
                    throw new Exception("Execute command requires Client ID which was not supplied");
                }
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/newitem?clientid=" + ClientID);
                var request = new RestRequest(Method.GET);
                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                IRestResponse response = client.Execute(request);
                var getNewItemResponse = ItemResponse.FromJson(response.Content);
                return getNewItemResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class GetItemCall
    {
        public string ItemID { get; set; }

        public ItemResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                if (ItemID == null)
                {
                    throw new Exception("Execute command requires Item ID which was not supplied");
                }
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/item?itemid=" + ItemID);
                var request = new RestRequest(Method.GET);
                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                IRestResponse response = client.Execute(request);
                var GetItemResponse = ItemResponse.FromJson(response.Content);
                return GetItemResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class AddItemCall
    {
        public Item Item { get; set; }

        public AddItemCall(Item item)
        {
            Item = item;
        }

        public ItemResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/item?clientid=" + Item.ClientId);
                var request = new RestRequest(Method.POST);
                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));
                AddItem addItem = new AddItem
                {
                    Item = Item
                };
                request.AddParameter("application/json", addItem.ToJson(), ParameterType.RequestBody);
                string s = addItem.ToJson();

                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
                else
                {
                    var GetItemResponse = ItemResponse.FromJson(response.Content);
                    return GetItemResponse;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class AsyncCopyItemCall
    {
        public ItemResponse ItemResponse { get; set; }

        public AsyncCopyItemCall(ItemResponse itemResponse)
        {
            ItemResponse = itemResponse;
        }

        public async Task<ItemResponse> Execute(Credentials LibertyApiAuth)
        {
            try
            {
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/copyitem?itemid=" + ItemResponse.Response.Item.ItemId + "&updatetime=1&cost=" + ItemResponse.Response.Item.Cost + "&quantity=" + ItemResponse.Response.Item.Quantity);
                var request = new RestRequest(Method.GET);

                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                var cancellationTokenSource = new CancellationTokenSource();
                var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new Exception(response.StatusCode.ToString());
                }
                else
                {
                    var GetItemResponse = ItemResponse.FromJson(response.Content);
                    return GetItemResponse;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class LoginCall
    {
        public string Username { get; set; }
        public SecureString Password { get; set; }

        public LoginCall(string user, SecureString pass)
        {
            this.Username = user;
            this.Password = pass;
        }

        public LoginCall(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
        }

        public LoginResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                if (Username == null || Password == null)
                {
                    throw new Exception("User credentials not supplied");
                }
                LibertyApiAuth.GetCallTime();
                Console.WriteLine(LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));
                Console.WriteLine(LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));
                Console.WriteLine(LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));
                String s = Cipher.SecureStringToString(Password);
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/login?username=" + Username + "&password=" + s);
                Password.Clear();
                client.CookieContainer = new CookieContainer();
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                IRestResponse response = client.Execute(request);
                LibertyApiAuth.Cookie = client.CookieContainer;
                var userResponse = LoginResponse.FromJson(response.Content);
                LibertyApiAuth.Cookie = client.CookieContainer;

                return userResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class AsyncLoginCall
    {
        public string Username { get; set; }
        public SecureString Password { get; set; }

        public AsyncLoginCall(string user, SecureString pass)
        {
            this.Username = user;
            this.Password = pass;
        }

        public AsyncLoginCall(User user)
        {
            this.Username = user.Username;
            this.Password = user.Password;
        }

        public async Task<LoginResponse> ExecuteAsync(Credentials LibertyApiAuth)
        {
            try
            {
                if (Username == null || Password == null)
                {
                    throw new Exception("User credentials not supplied");
                }
                LibertyApiAuth.GetCallTime();
                String s = Cipher.SecureStringToString(Password);
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/login?username=" + Username + "&password=" + s);
                Password.Clear();
                client.CookieContainer = new CookieContainer();
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                var cancellationTokenSource = new CancellationTokenSource();
                var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                var loginResponse = LoginResponse.FromJson(response.Content);
                LibertyApiAuth.Cookie = client.CookieContainer;

                return loginResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class AddClientCall
    {
        public Client Client { get; set; }

        /// <summary>
        /// Create a call and refernce the Client object being addded
        /// </summary>
        /// <param name="client">Reference to Client object being added to Liberty</param>
        public AddClientCall(Client client)
        {
            this.Client = client;
        }

        public ClientResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                if (Client == null)
                {
                    throw new Exception("Execute command requires a client object, which was not supplied");
                }

                AddClientClass addClient = new AddClientClass();
                addClient.Client = Client;

                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/basicclient");
                var request = new RestRequest(Method.POST);

                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));
                request.AddParameter("application/json", addClient.ToJson(), ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    throw new LibertyException(response.Content);
                }
                else
                {
                    ClientResponse GetClientResponse = ClientResponse.FromJson(response.Content);
                    return GetClientResponse;
                }

            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class GetCategoryTreeCall
    {
        public int CategoryID { get; set; }

        public GetCategoryTreeCall(int cateogryID)
        {
            CategoryID = cateogryID;
        }

        public CategoryTreeResponse Execute(Credentials LibertyApiAuth)
        {
            try
            {
                if (CategoryID < 9999999)
                {
                    throw new Exception("Execute command requires a valid Category ID which was not supplied");
                }
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/cattree?categoryid=" + CategoryID);
                var request = new RestRequest(Method.GET);
                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                IRestResponse response = client.Execute(request);
                var GetResponse = CategoryTreeResponse.FromJson(response.Content);
                return GetResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }

    public class AsyncGetItemPriceHistory
    {
        public string CategoryID { get; set; }
        public string BrandID { get; set; }

        public AsyncGetItemPriceHistory(string categoryID, string brandID)
        {
            this.CategoryID = categoryID;
            this.BrandID = brandID;
        }

        public async Task<GetItemPriceHistoryResponse> ExecuteAsync(Credentials LibertyApiAuth)
        {
            try
            {
                LibertyApiAuth.GetCallTime();
                var client = new RestClient("http://" + LibertyApiAuth.ServerName + ":" + LibertyApiAuth.ListenPortNumber + "/data/pricehistory?categoryid=" + CategoryID + "&brandid=" + BrandID);
                var request = new RestRequest(Method.GET);
                client.CookieContainer = LibertyApiAuth.Cookie;
                request.AddHeader("x-call-time", LibertyApiAuth.CallTime.ToString());
                request.AddHeader("x-application-id", LibertyApiAuth.ApplicationID);
                request.AddHeader("x-api-version", LibertyApiAuth.ApiVersion);
                request.AddHeader("x-auth-string", LibertyApiAuth.AuthString(LibertyApiAuth.CallTime));

                var cancellationTokenSource = new CancellationTokenSource();
                var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                var historyResponse = GetItemPriceHistoryResponse.FromJson(response.Content);

                return historyResponse;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
    }
}

