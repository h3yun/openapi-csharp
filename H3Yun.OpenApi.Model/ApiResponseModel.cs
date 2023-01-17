namespace H3Yun.OpenApi.Model
{
    /// <summary>
    /// 返回模型
    /// </summary>
    /// <typeparam name="T">返回参数类型</typeparam>
    public class ApiResponseModel<T> : ApiResponseModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ApiResponseModel() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">初始返回数据</param>
        public ApiResponseModel(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    /// <summary>
    /// 返回模型
    /// </summary>
    public class ApiResponseModel
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Successful
        {
            get
            {
                return string.IsNullOrEmpty(this.ErrorMessage);
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }
    }
}
