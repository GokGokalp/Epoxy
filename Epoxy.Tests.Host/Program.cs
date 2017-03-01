using System;
using Microsoft.Owin.Hosting;

namespace Epoxy.Tests.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("Web Server is running on 8080 port.");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}