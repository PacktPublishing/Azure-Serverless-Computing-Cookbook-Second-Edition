#r "SendGrid"
#r "Newtonsoft.Json"//new Recipe2
using System;
using SendGrid.Helpers.Mail; 
using Newtonsoft.Json;

public static void Run(string myQueueItem,
                       out SendGridMessage message,
                       TextWriter outputBlob,
                       ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    dynamic inputJson  = JsonConvert.DeserializeObject(myQueueItem);
    string FirstName=null, LastName=null, Email = null;
    string emailContent;

    FirstName=inputJson.FirstName;
    LastName=inputJson.LastName;
    Email=inputJson.Email;

    log.LogInformation($"Email{inputJson.Email}, {inputJson.FirstName + " " + inputJson.LastName}");

    message = new SendGridMessage(); 
    message.SetSubject("New User got registered successfully.");  
    message.SetFrom("donotreply@example.com");
    message.AddTo(Email,FirstName + " " + LastName);

    emailContent = "Thank you <b>" + FirstName + " " + 
    LastName +"</b> for your registration.<br><br>" +  
    "Below are the details that you have provided us<br> <br>"+ "<b>First name:</b> " +
    FirstName + "<br>" +  "<b>Last name:</b> " + 
    LastName + "<br>" +  "<b>Email Address:</b> " + 
    inputJson.Email + "<br><br>  <br>" + "Best Regards," + "<br>" + "Website Team";

    message.AddContent("text/html", emailContent);

    outputBlob.WriteLine(emailContent);
}
