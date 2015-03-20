using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Light.Data
{
    /// <summary>
    /// 数据参数
    /// </summary>
    public class DataParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="direction">数据方向</param>
        public DataParameter(string paramName, object paramValue, string dbType, ParameterDirection direction)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentNullException("ParamName");
            }
            
            _parameterName = paramName;
            _dbType = dbType;
            _value = paramValue;//!= null ? paramValue : DBNull.Value;
            _direction = direction;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        public DataParameter(string paramName, object paramValue)
            : this(paramName, paramValue, null, ParameterDirection.Input)
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="direction">数据方向</param>
        public DataParameter(string paramName, object paramValue, ParameterDirection direction)
            : this(paramName, paramValue, null, direction)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="dbType">数据类型</param>
        public DataParameter(string paramName, object paramValue, string dbType)
            : this(paramName, paramValue, dbType, ParameterDirection.Input)
        {

        }

        IDataParameter _dataParameter = null;

        /// <summary>
        /// 设置IDataParameter,用以获取输出参数
        /// </summary>
        /// <param name="dataParameter"></param>
        internal void SetDataParameter(IDataParameter dataParameter)
        {
            _dataParameter = dataParameter;
        }

        string _parameterName;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get
            {

                return _parameterName;
            }
            internal set
            {
                _parameterName = value;
            }
        }

        object _value;
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get
            {
                return _value;
            }
            internal set
            {
                _value = value;
            }
        }

        string _dbType;
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DbType
        {
            get
            {
                
                return _dbType;
            }
            internal set
            {
                _dbType = value;
            }
        }

        ParameterDirection _direction;
        /// <summary>
        /// 数据方向
        /// </summary>
        public ParameterDirection Direction
        {
            get
            {
                return _direction;
            }
            internal set
            {
                _direction = value;
            }
        }
        /// <summary>
        /// 输出值
        /// </summary>
        public object OutputValue
        {
            get
            {
                if (_dataParameter == null)
                    return null;
               return _dataParameter.Value;
            }
        }

    }
}
