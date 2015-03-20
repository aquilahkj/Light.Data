using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Data
{
	enum QueryPredicate
	{
		Eq,
		Lt,
		LtEq,
		Gt,
		GtEq,
		NotEq
	}

	enum QueryCollectionPredicate
	{
		In,
		NotIn,
		GtAll,
		LtAll,
		GtAny,
		LtAny
	}
}
