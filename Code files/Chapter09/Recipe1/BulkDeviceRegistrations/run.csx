#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;


public static async Task<IActionResult> Run(HttpRequest req, ILogger log,IAsyncCollector<string> DeviceQueue )
{
log.LogInformation("C# HTTP trigger function processed a request.");
string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
dynamic data = JsonConvert.DeserializeObject(requestBody);
string Device = string.Empty;
for(int nIndex=0;nIndex<data.devices.Count;nIndex++)
{
Device = Convert.ToString(data.devices[nIndex]);
log.LogInformation("devices data" + Device);
}

for(int nIndex=0;nIndex<data.devices.Count;nIndex++)
{
Device = Convert.ToString(data.devices[nIndex]);
DeviceQueue.AddAsync(Device); 
}


return (ActionResult)new OkObjectResult("Program has been executed Successfully.");
}