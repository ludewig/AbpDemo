using System;
using System.Collections.Generic;
using System.Text;

namespace AbpDemo.Client
{
    public class Classify
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassifyName { get; set; }
        /// <summary>
        /// 分类级别
        /// </summary>
        public int ClassfyLevel { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>
        public string ClassifyCode { get; set; }
        /// <summary>
        /// 父级分类
        /// </summary>
        public Classify Parent { get; set; }
        /// <summary>
        /// 子级分类
        /// </summary>
        public List<Classify> Children { get; set; }
    }
}
