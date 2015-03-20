using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Expressions;
using Light.Data.Mappings;

namespace Light.Data
{
    /// <summary>
    /// 排序表达式
    /// </summary>
    public class OrderExpression : Expression
    {
        List<OrderExpression> _orderExpressions = null;

        internal OrderExpression(DataEntityMapping tableMapping)
        {
            if (tableMapping == null)
            {
                IgnoreConsistency = true;
            }
            else
            {
                TableMapping = tableMapping;
            }
        }

        private static OrderExpression Catch(OrderExpression expression1, OrderExpression expression2)
        {
            if (expression1 == null && expression2 == null)
            {
                return null;
            }
            else if (expression1 == null && expression2 != null)
            {
                return expression2;
            }
            else if (expression1 != null && expression2 == null)
            {
                return expression1;
            }
            if (expression1 is RandomOrderExpression || expression2 is RandomOrderExpression)
            {
                throw new LightDataException(RE.RandomOrderForbitCatch);
            }
            DataEntityMapping demapping = null;

            if (expression1.IgnoreConsistency && !expression2.IgnoreConsistency)
            {
                demapping = expression2.TableMapping;
            }
            else if (!expression1.IgnoreConsistency && expression2.IgnoreConsistency)
            {
                demapping = expression1.TableMapping;
            }
            else if (!expression1.IgnoreConsistency && !expression2.IgnoreConsistency)
            {
                if (expression1.TableMapping.Equals(expression2.TableMapping))
                {
                    demapping = expression1.TableMapping;
                }
                else
                {
                    throw new LightDataException(RE.DataMappingOfExpressionIsNotMatch);
                }
            }
            OrderExpression newExpression = new OrderExpression(demapping);
            List<OrderExpression> list = new List<OrderExpression>();
            if (expression1._orderExpressions == null)
            {
                list.Add(expression1);
            }
            else
            {
                list.AddRange(expression1._orderExpressions);
            }
            if (expression2._orderExpressions == null)
            {
                list.Add(expression2);
            }
            else
            {
                list.AddRange(expression2._orderExpressions);
            }
            newExpression._orderExpressions = list;
            return newExpression;
        }

        /// <summary>
        /// 排序连接
        /// </summary>
        /// <param name="expression1"></param>
        /// <param name="expression2"></param>
        /// <returns></returns>
        public static OrderExpression operator &(OrderExpression expression1, OrderExpression expression2)
        {
            return Catch(expression1, expression2);
        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <returns></returns>
        public static OrderExpression Random()
        {
            return new RandomOrderExpression();
        }

        internal override string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters)
        {
            string[] array = new string[_orderExpressions.Count];
            List<DataParameter> list = new List<DataParameter>();
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                DataParameter[] dps = null;
                array[i] = _orderExpressions[i].CreateSqlString(factory, out dps);
                list.AddRange(dps);
            }
            dataParameters = list.ToArray();

            return factory.CreateCatchExpressionSql(array);
        }

        internal virtual string CreateSqlString(CommandFactory factory, out DataParameter[] dataParameters, GetAliasHandler handler)
        {
            string[] array = new string[_orderExpressions.Count];
            List<DataParameter> list = new List<DataParameter>();
            dataParameters = null;
            int len = array.Length;
            for (int i = 0; i < len; i++)
            {
                DataParameter[] dps = null;
                array[i] = _orderExpressions[i].CreateSqlString(factory, out dps, handler);
                list.AddRange(dps);
            }
            dataParameters = list.ToArray();
            return factory.CreateCatchExpressionSql(array);
        }

		/// <summary>
		/// Determines whether the specified <see cref="Light.Data.OrderExpression"/> is equal to the current <see cref="Light.Data.OrderExpression"/>.
		/// </summary>
		/// <param name="target">The <see cref="Light.Data.OrderExpression"/> to compare with the current <see cref="Light.Data.OrderExpression"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Light.Data.OrderExpression"/> is equal to the current
		/// <see cref="Light.Data.OrderExpression"/>; otherwise, <c>false</c>.</returns>
        public virtual bool Equals(OrderExpression target)
        {
            if (Object.Equals(target, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, target))
            {
                return true;
            }
            else
            {
                if (this.GetType() == target.GetType())
                {
                    if (Object.Equals(this._orderExpressions, target._orderExpressions))
                    {
                        return true;
                    }
                    else
                    {
                        if (this._orderExpressions.Count == target._orderExpressions.Count)
                        {
                            int len = this._orderExpressions.Count;
                            for (int i = 0; i < len; i++)
                            {
                                if (!this._orderExpressions[i].Equals(target._orderExpressions[i]))
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
