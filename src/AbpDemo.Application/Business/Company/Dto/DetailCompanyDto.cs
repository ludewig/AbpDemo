
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;


namespace AbpDemo
{
    /// <summary>
    /// 详情数据传输对象
    /// </summary>
    public class DetailCompanyDto : EntityDto<string>
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        public virtual string Representative { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        public virtual int CapitalAmount { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        public virtual DateTime EnrollDate { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string PhoneNum { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public virtual string Industry { get; set; }
        /// <summary>
        /// 存续状态
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public virtual double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public virtual double Latitude { get; set; }

    }
}
