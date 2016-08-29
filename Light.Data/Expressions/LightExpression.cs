
namespace Light.Data
{
	/// <summary>
	/// Base expression.
	/// </summary>
	public abstract class LightExpression
	{
		/// <summary>
		/// Gets or sets the table mapping.
		/// </summary>
		/// <value>The table mapping.</value>
		internal DataEntityMapping TableMapping {
			get;
			set;
		}

		/// <summary>
		/// Creates the sql string.
		/// </summary>
		/// <returns>The sql string.</returns>
		/// <param name="factory">Factory.</param>
		/// <param name="isFullName">If set to <c>true</c> full field name.</param>
		/// <param name="dataParameters">Data parameters.</param>
		//internal abstract string CreateSqlString (CommandFactory factory, bool isFullName, out DataParameter[] dataParameters);

		internal abstract string CreateSqlString (CommandFactory factory, bool isFullName, CreateSqlState state);
	}
}
