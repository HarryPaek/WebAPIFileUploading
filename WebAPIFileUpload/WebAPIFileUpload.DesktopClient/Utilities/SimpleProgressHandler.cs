using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Handlers;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIFileUpload.DesktopClient.Utilities
{
    internal class SimpleProgressHandler : ProgressMessageHandler
    {
        private long _fileSize = 0;

        public long FileSize
        {
            get
            {
                return _fileSize < 1 ? 1 : _fileSize;
            }

            set
            {
                _fileSize = value < 1 ? 1 : value;
            }
        }

        public long BytesTransferred { get; set;}
        
        public int ProgressPercentage {
            get
            {
                int percentage = (int)(BytesTransferred * 100 / FileSize);

                return percentage > 100 ? 100 : (percentage < 0 ? 0 : percentage);
            }
        }
    }
}
