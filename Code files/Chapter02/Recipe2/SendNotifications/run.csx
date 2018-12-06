#r "SendGrid"
#r "Newtonsoft.Json"//new Recipe2
using System;
using SendGrid.Helpers.Mail; 
using Newtonsoft.Json;//new Recipe2

public static void Run(string myQueueItem,out SendGridMessage message,  ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    dynamic inputJson  = JsonConvert.DeserializeObject(myQueueItem);//new Recipe2
    string FirstName=null, LastName=null, Email = null;//new recipe2
    FirstName=inputJson.FirstName;//new Recipe2
    LastName=inputJson.LastName;//new Recipe2
    Email=inputJson.Email;//new Recipe2
    log.LogInformation($"Email{inputJson.Email}, {inputJson.FirstName + " " + inputJson.LastName}");//new Recipe2
    message = new SendGridMessage(); 
    message.SetSubject("New User got registered successfully.");  //new Recipe2
    message.SetFrom("donotreply@example.com");//new Recipe2
    message.AddTo(Email,FirstName + " " + LastName);//new Recipe2
    message.AddContent("text/html", "Thank you <b>" + FirstName + "</b><b> " + LastName +" </b>so much for getting registered to our site.");//new Recipe2
}
