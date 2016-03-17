
namespace Light.Data
{
	/// <summary>
	/// Interface of relation field config.
	/// </summary>
    interface IRelationFieldConfig
    {
		RelationKey[]  GetRelationKeys();

		/// <summary>
		/// Gets the relation key count.
		/// </summary>
		/// <value>The relation key count.</value>
        int RelationKeyCount
        {
            get;
        }

		/// <summary>
		/// Gets the relation mode.
		/// </summary>
		/// <value>The relation mode.</value>
		RelationMode RelationMode {
			get;
		}
    }
}
