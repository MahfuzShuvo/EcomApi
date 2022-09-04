using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomApi.Common.OperationDTO
{
    public class RequestMessage
    {
#nullable enable
        public object? RequestObject { get; set; }
        public int LoggedInUserID { get; set; }
    }
}
