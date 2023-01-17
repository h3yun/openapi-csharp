using openapi_netcore.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace openapi_netcore
{
    public class H3YunAPI
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _url = "https://infrastructure.h3yun.com/openapi/invoke";

        public H3YunAPI(string engineCode, string engineSecret)
        {
            _httpClient.DefaultRequestHeaders.Add("EngineCode", engineCode);
            _httpClient.DefaultRequestHeaders.Add("EngineSecret", engineSecret);
        }
        /// <summary>
        /// 添加业务数据
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="data">要提交的数据内容</param>
        /// <returns></returns>
        public async Task<bool> Add(string schemaCode, Dictionary<string, object> data)
        {
            if (string.IsNullOrWhiteSpace(schemaCode) || data == null || data.Count == 0)
            {
                throw new System.Exception("参数不合法");
            }

            var dicParams = new Dictionary<string, object>();
            dicParams.Add("ActionName", "CreateBizObject");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("BizObject", Newtonsoft.Json.JsonConvert.SerializeObject(data));
            dicParams.Add("IsSubmit", "true");

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.Content.ReadAsStringAsync().Result);
            }
            return true;
        }

        public async Task<APIResponseModel> Get(string schemaCode, string objectId)
        {
            if (string.IsNullOrWhiteSpace(schemaCode) || string.IsNullOrWhiteSpace(objectId))
            {
                throw new System.Exception("参数不合法");
            }

            var dicParams = new Dictionary<string, string>();
            dicParams.Add("ActionName", "LoadBizObject");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("BizObjectId", objectId);

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.Content.ReadAsStringAsync().Result);
            }
            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                return null;
            }

            //部分类型会返回JObject需要用户手动处理
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseModel>(dataStr);
            return dataModel;
        }

        public async Task<APIResponseModel> GetList(string schemaCode, string[] objectIds)
        {
            if (string.IsNullOrWhiteSpace(schemaCode) || string.IsNullOrWhiteSpace(bizObjectId))
            {
                throw new System.Exception("参数不合法");
            }

            var dicParams = new Dictionary<string, string>();
            dicParams.Add("ActionName", "LoadBizObjects");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("Filter", bizObjectId);

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception(response.Content.ReadAsStringAsync().Result);
            }
            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                return null;
            }

            //部分类型会返回JObject需要用户手动处理
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseModel>(dataStr);
            return dataModel;
        }
    }
}
