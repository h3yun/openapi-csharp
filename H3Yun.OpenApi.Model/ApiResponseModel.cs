using System;

namespace H3Yun.OpenApi.Model
{
    public class ApiResponseModel<T>
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public bool Logined { get; set; }
        public T Data { get; set; }
    }
}
