using System.Net;
        using Newtonsoft.Json;
        public static void Run(HttpRequestMessage req, TraceWriter log, 
         IAsyncCollector<string>DeviceQueue)
        {
           var data = req.Content.ReadAsStringAsync().Result;
           dynamic inputJson = JsonConvert.DeserializeObject<dynamic>
            (data);
           for(int nIndex=0;nIndex<inputJson.devices.Count;nIndex++)
           {
              DeviceQueue.AddAsync
               (Convert.ToString(inputJson.devices
               [nIndex]));
           }
        }