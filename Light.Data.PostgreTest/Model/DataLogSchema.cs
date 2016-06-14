using System;
namespace Light.Data.PostgreTest
{
	public interface ITeDataLog
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		int Id { get; set; }

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		int UserId { get; set; }

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		int ArticleId { get; set; }

		/// <summary>
		/// RecordTime
		/// </summary>
		/// <value></value>
		DateTime RecordTime { get; set; }

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		int Status { get; set; }

		/// <summary>
		/// Action
		/// </summary>
		/// <value></value>
		int Action { get; set; }

		/// <summary>
		/// RequestUrl
		/// </summary>
		/// <value></value>
		string RequestUrl { get; set; }

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		int? CheckId { get; set; }

		/// <summary>
		/// CheckPoint
		/// </summary>
		/// <value></value>
		double? CheckPoint { get; set; }

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		DateTime? CheckTime { get; set; }

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		string CheckData { get; set; }

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		CheckLevelType? CheckLevelTypeInt { get; set; }

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		CheckLevelType? CheckLevelTypeString { get; set; }
	}

	public partial class TeDataLog : ITeDataLog
	{

	}

	public partial class TeDataLogHistory : ITeDataLog
	{

	}

	public partial class TeDataLogHistory2 : ITeDataLog
	{

	}
}

