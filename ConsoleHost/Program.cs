using Microsoft.Owin.Hosting;
using System;

namespace ConvRse.ConsoleHost
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebApplication.Start<Startup>("http://127.0.0.1:7777/"))
            {
                Console.WriteLine("Server running at http://127.0.0.1:7777/");
                Console.ReadLine();
            }
        }
    }
}
