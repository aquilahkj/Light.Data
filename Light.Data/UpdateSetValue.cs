using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// 批量更新设置值
	/// </summary>
	public class UpdateSetValue
	{
		DataFieldInfo _dataField = null;

		object _value = null;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="dataField">要更新的数据字段</param>
		/// <param name="value">更新值</param>
		public UpdateSetValue (DataFieldInfo dataField, object value)
		{
			if (Object.Equals (dataField, null)) {
				throw new ArgumentNullException ("DataField");
			}
			_dataField = dataField;
			_value = value;
		}

		/// <summary>
		/// 数据字段
		/// </summary>
		public DataFieldInfo DataField {
			get {
				return _dataField;
			}
		}

		/// <summary>
		/// 数据值
		/// </summary>
		public object Value {
			get {
				return _value;
			}
		}

		internal DataParameter CreateDataParameter (CommandFactory factory)
		{
			string pn = factory.CreateTempParamName ();
			DataParameter dataParameter = new DataParameter (pn, _dataField.DataField.ToColumn (_value), _dataField.DataField.DBType);
			return dataParameter;
		}

		//static DataFieldInfo<T> ChangeType(DataFieldInfo dataField)
		//{
		//    if (Object.Equals(dataField, null))
		//    {
		//        return null;
		//    }
		//    else
		//    {
		//        DataFieldInfo<T> dataFiledT = dataField as DataFieldInfo<T>;
		//        if (Object.Equals(dataFiledT, null))
		//        {
		//            throw new LightDataException(RE.UpdateFieldTypeIsError);
		//        }
		//        else
		//        {
		//            return dataFiledT;
		//        }
		//    }
		//}


		//public void Set(DataFieldInfo<T> dataField, object value)
		//{
		//    if (Object.Equals(dataField, null))
		//    {
		//        throw new ArgumentNullException("DataField");
		//    }
		//}

		//public void Set(DataFieldInfo dataField, object value)
		//{
		//    if (Object.Equals(dataField, null))
		//    {
		//        throw new ArgumentNullException("DataField");
		//    }
		//    DataFieldInfo<T> dataFiledT = dataField as DataFieldInfo<T>;
		//    if (Object.Equals(dataFiledT, null))
		//    {
		//        Type type = typeof(T);
		//    }
		//}
	}
}
