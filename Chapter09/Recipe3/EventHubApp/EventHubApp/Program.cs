namespace EventHubApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EventHubHelper.GenerateEventHubMessages().Wait();
        }
    }
}