
using System;
public static void Run(string myQueueItem,ILogger log)
{
if(Convert.ToInt32(myQueueItem)>50)
{
throw new Exception(myQueueItem);
}
else
{
log.LogInformation($"C# Queue trigger function
processed: {myQueueItem}");
}
}