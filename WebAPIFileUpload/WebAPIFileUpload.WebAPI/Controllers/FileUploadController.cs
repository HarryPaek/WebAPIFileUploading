﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPIFileUpload.Common.Infrastructure;
using WebAPIFileUpload.Common.Utilities;
using WebAPIFileUpload.WebAPI.Infrastructure;
using WebAPIFileUpload.WebAPI.Provider;
using WebAPIFileUpload.WebAPI.Services;

namespace WebAPIFileUpload.WebAPI.Controllers
{
    public class FileUploadController : ApiController
    {
        [MimeMultipart]
        public async Task<FileUploadResult> Post()
        {
            try
            {
                IFileServiceProvider fileServiceProvider = new WebAPIFileServiceProvider();
                var fileUploadResult = await fileServiceProvider.Upload(Request);

                if (UseVaultService())
                {
                    IVaultService vaultService = new WebAPIVaultService();
                    var vaultUploadResult = vaultService.Save(fileUploadResult.LocalFilePath);

                    if (vaultUploadResult != null)
                        System.Diagnostics.Debug.WriteLine("Vault Upload Result = [{0}]", new[] { vaultUploadResult });
                    else
                        System.Diagnostics.Debug.WriteLine("Vault Upload was failed!!!");
                }

                DeleteTempFileInAppServer(fileUploadResult);

                // Create response
                return fileUploadResult;
            }
            catch(HttpException httpex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(httpex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw httpex;
            }
            catch(IOException ioex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(ioex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw ioex;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw ex;
            }
        }

        private void DeleteTempFileInAppServer(FileUploadResult fileUploadResult)
        {
            return; // TO DO
        }

        private bool UseVaultService()
        {
            return Configurations.GetUseVaultServiceFlag();
        }
    }
}
