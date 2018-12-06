using System;

public static void Run(string myQueueItem, Stream outputBlob, ILogger log)
{
    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
    byte[] imageData = null;
    using (var wc = new System.Net.WebClient())
    {
        imageData = wc.DownloadData(myQueueItem);
    }
    outputBlob.WriteAsync(imageData,0,imageData.Length);
}
