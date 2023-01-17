namespace H3Yun.OpenApi.Model
{
    /// <summary>
    /// 新增业务数据返回数据模型
    /// </summary>
    public class CreateResponseModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bizObjectId">业务数据Id</param>
        /// <param name="workflowInstanceId">流程实例Id</param>
        public CreateResponseModel(string bizObjectId, string workflowInstanceId)
        {
            BizObjectId = bizObjectId;
            WorkflowInstanceId = workflowInstanceId;
        }

        /// <summary>
        /// 业务数据Id
        /// </summary>
        public string BizObjectId { get; set; }

        /// <summary>
        /// 对应流程实例Id，如果没有发起流程则为空
        /// </summary>
        public string WorkflowInstanceId { get; set; }
    }
}
