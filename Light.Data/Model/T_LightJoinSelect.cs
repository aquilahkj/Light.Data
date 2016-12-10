using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, T5, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, T5, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, T5, T6, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
		where T6 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, T5, T6, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, T5, T6, T7, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
		where T6 : class
		where T7 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, T5, T6, T7, T8, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
		where T6 : class
		where T7 : class
		where T8 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, T8, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

	/// <summary>
	/// Join table.
	/// </summary>		
	class LightJoinSelect<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, K> : LightJoinSelect<K>
		where T : class
		where T1 : class
		where T2 : class
		where T3 : class
		where T4 : class
		where T5 : class
		where T6 : class
		where T7 : class
		where T8 : class
		where T9 : class
		where K : class
	{
		public LightJoinSelect (DataContext context, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, K>> expression, List<IJoinModel> models, List<IMap> maps, QueryExpression query, OrderExpression order, bool distinct, Region region, SafeLevel level)
			:base(context, expression, models, maps, query, order, distinct, region, level)
		{

		}
	}

}


