using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIFileUpload.Common.Utilities;

namespace WebAPIFileUpload.WebAPI.VaultServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var serviceAddress = Configurations.GetServiceAddress("VaultServerBaseServiceAddress");

                Console.WriteLine(" Vault Server is starting at {0}", serviceAddress);
                Console.WriteLine(" ..... ..... .....");

                using (WebApp.Start<Startup>(serviceAddress))
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine(" Vault Server was started at {0}@{1}", serviceAddress, DateTime.Now);
                    Console.WriteLine(" Please press <ENTER> to QUIT the Vault Server...");

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
