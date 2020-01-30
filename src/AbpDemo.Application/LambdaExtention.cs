using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AbpDemo
{
    /// <summary>
    /// 动态Lambda构造类
    /// </summary>
    /// <typeparam name="Dto"></typeparam>
    public class LambdaExtention<Dto> where Dto : class
    {
        private List<Expression> m_lstExpression = null;
        private ParameterExpression m_Parameter = null;

        public LambdaExtention()
        {
            m_lstExpression = new List<Expression>();
            m_Parameter = Expression.Parameter(typeof(Dto), "x");
        }

        //构造表达式，存放到m_lstExpression集合里面
        public void GetExpression(string strPropertyName, object strValue, ExpressionType expressType)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            if (member.Type == typeof(int?) || member.Type == typeof(int))
            {
                strValue = Convert.ToInt32(strValue);
            }
            else if (member.Type == typeof(DateTime?) || member.Type == typeof(DateTime))
            {
                strValue = Convert.ToDateTime(strValue);
            }


            if (expressType == ExpressionType.Contains)
            {
                if (member.Type == typeof(string))
                {
                    //expRes = Expression.Call(member, typeof(string).GetMethod("Contains"), Expression.Constant(strValue));
                    Expression exp1 = Expression.NotEqual(member, Expression.Constant(null));//判断是否为null
                    Expression exp2 = Expression.Call(member, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), Expression.Constant(strValue));
                    expRes = Expression.AndAlso(exp1, exp2);//只有当值不为空时才进行模糊查询，否则会报错
                }
                else
                {
                    expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
                }
            }
            else if (expressType == ExpressionType.Equal)
            {
                expRes = Expression.Equal(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThan)
            {
                expRes = Expression.LessThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.LessThanOrEqual)
            {
                expRes = Expression.LessThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.GreaterThan)
            {
                expRes = Expression.GreaterThan(member, Expression.Constant(strValue, member.Type));
            }
            else if (expressType == ExpressionType.GreaterThanOrEqual)
            {
                expRes = Expression.GreaterThanOrEqual(member, Expression.Constant(strValue, member.Type));
            }
            //return expRes;
            m_lstExpression.Add(expRes);
        }

        /// <summary>
        /// 获取赋值操作的Lambda
        /// </summary>
        /// <param name="strPropertyName"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        /// 
        //Dto: Person
        //strPropertyName : Name
        //strValue : CH
        public static Expression<Action<Dto>> GetAssignAction(string strPropertyName, object strValue)
        {
            //获取Dto的对象 x=>
            ParameterExpression m_Parameter = Expression.Parameter(typeof(Dto), "x");
            //body
            Expression m_Expression = null;

            //获取Dto里名为Name的属性 x.Name
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            if (member.Type == typeof(int?))
            {
                strValue = (int?)strValue;
            }

            //将CH赋给Name属性   x.Name = "CH"
            m_Expression = Expression.Assign(member, Expression.Constant(strValue, member.Type));

            //将Parameter和Body拼接  x= > x.Name = "CH"
            return Expression.Lambda<Action<Dto>>(m_Expression, m_Parameter);
        }

        //针对Or条件的表达式
        public void GetExpression(string strPropertyName, List<object> lstValue)
        {
            Expression expRes = null;
            MemberExpression member = Expression.PropertyOrField(m_Parameter, strPropertyName);
            foreach (var oValue in lstValue)
            {
                if (expRes == null)
                {
                    expRes = Expression.Equal(member, Expression.Constant(oValue, member.Type));
                }
                else
                {
                    expRes = Expression.Or(expRes, Expression.Equal(member, Expression.Constant(oValue, member.Type)));
                }
            }


            m_lstExpression.Add(expRes);
        }

        //得到Lamada表达式的Expression对象
        public Expression<Func<Dto, bool>> GetLambda()
        {
            Expression whereExpr = null;
            foreach (var expr in m_lstExpression)
            {
                if (whereExpr == null) whereExpr = expr;
                else whereExpr = Expression.And(whereExpr, expr);
            }
            if (whereExpr == null)
                return null;
            return Expression.Lambda<Func<Dto, bool>>(whereExpr, m_Parameter);
        }


    }

}
