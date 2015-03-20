using System;
using System.Collections.Generic;
using System.Text;
using Light.Data.Config;

namespace Light.Data
{
	/// <summary>
	/// 数据表关联属性,只能在DataEntity基类中有效
	/// </summary>
	[AttributeUsage (AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
	public class RelationAttribute : Attribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="masterKey">主表关联键</param>
		/// <param name="relateKey">关联表关联键</param>
		public RelationAttribute (string masterKey, string relateKey)
		{
			if (string.IsNullOrEmpty (masterKey)) {
				throw new ArgumentNullException ("MasterKey");
			}
			if (string.IsNullOrEmpty (relateKey)) {
				throw new ArgumentNullException ("RelateKey");
			}
			MasterKey = masterKey;
			RelateKey = relateKey;
		}

		/// <summary>
		/// 主表关联键
		/// </summary>
		public string MasterKey {
			get;
			private set;
		}

		/// <summary>
		/// 关联表关联键
		/// </summary>
		public string RelateKey {
			get;
			private set;
		}
	}
}
