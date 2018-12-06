using System.Net;
using Microsoft.AspNetCore.Mvc;
public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
 System.Threading.Thread.Sleep(2000);
 return (ActionResult)new OkObjectResult($"Hello");
}