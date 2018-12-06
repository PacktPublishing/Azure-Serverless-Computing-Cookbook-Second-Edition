#r "Newtonsoft.Json"
#r "SendGrid"
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail; 
public static async Task<IActionResult> Run(HttpRequest req, IAsyncCollector<SendGridMessage> messages, ILogger log)
{
    log.LogInformation("C# HTTP trigger function processed a request.");

    string name = req.Query["name"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
   
    string strTweet = "";
    SendGridMessage message = new SendGridMessage(); 
    if(data.followersCount >= 200)
    {
        strTweet = "Tweet Content" +  data.tweettext;
        
        message.SetSubject($"{data.Name} with {data.followersCount} followers has posted a tweet");  
        message.SetFrom("donotreply@example.com");
        message.AddTo("prawin2k@gmail.com");
        message.AddContent("text/html", strTweet); 

    }
    else
    {
        message = null;
    }
    await messages.AddAsync(message);
     return (ActionResult)new OkObjectResult($"Hello");
}
