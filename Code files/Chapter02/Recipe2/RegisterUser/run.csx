#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"


using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;


public static async Task<IActionResult> Run(
                    HttpRequest req, 
                    CloudTable objUserProfileTable,
                    IAsyncCollector<string> objUserProfileQueueItem,
                    IAsyncCollector<string> NotificationQueueItem,
                    ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request");

    string firstname=null,lastname = null, email = null;//new recipe2

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic inputJson  = JsonConvert.DeserializeObject(requestBody);
    firstname = firstname ?? inputJson ?.firstname; 
    lastname = inputJson ?.lastname;
    email = inputJson.email;//new recipe2

    string profilePicUrl = inputJson.ProfilePicUrl;
    await objUserProfileQueueItem.AddAsync(profilePicUrl);

    UserProfile objUserProfile = new UserProfile(firstname, lastname,email);//new recipe2
    TableOperation objTblOperationInsert = 
    TableOperation.Insert(objUserProfile);
    await objUserProfileTable.ExecuteAsync(objTblOperationInsert); 

    await NotificationQueueItem.AddAsync(JsonConvert.SerializeObject(objUserProfile));//new recipe2

  
    return (lastname + firstname) != null
        ? (ActionResult)new OkObjectResult($"Hello, {firstname + " " + lastname}")
        : new BadRequestObjectResult("Please pass a name on the query" +
         "string or in the request body");
}

public class UserProfile : TableEntity
 {
    public UserProfile(string firstName,string lastName,string email)//new recipe2
    {
    this.PartitionKey = "p1";
    this.RowKey = Guid.NewGuid().ToString();
    this.FirstName = firstName;
    this.LastName = lastName;
    this.Email = email;//new recipe2
    }
    public UserProfile() { }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email  { get; set; }//new recipe2
 }