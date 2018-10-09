
//var client = new RestClient("https://postman-echo.com/get?foo1=bar1&foo2=bar2");
//client.Authenticator = new HttpBasicAuthenticator(username, password);

//var request = new RestRequest("", Method.GET);
//request.AddParameter("name", "value"); // adds to POST or URL query based on Method
//request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

//add parameters for all properties on an object
//request.AddObject(object);

//or just whitelisted properties
//request.AddObject(object, "PersonId", "Name", ...);

//easily add HTTP Headers
//request.AddHeader("header", "value");

//add files to upload(works with compatible verbs)
//request.AddFile("file", path);

//execute the request
//IRestResponse response = client.Execute(request);
//var content = response.Content; // raw content as 

//or automatically deserialize result
//return content type is sniffed but can be explicitly set via RestClient.AddHandler();
//IRestResponse<Person> response2 = client.Execute<Person>(request);
//var name = response2.Data.Name;

//or download and save file to disk
//client.DownloadData(request).SaveAs(path);

//easy async support
//await client.ExecuteAsync(request);

//Console.WriteLine(response);

//abort the request on demand
//asyncHandle.Abort();



