using System;
        public static void Run(Stream outputBlob,string myQueueItem, 
         TraceWriter log)
        {
            byte[] imageData = null;
            using (var wc = new System.Net.WebClient())
            {
                imageData = wc.DownloadData(myQueueItem);
            }
        outputBlob.WriteAsync(imageData,0,imageData.Length);
        }