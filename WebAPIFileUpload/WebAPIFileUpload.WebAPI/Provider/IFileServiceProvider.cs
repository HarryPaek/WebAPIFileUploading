using System.Net.Http;
using System.Threading.Tasks;
using WebAPIFileUpload.Common.Infrastructure;

namespace WebAPIFileUpload.WebAPI.Provider
{
    public interface IFileServiceProvider
    {
        Task<FileUploadResult> Upload(HttpRequestMessage request);
    }
}