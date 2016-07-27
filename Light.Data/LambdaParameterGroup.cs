using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	class LambdaParameterGroup
	{
		readonly Dictionary<string, DataEntityMapping> dict = new Dictionary<string, DataEntityMapping> ();

		public LambdaParameterGroup ()
		{

		}

		public LambdaParameterGroup (LambdaExpression expression)
		{
			foreach (ParameterExpression parameter in expression.Parameters) {
				string name = parameter.Name;
				Type type = parameter.Type;
				dict [name] = DataMapping.GetEntityMapping (type);
			}
		}

		public void Set (string name, Type type)
		{
			if (name == null)
				throw new ArgumentNullException (nameof (name));

			if (type == null)
				throw new ArgumentNullException (nameof (type));
			dict [name] = DataMapping.GetEntityMapping (type);
		}

		public bool TryGet (string name, out DataEntityMapping mapping)
		{
			return dict.TryGetValue (name, out mapping);
		}

		public DataEntityMapping mapping (string name)
		{
			return dict [name];
		}
	}
}

