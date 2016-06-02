using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIFileUpload.Common.Utilities;

namespace WebAPIFileUpload.WebAPI.SelfHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceAddress = Configurations.GetServiceAddress("SelfhostBaseServiceAddress");

                Console.WriteLine(" Service is starting at {0}", serviceAddress);
                Console.WriteLine(" ..... ..... .....");

                using (WebApp.Start<Startup>(serviceAddress))
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(" Service was  started at {0}", serviceAddress);
                    Console.WriteLine(" Please press <ENTER> to QUIT the Service...");

                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
