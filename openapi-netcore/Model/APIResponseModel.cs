using System.Collections.Generic;

namespace openapi_netcore.Model
{
    public class APIResponseModel
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public bool Logined { get; set; }
        public BizObjectModel ReturnData { get; set; }
        public int DataType { get; set; }
    }

    public class BizObjectModel
    {
        public Dictionary<string, object> BizObject { get; set; }
    }
}
