using System;

namespace Light.Data
{
	class JoinItem
	{
		DataEntityMapping mapping;

		public DataEntityMapping Mapping {
			get {
				return mapping;
			}
		}

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

		public JoinItem (DataEntityMapping mapping, DataFieldInfo[] infos, DataFieldExpression expression)
		{
			if (mapping == null)
				throw new ArgumentNullException ("mapping");
			if (infos == null)
				throw new ArgumentNullException ("infos");
			if (expression == null)
				throw new ArgumentNullException ("expression");
			this.mapping = mapping;
			this.infos = infos;
			this.expression = expression;
		}
	}
}

