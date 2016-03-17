using System;

namespace Light.Data
{
	/// <summary>
	/// Data entity.
	/// </summary>
	public abstract class DataEntity
	{
		DataContext _context = null;

		/// <summary>
		/// Gets the context.
		/// </summary>
		/// <value>The context.</value>
		internal DataContext Context {
			get {
				return _context;
			}
		}

		/// <summary>
		/// Sets the context.
		/// </summary>
		/// <param name="context">Context.</param>
		public void SetContext (DataContext context)
		{
			if (context == null) {
				throw new ArgumentNullException ("context");
			}
			_context = context;
		}

		internal virtual void LoadDataComplete ()
		{

		}

	}
}
