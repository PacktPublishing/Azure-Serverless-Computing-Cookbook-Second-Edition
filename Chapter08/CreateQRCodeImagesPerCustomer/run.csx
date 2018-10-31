#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
#r "Microsoft.WindowsAzure.Storage"
using Microsoft.WindowsAzure.Storage.Blob;

public static async Task<int> Run(DurableActivityContext customerContext,TraceWriter log)
{
int ncustomerId = Convert.ToInt32(customerContext.GetInput<string>());
Random objRandom = new Random(Guid.NewGuid().GetHashCode());
int nRandomValue = objRandom.Next(50000);
for(int nProcessIndex = 0;nProcessIndex<=nRandomValue;nProcessIndex++)
{
log.Info($" running for {nProcessIndex}");
}
return nRandomValue; 
}