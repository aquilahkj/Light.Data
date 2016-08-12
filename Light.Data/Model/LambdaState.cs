using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	abstract class LambdaState
	{
		public abstract bool CheckPamramter (string name, Type type);

		public abstract DataFieldInfo GetDataFileInfo (string fullPath);

		public abstract LambdaPathType ParsePath (string fullPath);

		public abstract ISelector CreateSelector (string[] fullPaths);


		bool mutliQuery;

		public bool MutliQuery {
			get {
				return mutliQuery;
			}

			set {
				mutliQuery = value;
			}
		}



	}

	enum LambdaPathType
	{
		None,
		Parameter,
		Field,
		RelateEntity,
		RelateCollection
	}
}

