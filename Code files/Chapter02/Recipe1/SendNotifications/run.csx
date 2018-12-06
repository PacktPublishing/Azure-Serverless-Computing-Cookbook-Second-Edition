#r "SendGrid"
using System;
using SendGrid.Helpers.Mail; 

public static void Run(string myQueueItem,out SendGridMessage message,  ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    message = new SendGridMessage();
}
