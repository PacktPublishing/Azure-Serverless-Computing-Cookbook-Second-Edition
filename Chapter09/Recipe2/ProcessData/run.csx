using System;
        public static void Run(string myQueueItem, 
         TraceWriter log)
        {
           if(Convert.ToInt32(myQueueItem)>50)
           {
              throw new Exception(myQueueItem);
           }
           else
           {
              log.Info($"C# Queue trigger function processed: {myQueueItem}");
           }
        }