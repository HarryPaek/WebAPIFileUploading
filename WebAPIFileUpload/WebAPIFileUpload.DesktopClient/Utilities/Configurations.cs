using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIFileUpload.DesktopClient.Utilities
{
    internal class Configurations
    {
        private const string defaultServiceAddress = "http://localhost/api/fileupload";

        public static string GetServiceAddress()
        {
            string baseAddress = ConfigurationManager.AppSettings.Get("BaseServiceAddress").Trim();

            return !string.IsNullOrWhiteSpace(baseAddress) ? baseAddress : defaultServiceAddress;
        }
    }
}
