using System;
namespace Light.Data
{
	abstract class ExtendAggregateData : AggregateData
	{
		AggregateData _baseAggregateData = null;

		/// <summary>
		/// Gets the base field info.
		/// </summary>
		/// <value>The base field info.</value>
		public AggregateData BaseAggregateData {
			get {
				return _baseAggregateData;
			}
		}

		public ExtendAggregateData (AggregateData data)
			: base (data.TableMapping)
		{
			_baseAggregateData = data;
		}
	}
}

