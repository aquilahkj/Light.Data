using System;
using System.Collections.Generic;
using System.Data;

namespace Light.Data
{
	public class ParameterState
	{

		class ObjectData
		{
			public string Full;

			public string Normal;
		}

		int seed = 0;

		string GetNextParameterName ()
		{
			seed++;
			return "P" + seed;
		}

		Dictionary<object, ObjectData> dict = new Dictionary<object, ObjectData> ();

		List<DataParameter> parameters = new List<DataParameter> ();

		public string GetSql (object obj, bool isFullName)
		{
			ObjectData data;
			if (dict.TryGetValue (obj, out data)) {
				if (isFullName) {
					return data.Full;
				}
				else {
					return data.Normal;
				}
			}
			else {
				return null;
			}
		}

		public void SetSql (object obj, bool isFullName, string sql)
		{
			ObjectData data;
			if (dict.TryGetValue (obj, out data)) {
				if (isFullName) {
					if (data.Full != null)
						data.Full = sql;
				}
				else {
					if (data.Normal != null)
						data.Normal = sql;
				}
			}
			else {
				data = new ObjectData ();
				if (isFullName) {
					data.Full = sql;
				}
				else {
					data.Normal = sql;
				}
				dict [obj] = data;
			}
		}

		/// <summary>
		/// Adds the data parameter.
		/// </summary>
		/// <returns>The data parameter.</returns>
		/// <param name="paramValue">Parameter value.</param>
		/// <param name="dbType">Db type.</param>
		/// <param name="direction">Direction.</param>
		public string AddDataParameter (object paramValue, string dbType, ParameterDirection direction)
		{
			string paramName = GetNextParameterName ();
			DataParameter dataParameter = new DataParameter (paramName, paramValue, dbType, direction);
			parameters.Add (dataParameter);
			return paramName;
		}

		/// <summary>
		/// Adds the data parameter.
		/// </summary>
		/// <returns>The data parameter.</returns>
		/// <param name="paramValue">Parameter value.</param>
		public string AddDataParameter (object paramValue)
		{
			return AddDataParameter (paramValue, null, ParameterDirection.Input);
		}

		/// <summary>
		/// Adds the data parameter.
		/// </summary>
		/// <returns>The data parameter.</returns>
		/// <param name="paramValue">Parameter value.</param>
		/// <param name="direction">Direction.</param>
		public string AddDataParameter (object paramValue, ParameterDirection direction)
		{
			return AddDataParameter (paramValue, null, direction);
		}

		/// <summary>
		/// Adds the data parameter.
		/// </summary>
		/// <returns>The data parameter.</returns>
		/// <param name="paramValue">Parameter value.</param>
		/// <param name="dbType">Db type.</param>
		public string AddDataParameter (object paramValue, string dbType)
		{
			return AddDataParameter (paramValue, dbType, ParameterDirection.Input);
		}

		public void SetData (object obj, bool isFullName, string sql)
		{

		}
	}
}

