#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"

public static async Task<List<string>> Run(DurableOrchestrationContext context)
{
    var outputs = new List<string>();
    outputs.Add(await context.CallFunctionAsync<string>("ConveyGreeting", "Welcome Cookbook Readers"));
    return outputs;
}