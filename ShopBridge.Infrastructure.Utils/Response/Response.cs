using ShopBridge.Infrastructure.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Infrastructure.Utils.Response
{
    public class Response<T> : IResponse
    {
        public ResponseCodes ResponseCode { get; set; }
        public string[] Messages { get; set; }
        public T Model { get; set; }
        ResponseCodes IResponse.ResponseCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Response(T data, ResponseCodes responseCode, string[] messages)
        {
            Model = data;
            ResponseCode = responseCode;
            Messages = messages;
        }
        public Response(T data)
        {
            Model = data;
            ResponseCode = ResponseCodes.OK;
            Messages = null;
        }
        public Response(T data, ResponseCodes responseCode)
        {
            Model = data;
            ResponseCode = responseCode;
            Messages = null;
        }
        public Response(T data, ResponseCodes responseCode, string messages)
        {
            Model = data;
            ResponseCode = responseCode;
            Messages = new[] { messages };
        }
    }
}
