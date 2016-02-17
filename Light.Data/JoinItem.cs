using System;

namespace Light.Data
{
	class JoinItem
	{
		readonly DataFieldExpression expression;

		public DataFieldExpression Expression {
			get {
				return expression;
			}
		}

		readonly DataFieldInfo[] infos;

		public DataFieldInfo[] Infos {
			get {
				return infos;
			}
		}

		public JoinItem (DataFieldInfo[] infos, DataFieldExpression expression)
		{
			if (infos == null)
				throw new ArgumentNullException ("infos");
			if (expression == null)
				throw new ArgumentNullException ("expression");
			this.infos = infos;
			this.expression = expression;
		}
	}
}

