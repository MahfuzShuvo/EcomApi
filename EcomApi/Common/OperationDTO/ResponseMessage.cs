using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Common.OperationDTO
{
    public class ResponseMessage
    {
#nullable enable
        public object? ResponseObject { get; set; }
        public int ResponseCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
