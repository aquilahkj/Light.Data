using System;
using System.Linq.Expressions;

namespace Light.Data
{
	/// <summary>
	/// Aggregate function.
	/// </summary>
	public interface IAggregateFunction<T>
	{
		/// <summary>
		/// Set the safe mode.
		/// </summary>
		/// <returns>The mode.</returns>
		/// <param name="level">Level.</param>
		IAggregateFunction<T> SafeMode (SafeLevel level);

		/// <summary>
		/// Set the distinct.
		/// </summary>
		/// <returns>The distinct.</returns>
		/// <param name="distinct">If set to <c>true</c> distinct.</param>
		IAggregateFunction<T> SetDistinct (bool distinct);

		#region Count
		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, string>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, bool>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, bool?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, byte>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, byte?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, sbyte>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, sbyte?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, short>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, short?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, int>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, int?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, uint>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, long>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, long?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, double>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, double?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, float>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, float?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, decimal?>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, DateTime>> field);

		/// <summary>
		/// Count the specified field.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		int Count (Expression<Func<T, DateTime?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, string>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, bool>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, bool?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, byte>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, byte?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, sbyte>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, sbyte?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, short>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, short?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, int>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, int?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, uint>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, long>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, long?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, double>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, double?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, float>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, float?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, decimal?>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, DateTime>> field);

		/// <summary>
		/// Count the specified expression to long value.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="field">Field.</param>
		long LongCount (Expression<Func<T, DateTime?>> field);

		#endregion

		#region Sum
		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		int Sum (Expression<Func<T, short>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		int? Sum (Expression<Func<T, short?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		uint Sum (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		uint? Sum (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long LongSum (Expression<Func<T, short>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long? LongSum (Expression<Func<T, short?>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong LongSum (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong? LongSum (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		int Sum (Expression<Func<T, int>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		int? Sum (Expression<Func<T, int?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		uint Sum (Expression<Func<T, uint>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		uint? Sum (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long LongSum (Expression<Func<T, int>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long? LongSum (Expression<Func<T, int?>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong LongSum (Expression<Func<T, uint>> field);

		/// <summary>
		/// Sum the specified expression to long value.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong? LongSum (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long Sum (Expression<Func<T, long>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		long? Sum (Expression<Func<T, long?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong Sum (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		ulong? Sum (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		double Sum (Expression<Func<T, double>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		double? Sum (Expression<Func<T, double?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		float Sum (Expression<Func<T, float>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		float? Sum (Expression<Func<T, float?>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		decimal Sum (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Sum the specified field.
		/// </summary>
		/// <returns>The sum.</returns>
		/// <param name="field">Field.</param>
		decimal? Sum (Expression<Func<T, decimal?>> field);

		#endregion

		#region Avg
		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, short>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, short?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, int>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, int?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, uint>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, long>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, long?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, double>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, double?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double Avg (Expression<Func<T, float>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		double? Avg (Expression<Func<T, float?>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		decimal Avg (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Avg the specified field.
		/// </summary>
		/// <returns>The avg.</returns>
		/// <param name="field">Field.</param>
		decimal? Avg (Expression<Func<T, decimal?>> field);

		#endregion

		#region Max
		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		short Max (Expression<Func<T, short>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		short? Max (Expression<Func<T, short?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		ushort Max (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		ushort? Max (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		int Max (Expression<Func<T, int>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		int? Max (Expression<Func<T, int?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		uint Max (Expression<Func<T, uint>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		uint? Max (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		long Max (Expression<Func<T, long>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		long? Max (Expression<Func<T, long?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		ulong Max (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		ulong? Max (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		double Max (Expression<Func<T, double>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		double? Max (Expression<Func<T, double?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		float Max (Expression<Func<T, float>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		float? Max (Expression<Func<T, float?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		decimal Max (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		decimal? Max (Expression<Func<T, decimal?>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		DateTime Max (Expression<Func<T, DateTime>> field);

		/// <summary>
		/// Max the specified field.
		/// </summary>
		/// <returns>The max.</returns>
		/// <param name="field">Field.</param>
		DateTime? Max (Expression<Func<T, DateTime?>> field);

		#endregion

		#region Max
		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		short Min (Expression<Func<T, short>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		short? Min (Expression<Func<T, short?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		ushort Min (Expression<Func<T, ushort>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		ushort? Min (Expression<Func<T, ushort?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		int Min (Expression<Func<T, int>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		int? Min (Expression<Func<T, int?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		uint Min (Expression<Func<T, uint>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		uint? Min (Expression<Func<T, uint?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		long Min (Expression<Func<T, long>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		long? Min (Expression<Func<T, long?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		ulong Min (Expression<Func<T, ulong>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		ulong? Min (Expression<Func<T, ulong?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		double Min (Expression<Func<T, double>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		double? Min (Expression<Func<T, double?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		float Min (Expression<Func<T, float>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		float? Min (Expression<Func<T, float?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		decimal Min (Expression<Func<T, decimal>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		decimal? Min (Expression<Func<T, decimal?>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		DateTime Min (Expression<Func<T, DateTime>> field);

		/// <summary>
		/// Minimum the specified field.
		/// </summary>
		/// <returns>The minimum.</returns>
		/// <param name="field">Field.</param>
		DateTime? Min (Expression<Func<T, DateTime?>> field);

		#endregion
	}
}
