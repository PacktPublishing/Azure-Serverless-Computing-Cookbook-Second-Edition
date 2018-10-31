#r "Newtonsoft.Json"
using Newtonsoft.Json;
using System;

public static void Run(string inputFile, string name, TraceWriter log)
{
    log.Info($"C# External trigger function processed file: " + name);
    var jsonResults = JsonConvert.DeserializeObject<dynamic>(inputFile);
    for(int nIndex=0;nIndex<jsonResults.Count;nIndex++)
    {
        log.Info(Convert.ToString(jsonResults[nIndex].firstname));
        log.Info(Convert.ToString(jsonResults[nIndex].lastname));
        log.Info(Convert.ToString(jsonResults[nIndex].devicelist));
    }  
}