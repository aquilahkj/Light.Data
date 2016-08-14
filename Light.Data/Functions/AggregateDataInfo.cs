using System;

namespace Light.Data
{
	class AggregateDataInfo
	{
		readonly AggregateData data;

		public AggregateData Data {
			get {
				return data;
			}
		}

		readonly string name;

		public string Name {
			get {
				return name;
			}
		}

		public AggregateDataInfo (AggregateData data, string name)
		{
			if (Object.Equals (data, null))
				throw new ArgumentNullException (nameof (data));
			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException (nameof (name));
			this.name = name;
			this.data = data;
		}
	}
}

