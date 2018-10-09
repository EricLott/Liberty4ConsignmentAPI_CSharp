using LibertyConsignmentAPI;
using LibertyConsignmentAPI.Auth;
using System;
using System.Security;

namespace LibertyAPITest
{
    class Program
    {
        //RSWID: 286617
        //Application ID: 10001
        //Expire Date: 1/5/2019
        //API Key:  CF6FC29E5D272CC1C0865D289C97963A

        static void Main(string[] args)
        {
            Credentials LibertyApiCredentials = new Credentials("10001", "14", "CF6FC29E5D272CC1C0865D289C97963A", "ubpos01");
            LibertyApiCredentials.KeepSessionOpen = true;
            SecureString pass = new SecureString();
            #region Password
            foreach (char ch in "Liberty,1")
            {
                pass.AppendChar(ch);
            }
            #endregion
            LibertyApiCredentials.User = new User("elott", pass);

            
            LoginCall loginCall = new LoginCall(LibertyApiCredentials.User);
            LoginResponse loginResponse = loginCall.Execute(LibertyApiCredentials);

            if (loginResponse.Response.Result == 1)
            {
                Console.WriteLine("login successful");
            }

            GetCategoryTreeCall getCategoryTreeCall = new GetCategoryTreeCall(10001328);
            CategoryTreeResponse categoryTreeResponse = getCategoryTreeCall.Execute(LibertyApiCredentials);

            Console.WriteLine(categoryTreeResponse.Response.Level1CategoryID);
            Console.WriteLine(categoryTreeResponse.Response.Level2CategoryID);
            Console.WriteLine(categoryTreeResponse.Response.Level3CategoryID);

            Console.ReadLine();
            return;
            
            #region AddClientTest
            Address address = new Address
            {
                City = "Scottsboro",
                State = "AL",
                Zip = 35768,
                Primary = 1,
                Line1 = "",
                Line2 = "",
                Line3 = "",
                Name = ""
            };

            Client c = new Client
            {
                Firstname = "API TEST",
                Lastname = "DELETE ME",
                Address = address,
                Account = 987987978,
                BirthMonth = 0,
                Company = "",
                Email = "",
                Phone = "",
                TaxExempt = 0,
                TaxExemptNo = "",
                Website = "",
                ClientTypeID = 1
            };

            AddClientCall ac = new AddClientCall(c);
            ClientResponse cr = ac.Execute(LibertyApiCredentials);
            #endregion
            
            #region AddItemTest
            try
            {
                GetNewItemCall newItemCall = new GetNewItemCall();
                newItemCall.ClientID = "370896";
                ItemResponse item = newItemCall.Execute(LibertyApiCredentials);

                AddItemCall addItemCall = new AddItemCall();
                Item newItem = new Item();
                newItem = item.Response.Item;
                newItem.Name = "Test Item";
                newItem.CategoryId = 10001485;
                newItem.Description = "Description";
                newItem.SectionID = 1;
                newItem.LocationID = 1;
                newItem.Quantity = 0;
                newItem.TagQuantity = 1;
                newItem.Price = 0;
                newItem.Msrp = 0;
                newItem.UserField1 = "";
                newItem.Attributes = null;

                addItemCall.Item = newItem;

                ItemResponse i = addItemCall.Execute(LibertyApiCredentials);

                Console.ReadLine();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            #endregion
        }
    }
}
