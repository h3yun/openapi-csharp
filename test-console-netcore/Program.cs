// See https://aka.ms/new-console-template for more information
using H3Yun.OpenApi.NetCore;
using System.Formats.Asn1;

Console.WriteLine("Hello, World!");

string engineCode = "mi4x54jcr54b0p8hwoad4wxo3";
string engineSecret = "IX0Zgel7bavBnupzU4E0jZ/jYsb2qp0H1bnf3iJEG06EcssZlNWLpg==";
string schemaCode = "D0001837d5662fe65c24c49af0ec9a664bd8f77";

var api = new H3YunAPI(engineCode, engineSecret);
var data = new Dictionary<string, object>();
data.Add("F0000001", "测试内容");

//添加数据
var t = await api.Add(schemaCode, data);
Console.WriteLine($"BId:{t.Data.BizObjectId},WId:{t.Data.WorkflowInstanceId}");

//获取数据
await api.Get(schemaCode, t.Data.BizObjectId);

//更新数据
var newData = new Dictionary<string, object>();
newData.Add("F0000001", "更改后的测试内容");
await api.Update(schemaCode, t.Data.BizObjectId, newData);

//删除数据
await api.Remove(schemaCode, t.Data.BizObjectId);