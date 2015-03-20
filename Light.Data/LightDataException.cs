using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	/// <summary>
	/// LightData异常
	/// </summary>
	public class LightDataException : Exception
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常信息</param>
		public LightDataException (string message)
			: base (message)
		{

		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常信息</param>
		/// <param name="innerException">子异常</param>
		public LightDataException (string message, Exception innerException)
			: base (message, innerException)
		{

		}
	}
}
