using System;
using System.Configuration;
using Microsoft.Azure.EventHubs;
using System.Text;

namespace MyEventHubApp
{
    class Program
    {
        static async void Main(string[] args)
        {
            EventHubsConnectionStringBuilder conBuilder = new EventHubsConnectionStringBuilder(ConfigurationManager.AppSettings["EventHubConnection"].ToString());
            //conBuilder.EntityPath = "MyEventHub";
            EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(conBuilder.ToString());
            string strMessage = string.Empty;
            for(int nEventIndex = 0 ; nEventIndex <= 100; nEventIndex++)
            {
                strMessage = "Message with index:" + nEventIndex;
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(strMessage)));
                Console.WriteLine(strMessage);
            }
            await eventHubClient.CloseAsync();
            
        }
    }
}
