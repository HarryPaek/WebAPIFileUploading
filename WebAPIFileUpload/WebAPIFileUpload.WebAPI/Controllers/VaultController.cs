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
using WebAPIFileUpload.WebAPI.Provider;

namespace WebAPIFileUpload.WebAPI.Controllers
{
    [RoutePrefix("api/vault")]
    public class VaultController : ApiController
    {
        [MimeMultipart]
        [Route("upload")]
        public async Task<FileUploadResult> Post()
        {
            try
            {
                IFileServiceProvider fileServiceProvider = new WebAPIFileServiceProvider();
                var fileUploadResult = await fileServiceProvider.Upload(Request);

                if (UseVaultReplicationService())  //TO DO
                {
                    //Run Replication Service
                    RunReplicationService(fileUploadResult);
                }

                // Create response
                return fileUploadResult;
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

        private void RunReplicationService(FileUploadResult fileUploadResult)
        {
            //TO DO
            throw new NotImplementedException();
        }

        private bool UseVaultReplicationService()
        {
            return false;
        }
    }
}
