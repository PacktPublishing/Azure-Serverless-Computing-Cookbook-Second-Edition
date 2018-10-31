#r "Microsoft.Azure.WebJobs.Extensions.DurableTask"

public static string Run(DurableActivityContext greetingContext)
{
    string name = greetingContext.GetInput<string>();
    return $"Hello {name}!";
}