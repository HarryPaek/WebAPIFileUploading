using System.Configuration;

namespace WebAPIFileUpload.Common.Utilities
{
    public class Configurations
    {
        private const string defaultServiceName = "BaseServiceAddress";
        private const string defaultServiceAddress = "http://localhost:18080/";

        private const string defaultUploadFolderName = "UploadFolder";
        private const string defaultUploadFolder = "~/Uploads";

        public static string GetServiceAddress()
        {
            return GetServiceAddress(defaultServiceName);
        }

        public static string GetServiceAddress(string serviceName)
        {
            string baseAddress = ConfigurationManager.AppSettings.Get(serviceName).Trim();

            return !string.IsNullOrWhiteSpace(baseAddress) ? baseAddress : defaultServiceAddress;
        }

        public static string GetUploadFolder()
        {
            return GetUploadFolder(defaultUploadFolderName);
        }

        public static string GetUploadFolder(string folderName)
        {
            string uploadFolder = ConfigurationManager.AppSettings.Get(folderName).Trim();

            return !string.IsNullOrWhiteSpace(uploadFolder) ? uploadFolder : defaultUploadFolder;
        }
    }
}
