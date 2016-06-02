using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebAPIFileUpload.Common.Infrastructure;
using WebAPIFileUpload.Common.Utilities;
using WebAPIFileUpload.WebAPI.Infrastructure;

namespace WebAPIFileUpload.WebAPI.Controllers
{
    public class FileUploadController : ApiController
    {
        [MimeMultipart]
        public async Task<FileUploadResult> Post()
        {
            try
            {
                var uploadPath = Configurations.GetUploadFolder("UploadFolder");
                uploadPath = HttpContext.Current != null ? HttpContext.Current.Server.MapPath(uploadPath) : uploadPath;

                var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                // Read the MIME multipart asynchronously 
                await Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                string _localFileName = multipartFormDataStreamProvider.FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                // Create response
                return new FileUploadResult
                {
                    LocalFilePath = _localFileName,
                    FileName = Path.GetFileName(_localFileName),
                    FileLength = new FileInfo(_localFileName).Length
                };
            }
            catch(HttpException httpex)
            {
                throw httpex;
            }
            catch(IOException ioex)
            {
                throw ioex;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
