using System;
namespace BackgroundJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main method execution has been started");
            Console.WriteLine("======================================");
            UserRegistration.RegisterUser();
            OrderProcessing.ProcessOrder();
            Console.WriteLine("======================================");
            Console.WriteLine("Main method execution has been completed");

        }
    }
}
