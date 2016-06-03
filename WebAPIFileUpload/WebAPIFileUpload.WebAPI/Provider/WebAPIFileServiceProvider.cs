using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebAPIFileUpload.Common.Infrastructure;
using WebAPIFileUpload.Common.Utilities;
using WebAPIFileUpload.WebAPI.Infrastructure;

namespace WebAPIFileUpload.WebAPI.Provider
{
    public class WebAPIFileServiceProvider : IFileServiceProvider
    {
        public async Task<FileUploadResult> Upload(HttpRequestMessage request)
        {
            try
            {
                var uploadPath = Configurations.GetUploadFolder("UploadFolder");
                uploadPath = HttpContext.Current != null ? HttpContext.Current.Server.MapPath(uploadPath) : uploadPath;

                System.Diagnostics.Debug.WriteLine("Uploading Path = [{0}]", new string[] { uploadPath });

                var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                // Read the MIME multipart asynchronously 
                await request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                string _localFileName = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                System.Diagnostics.Debug.WriteLine("Uploaded Path = [{0}]", new string[] { _localFileName });

                // Create response
                return new FileUploadResult
                {
                    LocalFilePath = _localFileName,
                    FileName = Path.GetFileName(_localFileName),
                    FileLength = new FileInfo(_localFileName).Length
                };
            }
            catch (HttpException httpex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(httpex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw httpex;
            }
            catch (IOException ioex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(ioex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw ioex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine("");

                throw ex;
            }
        }
    }
}