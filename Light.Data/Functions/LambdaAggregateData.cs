using System;
namespace Light.Data
{
	abstract class LambdaAggregateData : ExtendAggregateData
	{
		public LambdaAggregateData (AggregateData data)
			: base (data)
		{
		}
	}
}

