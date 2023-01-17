namespace openapi_netcore.Model
{
    public class Filter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }

    public class FilterItem
    {
        public string ItemName { get; set; }
        public Operator Operator { get; set; }
        public object Value { get; set; }
        public bool IsColumn { get; set; }
    }

    public enum Operator
    {
        /// <summary>
        /// 大于
        /// </summary>
        ABOVE = 0,

        /// <summary>
        /// 大于等于
        /// </summary>
        NOT_BELOW = 1,

        /// <summary>
        /// 等于
        /// </summary>
        EQUAL = 2,

        /// <summary>
        /// 小于等于
        /// </summary>
        NOT_ABOVE = 3,

        /// <summary>
        /// 小于
        /// </summary>
        BELOW = 4,

        /// <summary>
        /// 不等于
        /// </summary>
        NOT_EQUAL = 5,

        /// <summary>
        /// 在范围内
        /// </summary>
        IN = 6,

        /// <summary>
        /// 不在范围内
        /// </summary>
        NOT_IN = 7,

        /// <summary>
        /// 包含内容
        /// </summary>
        CONTAINS = 8,

        /// <summary>
        /// 以指定内容开始
        /// </summary>
        START_WITH = 13,

        /// <summary>
        /// 以指定内容结束
        /// </summary>
        END_WITH = 14,

        /// <summary>
        /// 为空
        /// </summary>
        ISNULL = 18,

        /// <summary>
        /// 不为空
        /// </summary>
        NOT_NULL = 19,
        IS_NONE = 20,

        /// <summary>
        /// 
        /// </summary>
        NOT_NONE = 21,
        NOT_START_WITH = 22,
        NOT_END_WITH = 23,
        NOT_CONTAINS = 24
    }
}
