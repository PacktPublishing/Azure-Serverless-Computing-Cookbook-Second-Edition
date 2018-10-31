 #r "Microsoft.Azure.WebJobs.Extensions.DurableTask"
public static async Task<int> Run(DurableOrchestrationContext context)
{
  int[] customers = await         
  context.CallActivityAsync<int[]>("GetAllCustomers");

  var tasks = new Task<int>[customers.Length];
  for (int nCustomerIndex = 0; nCustomerIndex < customers.Length; nCustomerIndex++)
  {
     tasks[nCustomerIndex] = 
     context.CallActivityAsync<int> ("CreateBARCodeImagesPerCustomer", customers[nCustomerIndex]);
  }
    await Task.WhenAll(tasks);
    int nTotalItems = tasks.Sum(item => item.Result);
    return nTotalItems; 
}
