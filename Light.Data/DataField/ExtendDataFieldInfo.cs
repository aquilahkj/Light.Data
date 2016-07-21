using System;

namespace Light.Data
{
	/// <summary>
	/// Extend data field info. use database function library.
	/// </summary>
	public abstract class ExtendDataFieldInfo : DataFieldInfo
	{
		DataFieldInfo _baseFieldInfo = null;

		/// <summary>
		/// Gets the base field info.
		/// </summary>
		/// <value>The base field info.</value>
		public DataFieldInfo BaseFieldInfo {
			get {
				return _baseFieldInfo;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.ExtendDataFieldInfo"/> class.
		/// </summary>
		/// <param name="info">Info.</param>
		internal ExtendDataFieldInfo (DataFieldInfo info)
			: base (info.DataField)
		{
			_baseFieldInfo = info;
		}



		/// <summary>
		/// Equalses the detail.
		/// </summary>
		/// <returns>true</returns>
		/// <c>false</c>
		/// <param name="info">Info.</param>
		protected override bool EqualsDetail (DataFieldInfo info)
		{
			ExtendDataFieldInfo target = info as ExtendDataFieldInfo;
			if (!Object.Equals (target, null)) {
				return this._baseFieldInfo.Equals (target._baseFieldInfo);
			}
			else {
				return false;
			}
		}
	}
}
