using System;

namespace Light.Data
{
	class AggregateFunctionInfo
	{
		readonly AggregateFunction function;

		public AggregateFunction Function {
			get {
				return function;
			}
		}

		readonly string name;

		public string Name {
			get {
				return name;
			}
		}

		public AggregateFunctionInfo (AggregateFunction function, string name)
		{
			if (Object.Equals (function, null))
				throw new ArgumentNullException (nameof (function));
			if (string.IsNullOrEmpty (name))
				throw new ArgumentNullException (nameof (name));
			this.name = name;
			this.function = function;
		}
	}
}

