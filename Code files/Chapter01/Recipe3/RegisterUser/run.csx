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
                    IAsyncCollector<string> objUserProfileQueueItem,//new
                    ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string firstname=null,lastname = null;

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic inputJson  = JsonConvert.DeserializeObject(requestBody);
    firstname = firstname ?? inputJson ?.firstname;
    lastname = inputJson ?.lastname;

    string profilePicUrl = inputJson.ProfilePicUrl;//new
    await objUserProfileQueueItem.AddAsync(profilePicUrl);//new

    UserProfile objUserProfile = new UserProfile(firstname, lastname);
    TableOperation objTblOperationInsert = 
    TableOperation.Insert(objUserProfile);
    await objUserProfileTable.ExecuteAsync(objTblOperationInsert); 


    return (lastname + firstname) != null
        ? (ActionResult)new OkObjectResult($"Hello, {firstname + " " + lastname}")
        : new BadRequestObjectResult("Please pass a name on the query" +
         "string or in the request body");
}

public class UserProfile : TableEntity
 {
    public UserProfile(string firstName,string lastName)
    {
    this.PartitionKey = "p1";
    this.RowKey = Guid.NewGuid().ToString();
    this.FirstName = firstName;
    this.LastName = lastName;
    }
    public UserProfile() { }
    public string FirstName { get; set; }
    public string LastName { get; set; }
 }