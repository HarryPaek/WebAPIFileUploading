using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIFileUpload.Common.Infrastructure;

namespace WebAPIFileUpload.WebAPI.Services
{
    public interface IVaultService
    {
        FileUploadResult Save(string fileFullPath);
    }
}
