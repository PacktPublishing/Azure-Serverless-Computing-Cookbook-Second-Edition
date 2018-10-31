using System;
using System.Text;
using Microsoft.Azure.EventHubs;
using System.Configuration;
using System.Threading.Tasks;

namespace EventHubApp
{
    class EventHubHelper
    {
        static EventHubClient eventHubClient = null;
        public static async Task GenerateEventHubMessages()
        {

            EventHubsConnectionStringBuilder conBuilder = new
             EventHubsConnectionStringBuilder
             (ConfigurationManager.AppSettings["EventHubConnection"].ToString());

            eventHubClient =
             EventHubClient.CreateFromConnectionString
             (conBuilder.ToString());
            string strMessage = string.Empty;

            for (int nEventIndex = 0; nEventIndex <= 100;
             nEventIndex++)
            {
                strMessage = Convert.ToString(nEventIndex);
                await eventHubClient.SendAsync(new EventData
                 (Encoding.UTF8.GetBytes(strMessage)));
                Console.WriteLine(strMessage);
            }
            await eventHubClient.CloseAsync();
        }
    }
}