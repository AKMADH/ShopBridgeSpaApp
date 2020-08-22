using ShopBridge.Infrastructure.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Response
{
    interface IResponse
    {
        ResponseCodes ResponseCode { get; set; }
        string[] Messages { get; set; }
    }
}
