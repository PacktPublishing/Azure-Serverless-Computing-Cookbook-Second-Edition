#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(
                    HttpRequest req, 
                    ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string firstname=null,lastname = null;

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic inputJson = JsonConvert.DeserializeObject(requestBody);
    firstname = firstname ?? inputJson?.firstname;
    lastname = inputJson?.lastname;

    return (lastname + firstname) != null
        ? (ActionResult)new OkObjectResult($"Hello, {firstname + " " + lastname}")
        : new BadRequestObjectResult("Please pass a name on the query" +
         "string or in the request body");
}
