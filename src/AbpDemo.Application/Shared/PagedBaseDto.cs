using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo
{
    public class PagedBaseDto
    {
        /// <summary>
        /// 过滤条件
        /// </summary>
        public List<DataFilter> Filters { get; set; }
        /// <summary>
        /// 排序规则
        /// </summary>
        public List<DataSort> Sorts { get; set; }

        private int pageSize = 10;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int pageIndex = 1;
        /// <summary>
        /// 当前页码
        /// </summary>

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
    }

    public class DataSort
    {
        public string SortName { get; set; }
        public SortType SortType { get; set; }
    }

    public enum SortType
    {
        /// <summary>
        /// 升序
        /// </summary>
        Asc,
        /// <summary>
        /// 降序
        /// </summary>
        Desc
    }

    public class DataFilter
    {
        public string FilterName { get; set; }
        public string FilterValue { get; set; }
        public FilterType FilterType { get; set; }
        public ExpressionType ExpressionType { get; set; }
    }

    public enum FilterType
    {
        String,
        Int,
        Long,
        Boolean
    }

    public enum ExpressionType
    {
        /// <summary>
        ///  like
        /// </summary>
        Contains,
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual
    }



}
