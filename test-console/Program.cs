using System;
using System.Collections.Generic;

namespace test_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var h3yunApi = new openapi_netcore.H3YunAPI("mi4x54jcr54b0p8hwoad4wxo3", "IX0Zgel7bavBnupzU4E0jZ/jYsb2qp0H1bnf3iJEG06EcssZlNWLpg==");
            var dic = new Dictionary<string, object>();
            dic.Add("F0000001", "单行文本");
            dic.Add("F0000002", "多行文本");
            dic.Add("F0000003", "abcde");
            dic.Add("F0000004", "20111-01-01");
            //var result = h3yunApi.Add("D000183b5ef70da83684fcaa7404e55ffb9fae1", dic).Result;
             var ret = h3yunApi.Get("D000183b5ef70da83684fcaa7404e55ffb9fae1", "808bad1d-3d24-4b3e-822a-c38a86ae548a").Result;
            var d = ret.ReturnData.BizObject["OwnerDeptIdObject"];
            var t = d.GetType();
            var dd = d as Dictionary<string, string>;
            var n = dd["Name"];
        }
    }
}
