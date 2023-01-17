using H3Yun.OpenApi.Model;
using Newtonsoft.Json.Linq;
using System.Text;

namespace H3Yun.OpenApi.NetCore
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
        /// 新增一条业务数据
        /// </summary>
        /// <param name="schemaCode">要添加数据的表单编码</param>
        /// <param name="data">要添加的数据键值对</param>
        /// <returns>成功返回业务数据Id等相关信息，失败则返回null</returns>
        public async Task<ApiResponseModel<CreateResponseModel>> Add(string schemaCode, Dictionary<string, object> data)
        {
            var ret = new ApiResponseModel<CreateResponseModel>();
            if (string.IsNullOrWhiteSpace(schemaCode) || data == null || data.Count == 0)
            {
                ret.ErrorMessage = "参数不合法";
                return ret;
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
                ret.ErrorMessage = response.Content.ReadAsStringAsync().Result;
                return ret;
            }

            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                return ret;
            }
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
            if (dataModel == null)
            {
                ret.ErrorMessage = "创建业务数据失败";
                return ret;
            }

            ret.ErrorMessage = dataModel[nameof(ApiResponseModel.ErrorMessage)]?.ToString();
            var jObj = dataModel["ReturnData"] as JObject;
            if (jObj == null)
            {
                ret.ErrorMessage = "创建业务数据失败";
                return ret;
            }

            ret.Data = new CreateResponseModel(jObj.GetValue(nameof(CreateResponseModel.BizObjectId))?.ToString(), jObj.GetValue(nameof(CreateResponseModel.WorkflowInstanceId))?.ToString());
            return ret;
        }

        /// <summary>
        /// 更新单条业务数据
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="bizObjectId">要更新的业务数据Id</param>
        /// <param name="data">更新后的数据</param>
        /// <returns>执行结果</returns>
        public async Task<ApiResponseModel<bool>> Update(string schemaCode, string bizObjectId, Dictionary<string, object> data)
        {
            var ret = new ApiResponseModel<bool>(false);
            if (string.IsNullOrWhiteSpace(schemaCode) || data == null || data.Count == 0)
            {
                ret.ErrorMessage = "参数不合法";
                return ret;
            }

            var dicParams = new Dictionary<string, object>();
            dicParams.Add("ActionName", "UpdateBizObject");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("BizObjectId", bizObjectId);
            dicParams.Add("BizObject", Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                ret.ErrorMessage = response.Content.ReadAsStringAsync().Result;
                return ret;
            }

            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                ret.ErrorMessage = "更新业务数据失败";
                return ret;
            }
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
            ret.ErrorMessage = dataModel[nameof(ApiResponseModel.ErrorMessage)]?.ToString();
            ret.Data = string.IsNullOrEmpty(ret.ErrorMessage);
            return ret;
        }

        /// <summary>
        /// 删除单条业务数据
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="bizObjectId">要删除的业务数据Id</param>
        /// <returns>执行结果</returns>
        public async Task<ApiResponseModel<bool>> Remove(string schemaCode, string bizObjectId)
        {
            var ret = new ApiResponseModel<bool>(false);
            if (string.IsNullOrWhiteSpace(schemaCode) || string.IsNullOrWhiteSpace(bizObjectId))
            {
                ret.ErrorMessage = "参数不合法";
                return ret;
            }

            var dicParams = new Dictionary<string, object>();
            dicParams.Add("ActionName", "RemoveBizObject");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("BizObjectId", bizObjectId);

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                ret.ErrorMessage = response.Content.ReadAsStringAsync().Result;
                return ret;
            }

            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                ret.ErrorMessage = "删除业务数据失败";
                return ret;
            }
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
            ret.ErrorMessage = dataModel[nameof(ApiResponseModel.ErrorMessage)]?.ToString();
            ret.Data = string.IsNullOrEmpty(ret.ErrorMessage);
            return ret;
        }


        /// <summary>
        /// 获取单条业务数据
        /// </summary>
        /// <param name="schemaCode">表单编码</param>
        /// <param name="bizObjectId">业务数据Id</param>
        /// <returns>业务数据模型</returns>
        public async Task<ApiResponseModel<Dictionary<string, object>>> Get(string schemaCode, string bizObjectId)
        {
            var ret = new ApiResponseModel<Dictionary<string, object>>();
            if (string.IsNullOrWhiteSpace(schemaCode) || string.IsNullOrWhiteSpace(bizObjectId))
            {
                ret.ErrorMessage = "参数不合法";
                return ret;
            }

            var dicParams = new Dictionary<string, object>();
            dicParams.Add("ActionName", "LoadBizObject");
            dicParams.Add("SchemaCode", schemaCode);
            dicParams.Add("BizObjectId", bizObjectId);

            var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(dicParams);
            var content = new StringContent(reqBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_url, content);
            if (!response.IsSuccessStatusCode)
            {
                ret.ErrorMessage = response.Content.ReadAsStringAsync().Result;
                return ret;
            }

            var dataStr = response.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(dataStr))
            {
                ret.ErrorMessage = "获取业务数据失败";
                return ret;
            }

            //部分类型会返回JObject需要用户手动处理
            var dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(dataStr);
            ret.ErrorMessage = dataModel[nameof(ApiResponseModel.ErrorMessage)]?.ToString();
            var jObj = dataModel["ReturnData"] as JObject;
            if (jObj == null)
            {
                ret.ErrorMessage = "获取业务数据失败";
                return ret;
            }

            var boStr = Newtonsoft.Json.JsonConvert.SerializeObject(jObj.GetValue("BizObject"));
            var boDic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(boStr);
            ret.Data = boDic;
            return ret;
        }
    }
}