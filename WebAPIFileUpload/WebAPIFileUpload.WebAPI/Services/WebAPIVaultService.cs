using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WebAPIFileUpload.Common.Infrastructure;
using WebAPIFileUpload.Common.Utilities;

namespace WebAPIFileUpload.WebAPI.Services
{
    public class WebAPIVaultService : IVaultService
    {
        public FileUploadResult Save(string fileFullPath)
        {
            FileUploadResult uploadResult = null;

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var fileStream = File.Open(fileFullPath, FileMode.Open);
                    var fileInfo = new FileInfo(fileFullPath);
                    bool _fileUploaded = false;

                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(fileStream), "\"file\"", string.Format("\"{0}\"", fileInfo.Name));

                    Task taskUpload = httpClient.PostAsync(VaultServiceAddress, content).ContinueWith(task =>
                    {
                        if (task.Status == TaskStatus.RanToCompletion)
                        {
                            var response = task.Result;

                            if (response.IsSuccessStatusCode)
                            {
                                uploadResult = response.Content.ReadAsAsync<FileUploadResult>().Result;
                                if (uploadResult != null)
                                    _fileUploaded = true;
                            }
                            else
                            {
                                Debug.WriteLine("Status Code: {0} - {1}", response.StatusCode, response.ReasonPhrase);
                                Debug.WriteLine("Response Body: {0}", response.Content.ReadAsStringAsync().Result);
                            }
                        }

                        fileStream.Dispose();
                    });

                    taskUpload.Wait();

                    if (_fileUploaded)
                        Debug.WriteLine(uploadResult.FileName + " with length " + uploadResult.FileLength + " has been uploaded at " + uploadResult.LocalFilePath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                throw ex;
            }

            return uploadResult;
        }

        private string VaultServiceAddress
        {
            get
            {
                var serviceAddress = Configurations.GetVaultServiceAddress("VaultServerBaseServiceAddress");
                Debug.WriteLine(string.Format("Connected to Vault Service Address = [{0}]", serviceAddress));

                return serviceAddress;
            }
        }
    }
}