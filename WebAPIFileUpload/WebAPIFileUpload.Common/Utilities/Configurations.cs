using System;
using System.Configuration;

namespace WebAPIFileUpload.Common.Utilities
{
    public class Configurations
    {
        private const string defaultVaultServiceName = "VaultServerBaseServiceAddress";
        private const string defaultVaultServiceAddress = "http://localhost:18888/";

        private const string defaultServiceName = "BaseServiceAddress";
        private const string defaultServiceAddress = "http://localhost:18080/";

        private const string defaultUploadFolderName = "UploadFolder";
        private const string defaultUploadFolder = "~/Uploads";

        private const string defaultUseVaultServiceFlagName = "UseVaultService";
        private const bool   defaultUseVaultServiceFlag = false;

        public static string GetServiceAddress()
        {
            return GetServiceAddress(defaultServiceName);
        }

        public static string GetServiceAddress(string serviceName)
        {
            string baseAddress = ConfigurationManager.AppSettings.Get(serviceName);

            return !string.IsNullOrWhiteSpace(baseAddress) ? baseAddress.Trim() : defaultServiceAddress;
        }

        public static string GetVaultServiceAddress()
        {
            return GetVaultServiceAddress(defaultVaultServiceName);
        }

        public static string GetVaultServiceAddress(string serviceName)
        {
            string baseAddress = ConfigurationManager.AppSettings.Get(serviceName);

            return !string.IsNullOrWhiteSpace(baseAddress) ? baseAddress.Trim() : defaultVaultServiceAddress;
        }

        public static string GetUploadFolder()
        {
            return GetUploadFolder(defaultUploadFolderName);
        }

        public static string GetUploadFolder(string folderName)
        {
            string uploadFolder = ConfigurationManager.AppSettings.Get(folderName);

            return !string.IsNullOrWhiteSpace(uploadFolder) ? uploadFolder.Trim() : defaultUploadFolder;
        }

        public static bool GetUseVaultServiceFlag()
        {
            return GetUseVaultServiceFlag(defaultUseVaultServiceFlagName);
        }

        public static bool GetUseVaultServiceFlag(string flagName)
        {
            string useVaultServiceFlag = ConfigurationManager.AppSettings.Get(flagName);

            return !string.IsNullOrWhiteSpace(useVaultServiceFlag) ? useVaultServiceFlag.Trim().Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase) : defaultUseVaultServiceFlag;
        }

    }
}
