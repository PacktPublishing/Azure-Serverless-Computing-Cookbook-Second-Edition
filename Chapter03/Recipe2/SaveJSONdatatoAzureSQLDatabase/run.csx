
using System;
using Newtonsoft.Json;
public static string Run(string inputFile, string name, TraceWriter log)
{
    log.Info($"C# External trigger function processed file: " + inputFile);
     dynamic inputJson = JsonConvert.DeserializeObject<dynamic>(inputFile);
     log.Info(inputJson.firstname);
    
    return inputFile;
}