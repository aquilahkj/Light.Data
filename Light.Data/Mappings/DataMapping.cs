using System;
using System.Collections.Generic;
using System.Data;

using Light.Data;

namespace Light.Data
{
	/// <summary>
	/// Data mapping.
	/// </summary>
	abstract class DataMapping : IDataDefine//IFieldCollection
	{

		//protected Dictionary<string, FieldMapping> _fieldMappingDictionary = new Dictionary<string, FieldMapping> ();

		//protected List<FieldMapping> _fieldList = new List<FieldMapping> ();

		/// <summary>
		/// Initializes a new instance of the <see cref="Light.Data.DataMapping"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		protected DataMapping (Type type)
		{
			this.objectType = type;
		}

		Type objectType;

		/// <summary>
		/// Gets or sets the type of the object.
		/// </summary>
		/// <value>The type of the object.</value>
		public Type ObjectType {
			get {
				return objectType;
			}
			protected set {
				objectType = value;
			}
		}

		ExtendParamCollection extentParams;

		/// <summary>
		/// Gets or sets the extent parameters.
		/// </summary>
		/// <value>The extent parameters.</value>
		public ExtendParamCollection ExtentParams {
			get {
				return extentParams;
			}
			protected set {
				extentParams = value;
			}
		}

		#region IFieldCollection 成员

		//public IEnumerable<FieldMapping> FieldMappings {
		//	get {
		//		foreach (FieldMapping item in this._fieldList) {
		//			yield return item;
		//		}
		//	}
		//}

		//public int FieldCount {
		//	get {
		//		return this._fieldList.Count;
		//	}
		//}

		//public virtual FieldMapping FindFieldMapping (string fieldName)
		//{
		//	FieldMapping mapping;
		//	_fieldMappingDictionary.TryGetValue (fieldName, out mapping);
		//	return mapping;
		//}

		public abstract object LoadData (DataContext context, IDataReader datareader, object state);

		public abstract object InitialData ();

		#endregion

		public override string ToString ()
		{
			return string.Format ("[DataMapping: ObjectType={0}, ExtentParams={1}]", ObjectType, ExtentParams);
		}
	}
}
