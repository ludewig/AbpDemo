
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Documents;
using Muyan.Search;
using Spatial4n.Core.Context;
using Spatial4n.Core.Shapes;
using Spatial4n.Core.Shapes.Impl;

namespace AbpDemo
{
    /// <summary>
    /// 实体类
    /// </summary>
    public class Company : AuditedEntity<string>, Muyan.Search.IEntity<string>
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        [Index(FieldName = "CompanyName", FieldType = FieldDataType.Text, IsStore = Field.Store.YES)]
        public virtual string CompanyName { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        [Index(FieldName = "Representative", FieldType = FieldDataType.String, IsStore = Field.Store.YES)]
        public virtual string Representative { get; set; }
        /// <summary>
        /// 注册资本
        /// </summary>
        [Index(FieldName = "CapitalAmount", FieldType = FieldDataType.Int32, IsStore = Field.Store.YES)]
        public virtual int CapitalAmount { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        [Index(FieldName = "EnrollDate", FieldType = FieldDataType.DateTime, IsStore = Field.Store.YES)]
        public virtual DateTime EnrollDate { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string PhoneNum { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        [Index(FieldName = "Industry", FieldType = FieldDataType.Facet, IsStore = Field.Store.YES)]
        public virtual string Industry { get; set; }
        /// <summary>
        /// 存续状态
        /// </summary>
        [Index(FieldName = "Status", FieldType = FieldDataType.Facet, IsStore = Field.Store.YES)]
        public virtual string Status { get; set; }

        [Index(FieldName = CoreConstant.DefaultGeoField,FieldType = FieldDataType.Geo,IsStore = Field.Store.YES)]
        internal virtual Point Location {
            get
            {
                return _context.MakePoint(Longitude,Latitude) as Point;
            } 
        }

        /// <summary>
        /// 经度
        /// </summary>
        public virtual double Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public virtual double Latitude { get; set; }

        private Spatial4n.Core.Context.SpatialContext _context;

        internal Company()
        {
            _context=SpatialContext.GEO;
        }
    }
}
