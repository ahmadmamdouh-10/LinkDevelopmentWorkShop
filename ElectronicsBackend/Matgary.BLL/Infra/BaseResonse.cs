using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matgary.BLL.Infra
{
    public class BaseResponse
    {
        public string errorMessage { get; set; }
        public bool? isSuccess { get; set; }
        public dynamic Response { get; set; }

        public BaseResponse()
        {
            errorMessage = "";
            isSuccess = true;
        }
        public BaseResponse(bool success, long errorCode, string messageKey)
        {
            this.isSuccess = success;
            this.errorMessage = messageKey;
        }
        public void SetBaseResponse(bool success, long errorCode, string messageKey)
        {
            this.isSuccess = success;
            this.errorMessage = messageKey;
        }

    }
}
