#r "Newtonsoft.Json"
#r "SendGrid"
using System;
using System.Net;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
public static void Run(HttpRequestMessage req, 
                       TraceWriter log,
                       out Mail message
                        )
{
    log.Info($"Webhook was triggered!");
    string jsonContent = req.Content.ReadAsStringAsync().Result;
    dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonContent);
    string strTweet = "";
    if(data.followersCount >= 200)
    {
          strTweet = "Tweet Content" +  data.tweettext;
 
          message = new Mail();
          message.Subject = $"{data.Name} with {data.followersCount} followers has posted a tweet";
          message.From = new Email("donotreply@example.com");
          message.AddContent(new Content("text/html",strTweet));
          
          Personalization personalization = new Personalization();
          personalization.AddTo(new Email("praveen.sreeram@gmail.com"));
          message.AddPersonalization(personalization);
    }
    else
    {
        message = null;
    }
}
