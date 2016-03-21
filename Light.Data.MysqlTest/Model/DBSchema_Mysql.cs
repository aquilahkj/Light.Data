using System;
using System.Collections.Generic;
using System.Text;
using Light.Data;

namespace Light.Data.MysqlTest
{
    [Serializable]
    [DataTable("MqDeviceInfo")]
    public partial class MqDeviceInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo imeiField = DataFieldInfo<MqDeviceInfo>.Create("Imei");

		static readonly DataFieldInfo flagField = DataFieldInfo<MqDeviceInfo>.Create("Flag");

		static readonly DataFieldInfo statusField = DataFieldInfo<MqDeviceInfo>.Create("Status");

		static readonly DataFieldInfo validField = DataFieldInfo<MqDeviceInfo>.Create("Valid");

		static readonly DataFieldInfo registerTimeField = DataFieldInfo<MqDeviceInfo>.Create("RegisterTime");

		static readonly DataFieldInfo remarkField = DataFieldInfo<MqDeviceInfo>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo ImeiField
        {
            get {
                return imeiField;
            }
        }

		public static DataFieldInfo FlagField
        {
            get {
                return flagField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ValidField
        {
            get {
                return validField;
            }
        }

		public static DataFieldInfo RegisterTimeField
        {
            get {
                return registerTimeField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string imei;

		/// <summary>
		/// Imei
		/// </summary>
		/// <value></value>
		[DataField("Imei", IsPrimaryKey = true)]
        public string Imei
        {
            get { 
            	return this.imei; 
            }
            set { 
            	this.imei = value; 
            }
        }
		private string flag;

		/// <summary>
		/// Flag
		/// </summary>
		/// <value></value>
		[DataField("Flag")]
        public string Flag
        {
            get { 
            	return this.flag; 
            }
            set { 
            	this.flag = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int valid;

		/// <summary>
		/// Valid
		/// </summary>
		/// <value></value>
		[DataField("Valid")]
        public int Valid
        {
            get { 
            	return this.valid; 
            }
            set { 
            	this.valid = value; 
            }
        }
		private DateTime registerTime;

		/// <summary>
		/// RegisterTime
		/// </summary>
		/// <value></value>
		[DataField("RegisterTime")]
        public DateTime RegisterTime
        {
            get { 
            	return this.registerTime; 
            }
            set { 
            	this.registerTime = value; 
            }
        }
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_AreaInfo")]
    public partial class TeAreaInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeAreaInfo>.Create("Id");

		static readonly DataFieldInfo nameField = DataFieldInfo<TeAreaInfo>.Create("Name");

		static readonly DataFieldInfo v1Field = DataFieldInfo<TeAreaInfo>.Create("V1");

		static readonly DataFieldInfo v2Field = DataFieldInfo<TeAreaInfo>.Create("V2");

		static readonly DataFieldInfo v3Field = DataFieldInfo<TeAreaInfo>.Create("V3");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo NameField
        {
            get {
                return nameField;
            }
        }

		public static DataFieldInfo V1Field
        {
            get {
                return v1Field;
            }
        }

		public static DataFieldInfo V2Field
        {
            get {
                return v2Field;
            }
        }

		public static DataFieldInfo V3Field
        {
            get {
                return v3Field;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private string name;

		/// <summary>
		/// Name
		/// </summary>
		/// <value></value>
		[DataField("Name")]
        public string Name
        {
            get { 
            	return this.name; 
            }
            set { 
            	this.name = value; 
            }
        }
		private int v1;

		/// <summary>
		/// V1
		/// </summary>
		/// <value></value>
		[DataField("V1")]
        public int V1
        {
            get { 
            	return this.v1; 
            }
            set { 
            	this.v1 = value; 
            }
        }
		private int v2;

		/// <summary>
		/// V2
		/// </summary>
		/// <value></value>
		[DataField("V2")]
        public int V2
        {
            get { 
            	return this.v2; 
            }
            set { 
            	this.v2 = value; 
            }
        }
		private int v3;

		/// <summary>
		/// V3
		/// </summary>
		/// <value></value>
		[DataField("V3")]
        public int V3
        {
            get { 
            	return this.v3; 
            }
            set { 
            	this.v3 = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_Article")]
    public partial class TeArticle : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeArticle>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeArticle>.Create("UserId");

		static readonly DataFieldInfo titleField = DataFieldInfo<TeArticle>.Create("Title");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeArticle>.Create("Content");

		static readonly DataFieldInfo publishTimeField = DataFieldInfo<TeArticle>.Create("PublishTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticle>.Create("Status");

		static readonly DataFieldInfo readNumField = DataFieldInfo<TeArticle>.Create("ReadNum");

		static readonly DataFieldInfo praiseNumField = DataFieldInfo<TeArticle>.Create("PraiseNum");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<TeArticle>.Create("LastModifyTime");

		static readonly DataFieldInfo lastCommentTimeField = DataFieldInfo<TeArticle>.Create("LastCommentTime");

		static readonly DataFieldInfo tagsField = DataFieldInfo<TeArticle>.Create("Tags");

		static readonly DataFieldInfo columnIdField = DataFieldInfo<TeArticle>.Create("ColumnId");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo TitleField
        {
            get {
                return titleField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo PublishTimeField
        {
            get {
                return publishTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ReadNumField
        {
            get {
                return readNumField;
            }
        }

		public static DataFieldInfo PraiseNumField
        {
            get {
                return praiseNumField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
            }
        }

		public static DataFieldInfo LastCommentTimeField
        {
            get {
                return lastCommentTimeField;
            }
        }

		public static DataFieldInfo TagsField
        {
            get {
                return tagsField;
            }
        }

		public static DataFieldInfo ColumnIdField
        {
            get {
                return columnIdField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string title;

		/// <summary>
		/// Title
		/// </summary>
		/// <value></value>
		[DataField("Title")]
        public string Title
        {
            get { 
            	return this.title; 
            }
            set { 
            	this.title = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		private DateTime publishTime;

		/// <summary>
		/// PublishTime
		/// </summary>
		/// <value></value>
		[DataField("PublishTime")]
        public DateTime PublishTime
        {
            get { 
            	return this.publishTime; 
            }
            set { 
            	this.publishTime = value; 
            }
        }
		private string status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public string Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int readNum;

		/// <summary>
		/// ReadNum
		/// </summary>
		/// <value></value>
		[DataField("ReadNum")]
        public int ReadNum
        {
            get { 
            	return this.readNum; 
            }
            set { 
            	this.readNum = value; 
            }
        }
		private int praiseNum;

		/// <summary>
		/// PraiseNum
		/// </summary>
		/// <value></value>
		[DataField("PraiseNum")]
        public int PraiseNum
        {
            get { 
            	return this.praiseNum; 
            }
            set { 
            	this.praiseNum = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// LastModifyTime
		/// </summary>
		/// <value></value>
		[DataField("LastModifyTime")]
        public DateTime LastModifyTime
        {
            get { 
            	return this.lastModifyTime; 
            }
            set { 
            	this.lastModifyTime = value; 
            }
        }
		private DateTime lastCommentTime;

		/// <summary>
		/// LastCommentTime
		/// </summary>
		/// <value></value>
		[DataField("LastCommentTime")]
        public DateTime LastCommentTime
        {
            get { 
            	return this.lastCommentTime; 
            }
            set { 
            	this.lastCommentTime = value; 
            }
        }
		private string tags;

		/// <summary>
		/// Tags
		/// </summary>
		/// <value></value>
		[DataField("Tags", IsNullable = true)]
        public string Tags
        {
            get { 
            	return this.tags; 
            }
            set { 
            	this.tags = value; 
            }
        }
		private string columnId;

		/// <summary>
		/// ColumnId
		/// </summary>
		/// <value></value>
		[DataField("ColumnId", IsNullable = true)]
        public string ColumnId
        {
            get { 
            	return this.columnId; 
            }
            set { 
            	this.columnId = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_ArticleColumn")]
    public partial class TeArticleColumn : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo columnIdField = DataFieldInfo<TeArticleColumn>.Create("ColumnId");

		static readonly DataFieldInfo columnNameField = DataFieldInfo<TeArticleColumn>.Create("ColumnName");

		static readonly DataFieldInfo parentIdField = DataFieldInfo<TeArticleColumn>.Create("ParentId");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticleColumn>.Create("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeArticleColumn>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo ColumnIdField
        {
            get {
                return columnIdField;
            }
        }

		public static DataFieldInfo ColumnNameField
        {
            get {
                return columnNameField;
            }
        }

		public static DataFieldInfo ParentIdField
        {
            get {
                return parentIdField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string columnId;

		/// <summary>
		/// ColumnId
		/// </summary>
		/// <value></value>
		[DataField("ColumnId", IsPrimaryKey = true)]
        public string ColumnId
        {
            get { 
            	return this.columnId; 
            }
            set { 
            	this.columnId = value; 
            }
        }
		private string columnName;

		/// <summary>
		/// ColumnName
		/// </summary>
		/// <value></value>
		[DataField("ColumnName")]
        public string ColumnName
        {
            get { 
            	return this.columnName; 
            }
            set { 
            	this.columnName = value; 
            }
        }
		private string parentId;

		/// <summary>
		/// ParentId
		/// </summary>
		/// <value></value>
		[DataField("ParentId")]
        public string ParentId
        {
            get { 
            	return this.parentId; 
            }
            set { 
            	this.parentId = value; 
            }
        }
		private string status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public string Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_ArticleComment")]
    public partial class TeArticleComment : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeArticleComment>.Create("Id");

		static readonly DataFieldInfo articleIdField = DataFieldInfo<TeArticleComment>.Create("ArticleId");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeArticleComment>.Create("UserId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeArticleComment>.Create("Content");

		static readonly DataFieldInfo publishTimeField = DataFieldInfo<TeArticleComment>.Create("PublishTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeArticleComment>.Create("Status");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo ArticleIdField
        {
            get {
                return articleIdField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo PublishTimeField
        {
            get {
                return publishTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int articleId;

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		[DataField("ArticleId")]
        public int ArticleId
        {
            get { 
            	return this.articleId; 
            }
            set { 
            	this.articleId = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		private DateTime publishTime;

		/// <summary>
		/// PublishTime
		/// </summary>
		/// <value></value>
		[DataField("PublishTime")]
        public DateTime PublishTime
        {
            get { 
            	return this.publishTime; 
            }
            set { 
            	this.publishTime = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_CheckValue")]
    public partial class TeCheckValue : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeCheckValue>.Create("Id");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeCheckValue>.Create("CheckId");

		static readonly DataFieldInfo checkRateField = DataFieldInfo<TeCheckValue>.Create("CheckRate");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeCheckValue>.Create("CheckTime");

		static readonly DataFieldInfo checkDateField = DataFieldInfo<TeCheckValue>.Create("CheckDate");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeCheckValue>.Create("CheckData");

		static readonly DataFieldInfo checkLevelField = DataFieldInfo<TeCheckValue>.Create("CheckLevel");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo CheckIdField
        {
            get {
                return checkIdField;
            }
        }

		public static DataFieldInfo CheckRateField
        {
            get {
                return checkRateField;
            }
        }

		public static DataFieldInfo CheckTimeField
        {
            get {
                return checkTimeField;
            }
        }

		public static DataFieldInfo CheckDateField
        {
            get {
                return checkDateField;
            }
        }

		public static DataFieldInfo CheckDataField
        {
            get {
                return checkDataField;
            }
        }

		public static DataFieldInfo CheckLevelField
        {
            get {
                return checkLevelField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField("CheckId")]
        public int CheckId
        {
            get { 
            	return this.checkId; 
            }
            set { 
            	this.checkId = value; 
            }
        }
		private double checkRate;

		/// <summary>
		/// CheckRate
		/// </summary>
		/// <value></value>
		[DataField("CheckRate")]
        public double CheckRate
        {
            get { 
            	return this.checkRate; 
            }
            set { 
            	this.checkRate = value; 
            }
        }
		private DateTime checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField("CheckTime")]
        public DateTime CheckTime
        {
            get { 
            	return this.checkTime; 
            }
            set { 
            	this.checkTime = value; 
            }
        }
		private DateTime checkDate;

		/// <summary>
		/// CheckDate
		/// </summary>
		/// <value></value>
		[DataField("CheckDate")]
        public DateTime CheckDate
        {
            get { 
            	return this.checkDate; 
            }
            set { 
            	this.checkDate = value; 
            }
        }
		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField("CheckData")]
        public string CheckData
        {
            get { 
            	return this.checkData; 
            }
            set { 
            	this.checkData = value; 
            }
        }
		private int checkLevel;

		/// <summary>
		/// CheckLevel
		/// </summary>
		/// <value></value>
		[DataField("CheckLevel")]
        public int CheckLevel
        {
            get { 
            	return this.checkLevel; 
            }
            set { 
            	this.checkLevel = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_DataLog")]
    public partial class TeDataLog : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeDataLog>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeDataLog>.Create("UserId");

		static readonly DataFieldInfo articleIdField = DataFieldInfo<TeDataLog>.Create("ArticleId");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<TeDataLog>.Create("RecordTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeDataLog>.Create("Status");

		static readonly DataFieldInfo actionField = DataFieldInfo<TeDataLog>.Create("Action");

		static readonly DataFieldInfo requestUrlField = DataFieldInfo<TeDataLog>.Create("RequestUrl");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeDataLog>.Create("CheckId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeDataLog>.Create("CheckPoint");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeDataLog>.Create("CheckTime");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeDataLog>.Create("CheckData");

		static readonly DataFieldInfo checkLevelTypeIntField = DataFieldInfo<TeDataLog>.Create("CheckLevelTypeInt");

		static readonly DataFieldInfo checkLevelTypeStringField = DataFieldInfo<TeDataLog>.Create("CheckLevelTypeString");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo ArticleIdField
        {
            get {
                return articleIdField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ActionField
        {
            get {
                return actionField;
            }
        }

		public static DataFieldInfo RequestUrlField
        {
            get {
                return requestUrlField;
            }
        }

		public static DataFieldInfo CheckIdField
        {
            get {
                return checkIdField;
            }
        }

		public static DataFieldInfo CheckPointField
        {
            get {
                return checkPointField;
            }
        }

		public static DataFieldInfo CheckTimeField
        {
            get {
                return checkTimeField;
            }
        }

		public static DataFieldInfo CheckDataField
        {
            get {
                return checkDataField;
            }
        }

		public static DataFieldInfo CheckLevelTypeIntField
        {
            get {
                return checkLevelTypeIntField;
            }
        }

		public static DataFieldInfo CheckLevelTypeStringField
        {
            get {
                return checkLevelTypeStringField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private int articleId;

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		[DataField("ArticleId")]
        public int ArticleId
        {
            get { 
            	return this.articleId; 
            }
            set { 
            	this.articleId = value; 
            }
        }
		private DateTime recordTime;

		/// <summary>
		/// RecordTime
		/// </summary>
		/// <value></value>
		[DataField("RecordTime")]
        public DateTime RecordTime
        {
            get { 
            	return this.recordTime; 
            }
            set { 
            	this.recordTime = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int action;

		/// <summary>
		/// Action
		/// </summary>
		/// <value></value>
		[DataField("Action")]
        public int Action
        {
            get { 
            	return this.action; 
            }
            set { 
            	this.action = value; 
            }
        }
		private string requestUrl;

		/// <summary>
		/// RequestUrl
		/// </summary>
		/// <value></value>
		[DataField("RequestUrl")]
        public string RequestUrl
        {
            get { 
            	return this.requestUrl; 
            }
            set { 
            	this.requestUrl = value; 
            }
        }
		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField("CheckId", IsNullable = true)]
        public int? CheckId
        {
            get { 
            	return this.checkId; 
            }
            set { 
            	this.checkId = value; 
            }
        }
		private double? checkPoint;

		/// <summary>
		/// CheckPoint
		/// </summary>
		/// <value></value>
		[DataField("CheckPoint", IsNullable = true)]
        public double? CheckPoint
        {
            get { 
            	return this.checkPoint; 
            }
            set { 
            	this.checkPoint = value; 
            }
        }
		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField("CheckTime", IsNullable = true)]
        public DateTime? CheckTime
        {
            get { 
            	return this.checkTime; 
            }
            set { 
            	this.checkTime = value; 
            }
        }
		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField("CheckData", IsNullable = true)]
        public string CheckData
        {
            get { 
            	return this.checkData; 
            }
            set { 
            	this.checkData = value; 
            }
        }
		private CheckLevelType? checkLevelTypeInt;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeInt", IsNullable = true)]
        public CheckLevelType? CheckLevelTypeInt
        {
            get { 
            	return this.checkLevelTypeInt; 
            }
            set { 
            	this.checkLevelTypeInt = value; 
            }
        }
		private CheckLevelType? checkLevelTypeString;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeString", IsNullable = true, DBType = "varchar")]
        public CheckLevelType? CheckLevelTypeString
        {
            get { 
            	return this.checkLevelTypeString; 
            }
            set { 
            	this.checkLevelTypeString = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_DataLogHistory")]
    public partial class TeDataLogHistory : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeDataLogHistory>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeDataLogHistory>.Create("UserId");

		static readonly DataFieldInfo articleIdField = DataFieldInfo<TeDataLogHistory>.Create("ArticleId");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<TeDataLogHistory>.Create("RecordTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeDataLogHistory>.Create("Status");

		static readonly DataFieldInfo actionField = DataFieldInfo<TeDataLogHistory>.Create("Action");

		static readonly DataFieldInfo requestUrlField = DataFieldInfo<TeDataLogHistory>.Create("RequestUrl");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeDataLogHistory>.Create("CheckId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeDataLogHistory>.Create("CheckPoint");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeDataLogHistory>.Create("CheckTime");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeDataLogHistory>.Create("CheckData");

		static readonly DataFieldInfo checkLevelTypeIntField = DataFieldInfo<TeDataLogHistory>.Create("CheckLevelTypeInt");

		static readonly DataFieldInfo checkLevelTypeStringField = DataFieldInfo<TeDataLogHistory>.Create("CheckLevelTypeString");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo ArticleIdField
        {
            get {
                return articleIdField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ActionField
        {
            get {
                return actionField;
            }
        }

		public static DataFieldInfo RequestUrlField
        {
            get {
                return requestUrlField;
            }
        }

		public static DataFieldInfo CheckIdField
        {
            get {
                return checkIdField;
            }
        }

		public static DataFieldInfo CheckPointField
        {
            get {
                return checkPointField;
            }
        }

		public static DataFieldInfo CheckTimeField
        {
            get {
                return checkTimeField;
            }
        }

		public static DataFieldInfo CheckDataField
        {
            get {
                return checkDataField;
            }
        }

		public static DataFieldInfo CheckLevelTypeIntField
        {
            get {
                return checkLevelTypeIntField;
            }
        }

		public static DataFieldInfo CheckLevelTypeStringField
        {
            get {
                return checkLevelTypeStringField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id")]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private int articleId;

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		[DataField("ArticleId")]
        public int ArticleId
        {
            get { 
            	return this.articleId; 
            }
            set { 
            	this.articleId = value; 
            }
        }
		private DateTime recordTime;

		/// <summary>
		/// RecordTime
		/// </summary>
		/// <value></value>
		[DataField("RecordTime")]
        public DateTime RecordTime
        {
            get { 
            	return this.recordTime; 
            }
            set { 
            	this.recordTime = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int action;

		/// <summary>
		/// Action
		/// </summary>
		/// <value></value>
		[DataField("Action")]
        public int Action
        {
            get { 
            	return this.action; 
            }
            set { 
            	this.action = value; 
            }
        }
		private string requestUrl;

		/// <summary>
		/// RequestUrl
		/// </summary>
		/// <value></value>
		[DataField("RequestUrl")]
        public string RequestUrl
        {
            get { 
            	return this.requestUrl; 
            }
            set { 
            	this.requestUrl = value; 
            }
        }
		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField("CheckId", IsNullable = true)]
        public int? CheckId
        {
            get { 
            	return this.checkId; 
            }
            set { 
            	this.checkId = value; 
            }
        }
		private double? checkPoint;

		/// <summary>
		/// CheckPoint
		/// </summary>
		/// <value></value>
		[DataField("CheckPoint", IsNullable = true)]
        public double? CheckPoint
        {
            get { 
            	return this.checkPoint; 
            }
            set { 
            	this.checkPoint = value; 
            }
        }
		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField("CheckTime", IsNullable = true)]
        public DateTime? CheckTime
        {
            get { 
            	return this.checkTime; 
            }
            set { 
            	this.checkTime = value; 
            }
        }
		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField("CheckData", IsNullable = true)]
        public string CheckData
        {
            get { 
            	return this.checkData; 
            }
            set { 
            	this.checkData = value; 
            }
        }
		private CheckLevelType? checkLevelTypeInt;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeInt", IsNullable = true)]
        public CheckLevelType? CheckLevelTypeInt
        {
            get { 
            	return this.checkLevelTypeInt; 
            }
            set { 
            	this.checkLevelTypeInt = value; 
            }
        }
		private CheckLevelType? checkLevelTypeString;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeString", IsNullable = true, DBType = "varchar")]
        public CheckLevelType? CheckLevelTypeString
        {
            get { 
            	return this.checkLevelTypeString; 
            }
            set { 
            	this.checkLevelTypeString = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_DataLogHistory2")]
    public partial class TeDataLogHistory2 : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeDataLogHistory2>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeDataLogHistory2>.Create("UserId");

		static readonly DataFieldInfo articleIdField = DataFieldInfo<TeDataLogHistory2>.Create("ArticleId");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<TeDataLogHistory2>.Create("RecordTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeDataLogHistory2>.Create("Status");

		static readonly DataFieldInfo actionField = DataFieldInfo<TeDataLogHistory2>.Create("Action");

		static readonly DataFieldInfo requestUrlField = DataFieldInfo<TeDataLogHistory2>.Create("RequestUrl");

		static readonly DataFieldInfo checkIdField = DataFieldInfo<TeDataLogHistory2>.Create("CheckId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeDataLogHistory2>.Create("CheckPoint");

		static readonly DataFieldInfo checkTimeField = DataFieldInfo<TeDataLogHistory2>.Create("CheckTime");

		static readonly DataFieldInfo checkDataField = DataFieldInfo<TeDataLogHistory2>.Create("CheckData");

		static readonly DataFieldInfo checkLevelTypeIntField = DataFieldInfo<TeDataLogHistory2>.Create("CheckLevelTypeInt");

		static readonly DataFieldInfo checkLevelTypeStringField = DataFieldInfo<TeDataLogHistory2>.Create("CheckLevelTypeString");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo ArticleIdField
        {
            get {
                return articleIdField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo ActionField
        {
            get {
                return actionField;
            }
        }

		public static DataFieldInfo RequestUrlField
        {
            get {
                return requestUrlField;
            }
        }

		public static DataFieldInfo CheckIdField
        {
            get {
                return checkIdField;
            }
        }

		public static DataFieldInfo CheckPointField
        {
            get {
                return checkPointField;
            }
        }

		public static DataFieldInfo CheckTimeField
        {
            get {
                return checkTimeField;
            }
        }

		public static DataFieldInfo CheckDataField
        {
            get {
                return checkDataField;
            }
        }

		public static DataFieldInfo CheckLevelTypeIntField
        {
            get {
                return checkLevelTypeIntField;
            }
        }

		public static DataFieldInfo CheckLevelTypeStringField
        {
            get {
                return checkLevelTypeStringField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private int articleId;

		/// <summary>
		/// ArticleId
		/// </summary>
		/// <value></value>
		[DataField("ArticleId")]
        public int ArticleId
        {
            get { 
            	return this.articleId; 
            }
            set { 
            	this.articleId = value; 
            }
        }
		private DateTime recordTime;

		/// <summary>
		/// RecordTime
		/// </summary>
		/// <value></value>
		[DataField("RecordTime")]
        public DateTime RecordTime
        {
            get { 
            	return this.recordTime; 
            }
            set { 
            	this.recordTime = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int action;

		/// <summary>
		/// Action
		/// </summary>
		/// <value></value>
		[DataField("Action")]
        public int Action
        {
            get { 
            	return this.action; 
            }
            set { 
            	this.action = value; 
            }
        }
		private string requestUrl;

		/// <summary>
		/// RequestUrl
		/// </summary>
		/// <value></value>
		[DataField("RequestUrl")]
        public string RequestUrl
        {
            get { 
            	return this.requestUrl; 
            }
            set { 
            	this.requestUrl = value; 
            }
        }
		private int? checkId;

		/// <summary>
		/// CheckId
		/// </summary>
		/// <value></value>
		[DataField("CheckId", IsNullable = true)]
        public int? CheckId
        {
            get { 
            	return this.checkId; 
            }
            set { 
            	this.checkId = value; 
            }
        }
		private double? checkPoint;

		/// <summary>
		/// CheckPoint
		/// </summary>
		/// <value></value>
		[DataField("CheckPoint", IsNullable = true)]
        public double? CheckPoint
        {
            get { 
            	return this.checkPoint; 
            }
            set { 
            	this.checkPoint = value; 
            }
        }
		private DateTime? checkTime;

		/// <summary>
		/// CheckTime
		/// </summary>
		/// <value></value>
		[DataField("CheckTime", IsNullable = true)]
        public DateTime? CheckTime
        {
            get { 
            	return this.checkTime; 
            }
            set { 
            	this.checkTime = value; 
            }
        }
		private string checkData;

		/// <summary>
		/// CheckData
		/// </summary>
		/// <value></value>
		[DataField("CheckData", IsNullable = true)]
        public string CheckData
        {
            get { 
            	return this.checkData; 
            }
            set { 
            	this.checkData = value; 
            }
        }
		private CheckLevelType? checkLevelTypeInt;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeInt", IsNullable = true)]
        public CheckLevelType? CheckLevelTypeInt
        {
            get { 
            	return this.checkLevelTypeInt; 
            }
            set { 
            	this.checkLevelTypeInt = value; 
            }
        }
		private CheckLevelType? checkLevelTypeString;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelTypeString", IsNullable = true, DBType = "varchar")]
        public CheckLevelType? CheckLevelTypeString
        {
            get { 
            	return this.checkLevelTypeString; 
            }
            set { 
            	this.checkLevelTypeString = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateA")]
    public partial class TeRelateA : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateA>.Create("Id");

		static readonly DataFieldInfo relateBIdField = DataFieldInfo<TeRelateA>.Create("RelateBId");

		static readonly DataFieldInfo relateCIdField = DataFieldInfo<TeRelateA>.Create("RelateCId");

		static readonly DataFieldInfo relateDIdField = DataFieldInfo<TeRelateA>.Create("RelateDId");

		static readonly DataFieldInfo relateEIdField = DataFieldInfo<TeRelateA>.Create("RelateEId");

		static readonly DataFieldInfo relateFIdField = DataFieldInfo<TeRelateA>.Create("RelateFId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateA>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateBIdField
        {
            get {
                return relateBIdField;
            }
        }

		public static DataFieldInfo RelateCIdField
        {
            get {
                return relateCIdField;
            }
        }

		public static DataFieldInfo RelateDIdField
        {
            get {
                return relateDIdField;
            }
        }

		public static DataFieldInfo RelateEIdField
        {
            get {
                return relateEIdField;
            }
        }

		public static DataFieldInfo RelateFIdField
        {
            get {
                return relateFIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateBId;

		/// <summary>
		/// RelateBId
		/// </summary>
		/// <value></value>
		[DataField("RelateBId")]
        public int RelateBId
        {
            get { 
            	return this.relateBId; 
            }
            set { 
            	this.relateBId = value; 
            }
        }
		private int relateCId;

		/// <summary>
		/// RelateCId
		/// </summary>
		/// <value></value>
		[DataField("RelateCId")]
        public int RelateCId
        {
            get { 
            	return this.relateCId; 
            }
            set { 
            	this.relateCId = value; 
            }
        }
		private int relateDId;

		/// <summary>
		/// RelateDId
		/// </summary>
		/// <value></value>
		[DataField("RelateDId")]
        public int RelateDId
        {
            get { 
            	return this.relateDId; 
            }
            set { 
            	this.relateDId = value; 
            }
        }
		private int relateEId;

		/// <summary>
		/// RelateEId
		/// </summary>
		/// <value></value>
		[DataField("RelateEId")]
        public int RelateEId
        {
            get { 
            	return this.relateEId; 
            }
            set { 
            	this.relateEId = value; 
            }
        }
		private int relateFId;

		/// <summary>
		/// RelateFId
		/// </summary>
		/// <value></value>
		[DataField("RelateFId")]
        public int RelateFId
        {
            get { 
            	return this.relateFId; 
            }
            set { 
            	this.relateFId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateB")]
    public partial class TeRelateB : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateB>.Create("Id");

		static readonly DataFieldInfo relateAIdField = DataFieldInfo<TeRelateB>.Create("RelateAId");

		static readonly DataFieldInfo relateCIdField = DataFieldInfo<TeRelateB>.Create("RelateCId");

		static readonly DataFieldInfo relateDIdField = DataFieldInfo<TeRelateB>.Create("RelateDId");

		static readonly DataFieldInfo relateEIdField = DataFieldInfo<TeRelateB>.Create("RelateEId");

		static readonly DataFieldInfo relateFIdField = DataFieldInfo<TeRelateB>.Create("RelateFId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateB>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateAIdField
        {
            get {
                return relateAIdField;
            }
        }

		public static DataFieldInfo RelateCIdField
        {
            get {
                return relateCIdField;
            }
        }

		public static DataFieldInfo RelateDIdField
        {
            get {
                return relateDIdField;
            }
        }

		public static DataFieldInfo RelateEIdField
        {
            get {
                return relateEIdField;
            }
        }

		public static DataFieldInfo RelateFIdField
        {
            get {
                return relateFIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateAId;

		/// <summary>
		/// RelateAId
		/// </summary>
		/// <value></value>
		[DataField("RelateAId")]
        public int RelateAId
        {
            get { 
            	return this.relateAId; 
            }
            set { 
            	this.relateAId = value; 
            }
        }
		private int relateCId;

		/// <summary>
		/// RelateCId
		/// </summary>
		/// <value></value>
		[DataField("RelateCId")]
        public int RelateCId
        {
            get { 
            	return this.relateCId; 
            }
            set { 
            	this.relateCId = value; 
            }
        }
		private int relateDId;

		/// <summary>
		/// RelateDId
		/// </summary>
		/// <value></value>
		[DataField("RelateDId")]
        public int RelateDId
        {
            get { 
            	return this.relateDId; 
            }
            set { 
            	this.relateDId = value; 
            }
        }
		private int relateEId;

		/// <summary>
		/// RelateEId
		/// </summary>
		/// <value></value>
		[DataField("RelateEId")]
        public int RelateEId
        {
            get { 
            	return this.relateEId; 
            }
            set { 
            	this.relateEId = value; 
            }
        }
		private int relateFId;

		/// <summary>
		/// RelateFId
		/// </summary>
		/// <value></value>
		[DataField("RelateFId")]
        public int RelateFId
        {
            get { 
            	return this.relateFId; 
            }
            set { 
            	this.relateFId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateC")]
    public partial class TeRelateC : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateC>.Create("Id");

		static readonly DataFieldInfo relateAIdField = DataFieldInfo<TeRelateC>.Create("RelateAId");

		static readonly DataFieldInfo relateBIdField = DataFieldInfo<TeRelateC>.Create("RelateBId");

		static readonly DataFieldInfo relateDIdField = DataFieldInfo<TeRelateC>.Create("RelateDId");

		static readonly DataFieldInfo relateEIdField = DataFieldInfo<TeRelateC>.Create("RelateEId");

		static readonly DataFieldInfo relateFIdField = DataFieldInfo<TeRelateC>.Create("RelateFId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateC>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateAIdField
        {
            get {
                return relateAIdField;
            }
        }

		public static DataFieldInfo RelateBIdField
        {
            get {
                return relateBIdField;
            }
        }

		public static DataFieldInfo RelateDIdField
        {
            get {
                return relateDIdField;
            }
        }

		public static DataFieldInfo RelateEIdField
        {
            get {
                return relateEIdField;
            }
        }

		public static DataFieldInfo RelateFIdField
        {
            get {
                return relateFIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateAId;

		/// <summary>
		/// RelateAId
		/// </summary>
		/// <value></value>
		[DataField("RelateAId")]
        public int RelateAId
        {
            get { 
            	return this.relateAId; 
            }
            set { 
            	this.relateAId = value; 
            }
        }
		private int relateBId;

		/// <summary>
		/// RelateBId
		/// </summary>
		/// <value></value>
		[DataField("RelateBId")]
        public int RelateBId
        {
            get { 
            	return this.relateBId; 
            }
            set { 
            	this.relateBId = value; 
            }
        }
		private int relateDId;

		/// <summary>
		/// RelateDId
		/// </summary>
		/// <value></value>
		[DataField("RelateDId")]
        public int RelateDId
        {
            get { 
            	return this.relateDId; 
            }
            set { 
            	this.relateDId = value; 
            }
        }
		private int relateEId;

		/// <summary>
		/// RelateEId
		/// </summary>
		/// <value></value>
		[DataField("RelateEId")]
        public int RelateEId
        {
            get { 
            	return this.relateEId; 
            }
            set { 
            	this.relateEId = value; 
            }
        }
		private int relateFId;

		/// <summary>
		/// RelateFId
		/// </summary>
		/// <value></value>
		[DataField("RelateFId")]
        public int RelateFId
        {
            get { 
            	return this.relateFId; 
            }
            set { 
            	this.relateFId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateD")]
    public partial class TeRelateD : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateD>.Create("Id");

		static readonly DataFieldInfo relateAIdField = DataFieldInfo<TeRelateD>.Create("RelateAId");

		static readonly DataFieldInfo relateBIdField = DataFieldInfo<TeRelateD>.Create("RelateBId");

		static readonly DataFieldInfo relateCIdField = DataFieldInfo<TeRelateD>.Create("RelateCId");

		static readonly DataFieldInfo relateEIdField = DataFieldInfo<TeRelateD>.Create("RelateEId");

		static readonly DataFieldInfo relateFIdField = DataFieldInfo<TeRelateD>.Create("RelateFId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateD>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateAIdField
        {
            get {
                return relateAIdField;
            }
        }

		public static DataFieldInfo RelateBIdField
        {
            get {
                return relateBIdField;
            }
        }

		public static DataFieldInfo RelateCIdField
        {
            get {
                return relateCIdField;
            }
        }

		public static DataFieldInfo RelateEIdField
        {
            get {
                return relateEIdField;
            }
        }

		public static DataFieldInfo RelateFIdField
        {
            get {
                return relateFIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateAId;

		/// <summary>
		/// RelateAId
		/// </summary>
		/// <value></value>
		[DataField("RelateAId")]
        public int RelateAId
        {
            get { 
            	return this.relateAId; 
            }
            set { 
            	this.relateAId = value; 
            }
        }
		private int relateBId;

		/// <summary>
		/// RelateBId
		/// </summary>
		/// <value></value>
		[DataField("RelateBId")]
        public int RelateBId
        {
            get { 
            	return this.relateBId; 
            }
            set { 
            	this.relateBId = value; 
            }
        }
		private int relateCId;

		/// <summary>
		/// RelateCId
		/// </summary>
		/// <value></value>
		[DataField("RelateCId")]
        public int RelateCId
        {
            get { 
            	return this.relateCId; 
            }
            set { 
            	this.relateCId = value; 
            }
        }
		private int relateEId;

		/// <summary>
		/// RelateEId
		/// </summary>
		/// <value></value>
		[DataField("RelateEId")]
        public int RelateEId
        {
            get { 
            	return this.relateEId; 
            }
            set { 
            	this.relateEId = value; 
            }
        }
		private int relateFId;

		/// <summary>
		/// RelateFId
		/// </summary>
		/// <value></value>
		[DataField("RelateFId")]
        public int RelateFId
        {
            get { 
            	return this.relateFId; 
            }
            set { 
            	this.relateFId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateE")]
    public partial class TeRelateE : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateE>.Create("Id");

		static readonly DataFieldInfo relateAIdField = DataFieldInfo<TeRelateE>.Create("RelateAId");

		static readonly DataFieldInfo relateBIdField = DataFieldInfo<TeRelateE>.Create("RelateBId");

		static readonly DataFieldInfo relateCIdField = DataFieldInfo<TeRelateE>.Create("RelateCId");

		static readonly DataFieldInfo relateDIdField = DataFieldInfo<TeRelateE>.Create("RelateDId");

		static readonly DataFieldInfo relateFIdField = DataFieldInfo<TeRelateE>.Create("RelateFId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateE>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateAIdField
        {
            get {
                return relateAIdField;
            }
        }

		public static DataFieldInfo RelateBIdField
        {
            get {
                return relateBIdField;
            }
        }

		public static DataFieldInfo RelateCIdField
        {
            get {
                return relateCIdField;
            }
        }

		public static DataFieldInfo RelateDIdField
        {
            get {
                return relateDIdField;
            }
        }

		public static DataFieldInfo RelateFIdField
        {
            get {
                return relateFIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateAId;

		/// <summary>
		/// RelateAId
		/// </summary>
		/// <value></value>
		[DataField("RelateAId")]
        public int RelateAId
        {
            get { 
            	return this.relateAId; 
            }
            set { 
            	this.relateAId = value; 
            }
        }
		private int relateBId;

		/// <summary>
		/// RelateBId
		/// </summary>
		/// <value></value>
		[DataField("RelateBId")]
        public int RelateBId
        {
            get { 
            	return this.relateBId; 
            }
            set { 
            	this.relateBId = value; 
            }
        }
		private int relateCId;

		/// <summary>
		/// RelateCId
		/// </summary>
		/// <value></value>
		[DataField("RelateCId")]
        public int RelateCId
        {
            get { 
            	return this.relateCId; 
            }
            set { 
            	this.relateCId = value; 
            }
        }
		private int relateDId;

		/// <summary>
		/// RelateDId
		/// </summary>
		/// <value></value>
		[DataField("RelateDId")]
        public int RelateDId
        {
            get { 
            	return this.relateDId; 
            }
            set { 
            	this.relateDId = value; 
            }
        }
		private int relateFId;

		/// <summary>
		/// RelateFId
		/// </summary>
		/// <value></value>
		[DataField("RelateFId")]
        public int RelateFId
        {
            get { 
            	return this.relateFId; 
            }
            set { 
            	this.relateFId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_RelateF")]
    public partial class TeRelateF : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeRelateF>.Create("Id");

		static readonly DataFieldInfo relateAIdField = DataFieldInfo<TeRelateF>.Create("RelateAId");

		static readonly DataFieldInfo relateBIdField = DataFieldInfo<TeRelateF>.Create("RelateBId");

		static readonly DataFieldInfo relateCIdField = DataFieldInfo<TeRelateF>.Create("RelateCId");

		static readonly DataFieldInfo relateDIdField = DataFieldInfo<TeRelateF>.Create("RelateDId");

		static readonly DataFieldInfo relateEIdField = DataFieldInfo<TeRelateF>.Create("RelateEId");

		static readonly DataFieldInfo contentField = DataFieldInfo<TeRelateF>.Create("Content");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RelateAIdField
        {
            get {
                return relateAIdField;
            }
        }

		public static DataFieldInfo RelateBIdField
        {
            get {
                return relateBIdField;
            }
        }

		public static DataFieldInfo RelateCIdField
        {
            get {
                return relateCIdField;
            }
        }

		public static DataFieldInfo RelateDIdField
        {
            get {
                return relateDIdField;
            }
        }

		public static DataFieldInfo RelateEIdField
        {
            get {
                return relateEIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int relateAId;

		/// <summary>
		/// RelateAId
		/// </summary>
		/// <value></value>
		[DataField("RelateAId")]
        public int RelateAId
        {
            get { 
            	return this.relateAId; 
            }
            set { 
            	this.relateAId = value; 
            }
        }
		private int relateBId;

		/// <summary>
		/// RelateBId
		/// </summary>
		/// <value></value>
		[DataField("RelateBId")]
        public int RelateBId
        {
            get { 
            	return this.relateBId; 
            }
            set { 
            	this.relateBId = value; 
            }
        }
		private int relateCId;

		/// <summary>
		/// RelateCId
		/// </summary>
		/// <value></value>
		[DataField("RelateCId")]
        public int RelateCId
        {
            get { 
            	return this.relateCId; 
            }
            set { 
            	this.relateCId = value; 
            }
        }
		private int relateDId;

		/// <summary>
		/// RelateDId
		/// </summary>
		/// <value></value>
		[DataField("RelateDId")]
        public int RelateDId
        {
            get { 
            	return this.relateDId; 
            }
            set { 
            	this.relateDId = value; 
            }
        }
		private int relateEId;

		/// <summary>
		/// RelateEId
		/// </summary>
		/// <value></value>
		[DataField("RelateEId")]
        public int RelateEId
        {
            get { 
            	return this.relateEId; 
            }
            set { 
            	this.relateEId = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content")]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_TagInfo")]
    public partial class TeTagInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo groupCodeField = DataFieldInfo<TeTagInfo>.Create("GroupCode");

		static readonly DataFieldInfo tagCodeField = DataFieldInfo<TeTagInfo>.Create("TagCode");

		static readonly DataFieldInfo tagNameField = DataFieldInfo<TeTagInfo>.Create("TagName");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeTagInfo>.Create("Remark");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeTagInfo>.Create("Status");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo GroupCodeField
        {
            get {
                return groupCodeField;
            }
        }

		public static DataFieldInfo TagCodeField
        {
            get {
                return tagCodeField;
            }
        }

		public static DataFieldInfo TagNameField
        {
            get {
                return tagNameField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string groupCode;

		/// <summary>
		/// GroupCode
		/// </summary>
		/// <value></value>
		[DataField("GroupCode", IsPrimaryKey = true)]
        public string GroupCode
        {
            get { 
            	return this.groupCode; 
            }
            set { 
            	this.groupCode = value; 
            }
        }
		private string tagCode;

		/// <summary>
		/// TagCode
		/// </summary>
		/// <value></value>
		[DataField("TagCode", IsPrimaryKey = true)]
        public string TagCode
        {
            get { 
            	return this.tagCode; 
            }
            set { 
            	this.tagCode = value; 
            }
        }
		private string tagName;

		/// <summary>
		/// TagName
		/// </summary>
		/// <value></value>
		[DataField("TagName")]
        public string TagName
        {
            get { 
            	return this.tagName; 
            }
            set { 
            	this.tagName = value; 
            }
        }
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_User")]
    public partial class TeUser : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUser>.Create("Id");

		static readonly DataFieldInfo accountField = DataFieldInfo<TeUser>.Create("Account");

		static readonly DataFieldInfo passwordField = DataFieldInfo<TeUser>.Create("Password");

		static readonly DataFieldInfo nickNameField = DataFieldInfo<TeUser>.Create("NickName");

		static readonly DataFieldInfo genderField = DataFieldInfo<TeUser>.Create("Gender");

		static readonly DataFieldInfo birthdayField = DataFieldInfo<TeUser>.Create("Birthday");

		static readonly DataFieldInfo telephoneField = DataFieldInfo<TeUser>.Create("Telephone");

		static readonly DataFieldInfo emailField = DataFieldInfo<TeUser>.Create("Email");

		static readonly DataFieldInfo addressField = DataFieldInfo<TeUser>.Create("Address");

		static readonly DataFieldInfo levelIdField = DataFieldInfo<TeUser>.Create("LevelId");

		static readonly DataFieldInfo regTimeField = DataFieldInfo<TeUser>.Create("RegTime");

		static readonly DataFieldInfo lastLoginTimeField = DataFieldInfo<TeUser>.Create("LastLoginTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUser>.Create("Status");

		static readonly DataFieldInfo hotRateField = DataFieldInfo<TeUser>.Create("HotRate");

		static readonly DataFieldInfo areaField = DataFieldInfo<TeUser>.Create("Area");

		static readonly DataFieldInfo deleteFlagField = DataFieldInfo<TeUser>.Create("DeleteFlag");

		static readonly DataFieldInfo refereeIdField = DataFieldInfo<TeUser>.Create("RefereeId");

		static readonly DataFieldInfo checkPointField = DataFieldInfo<TeUser>.Create("CheckPoint");

		static readonly DataFieldInfo checkStatusField = DataFieldInfo<TeUser>.Create("CheckStatus");

		static readonly DataFieldInfo checkLevelTypeField = DataFieldInfo<TeUser>.Create("CheckLevelType");

		static readonly DataFieldInfo loginTimesField = DataFieldInfo<TeUser>.Create("LoginTimes");

		static readonly DataFieldInfo markField = DataFieldInfo<TeUser>.Create("Mark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo AccountField
        {
            get {
                return accountField;
            }
        }

		public static DataFieldInfo PasswordField
        {
            get {
                return passwordField;
            }
        }

		public static DataFieldInfo NickNameField
        {
            get {
                return nickNameField;
            }
        }

		public static DataFieldInfo GenderField
        {
            get {
                return genderField;
            }
        }

		public static DataFieldInfo BirthdayField
        {
            get {
                return birthdayField;
            }
        }

		public static DataFieldInfo TelephoneField
        {
            get {
                return telephoneField;
            }
        }

		public static DataFieldInfo EmailField
        {
            get {
                return emailField;
            }
        }

		public static DataFieldInfo AddressField
        {
            get {
                return addressField;
            }
        }

		public static DataFieldInfo LevelIdField
        {
            get {
                return levelIdField;
            }
        }

		public static DataFieldInfo RegTimeField
        {
            get {
                return regTimeField;
            }
        }

		public static DataFieldInfo LastLoginTimeField
        {
            get {
                return lastLoginTimeField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo HotRateField
        {
            get {
                return hotRateField;
            }
        }

		public static DataFieldInfo AreaField
        {
            get {
                return areaField;
            }
        }

		public static DataFieldInfo DeleteFlagField
        {
            get {
                return deleteFlagField;
            }
        }

		public static DataFieldInfo RefereeIdField
        {
            get {
                return refereeIdField;
            }
        }

		public static DataFieldInfo CheckPointField
        {
            get {
                return checkPointField;
            }
        }

		public static DataFieldInfo CheckStatusField
        {
            get {
                return checkStatusField;
            }
        }

		public static DataFieldInfo CheckLevelTypeField
        {
            get {
                return checkLevelTypeField;
            }
        }

		public static DataFieldInfo LoginTimesField
        {
            get {
                return loginTimesField;
            }
        }

		public static DataFieldInfo MarkField
        {
            get {
                return markField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private string account;

		/// <summary>
		/// Account
		/// </summary>
		/// <value></value>
		[DataField("Account")]
        public string Account
        {
            get { 
            	return this.account; 
            }
            set { 
            	this.account = value; 
            }
        }
		private string password;

		/// <summary>
		/// Password
		/// </summary>
		/// <value></value>
		[DataField("Password")]
        public string Password
        {
            get { 
            	return this.password; 
            }
            set { 
            	this.password = value; 
            }
        }
		private string nickName;

		/// <summary>
		/// NickName
		/// </summary>
		/// <value></value>
		[DataField("NickName", IsNullable = true)]
        public string NickName
        {
            get { 
            	return this.nickName; 
            }
            set { 
            	this.nickName = value; 
            }
        }
		private GenderType gender;

		/// <summary>
		/// #EnumType:GenderType#sexy
		/// </summary>
		/// <value></value>
		[DataField("Gender")]
        public GenderType Gender
        {
            get { 
            	return this.gender; 
            }
            set { 
            	this.gender = value; 
            }
        }
		private DateTime birthday;

		/// <summary>
		/// Birthday
		/// </summary>
		/// <value></value>
		[DataField("Birthday")]
        public DateTime Birthday
        {
            get { 
            	return this.birthday; 
            }
            set { 
            	this.birthday = value; 
            }
        }
		private string telephone;

		/// <summary>
		/// Telephone
		/// </summary>
		/// <value></value>
		[DataField("Telephone", IsNullable = true)]
        public string Telephone
        {
            get { 
            	return this.telephone; 
            }
            set { 
            	this.telephone = value; 
            }
        }
		private string email;

		/// <summary>
		/// Email
		/// </summary>
		/// <value></value>
		[DataField("Email", IsNullable = true)]
        public string Email
        {
            get { 
            	return this.email; 
            }
            set { 
            	this.email = value; 
            }
        }
		private string address;

		/// <summary>
		/// Address
		/// </summary>
		/// <value></value>
		[DataField("Address", IsNullable = true)]
        public string Address
        {
            get { 
            	return this.address; 
            }
            set { 
            	this.address = value; 
            }
        }
		private int levelId;

		/// <summary>
		/// LevelId
		/// </summary>
		/// <value></value>
		[DataField("LevelId")]
        public int LevelId
        {
            get { 
            	return this.levelId; 
            }
            set { 
            	this.levelId = value; 
            }
        }
		private DateTime regTime;

		/// <summary>
		/// RegTime
		/// </summary>
		/// <value></value>
		[DataField("RegTime")]
        public DateTime RegTime
        {
            get { 
            	return this.regTime; 
            }
            set { 
            	this.regTime = value; 
            }
        }
		private DateTime? lastLoginTime;

		/// <summary>
		/// LastLoginTime
		/// </summary>
		/// <value></value>
		[DataField("LastLoginTime", IsNullable = true)]
        public DateTime? LastLoginTime
        {
            get { 
            	return this.lastLoginTime; 
            }
            set { 
            	this.lastLoginTime = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private double hotRate;

		/// <summary>
		/// HotRate
		/// </summary>
		/// <value></value>
		[DataField("HotRate")]
        public double HotRate
        {
            get { 
            	return this.hotRate; 
            }
            set { 
            	this.hotRate = value; 
            }
        }
		private int? area;

		/// <summary>
		/// Area
		/// </summary>
		/// <value></value>
		[DataField("Area", IsNullable = true)]
        public int? Area
        {
            get { 
            	return this.area; 
            }
            set { 
            	this.area = value; 
            }
        }
		private bool deleteFlag;

		/// <summary>
		/// DeleteFlag
		/// </summary>
		/// <value></value>
		[DataField("DeleteFlag")]
        public bool DeleteFlag
        {
            get { 
            	return this.deleteFlag; 
            }
            set { 
            	this.deleteFlag = value; 
            }
        }
		private int? refereeId;

		/// <summary>
		/// RefereeId
		/// </summary>
		/// <value></value>
		[DataField("RefereeId", IsNullable = true)]
        public int? RefereeId
        {
            get { 
            	return this.refereeId; 
            }
            set { 
            	this.refereeId = value; 
            }
        }
		private double? checkPoint;

		/// <summary>
		/// Check_Point
		/// </summary>
		/// <value></value>
		[DataField("Check_Point", IsNullable = true)]
        public double? CheckPoint
        {
            get { 
            	return this.checkPoint; 
            }
            set { 
            	this.checkPoint = value; 
            }
        }
		private bool? checkStatus;

		/// <summary>
		/// Check_Status
		/// </summary>
		/// <value></value>
		[DataField("Check_Status", IsNullable = true)]
        public bool? CheckStatus
        {
            get { 
            	return this.checkStatus; 
            }
            set { 
            	this.checkStatus = value; 
            }
        }
		private CheckLevelType? checkLevelType;

		/// <summary>
		/// #EnumType:CheckLevelType#level
		/// </summary>
		/// <value></value>
		[DataField("Check_LevelType", IsNullable = true)]
        public CheckLevelType? CheckLevelType
        {
            get { 
            	return this.checkLevelType; 
            }
            set { 
            	this.checkLevelType = value; 
            }
        }
		private int loginTimes;

		/// <summary>
		/// LoginTimes
		/// </summary>
		/// <value></value>
		[DataField("LoginTimes")]
        public int LoginTimes
        {
            get { 
            	return this.loginTimes; 
            }
            set { 
            	this.loginTimes = value; 
            }
        }
		private int mark;

		/// <summary>
		/// Mark
		/// </summary>
		/// <value></value>
		[DataField("Mark")]
        public int Mark
        {
            get { 
            	return this.mark; 
            }
            set { 
            	this.mark = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_UserExtend")]
    public partial class TeUserExtend : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUserExtend>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeUserExtend>.Create("UserId");

		static readonly DataFieldInfo extend1Field = DataFieldInfo<TeUserExtend>.Create("Extend1");

		static readonly DataFieldInfo extend2Field = DataFieldInfo<TeUserExtend>.Create("Extend2");

		static readonly DataFieldInfo extend3Field = DataFieldInfo<TeUserExtend>.Create("Extend3");

		static readonly DataFieldInfo extendAreaIdField = DataFieldInfo<TeUserExtend>.Create("ExtendAreaId");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo Extend1Field
        {
            get {
                return extend1Field;
            }
        }

		public static DataFieldInfo Extend2Field
        {
            get {
                return extend2Field;
            }
        }

		public static DataFieldInfo Extend3Field
        {
            get {
                return extend3Field;
            }
        }

		public static DataFieldInfo ExtendAreaIdField
        {
            get {
                return extendAreaIdField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string extend1;

		/// <summary>
		/// Extend1
		/// </summary>
		/// <value></value>
		[DataField("Extend1", IsNullable = true)]
        public string Extend1
        {
            get { 
            	return this.extend1; 
            }
            set { 
            	this.extend1 = value; 
            }
        }
		private string extend2;

		/// <summary>
		/// Extend2
		/// </summary>
		/// <value></value>
		[DataField("Extend2", IsNullable = true)]
        public string Extend2
        {
            get { 
            	return this.extend2; 
            }
            set { 
            	this.extend2 = value; 
            }
        }
		private string extend3;

		/// <summary>
		/// Extend3
		/// </summary>
		/// <value></value>
		[DataField("Extend3", IsNullable = true)]
        public string Extend3
        {
            get { 
            	return this.extend3; 
            }
            set { 
            	this.extend3 = value; 
            }
        }
		private int? extendAreaId;

		/// <summary>
		/// ExtendAreaId
		/// </summary>
		/// <value></value>
		[DataField("ExtendAreaId", IsNullable = true)]
        public int? ExtendAreaId
        {
            get { 
            	return this.extendAreaId; 
            }
            set { 
            	this.extendAreaId = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_UserExtendProfile")]
    public partial class TeUserExtendProfile : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUserExtendProfile>.Create("Id");

		static readonly DataFieldInfo userIdField = DataFieldInfo<TeUserExtendProfile>.Create("UserId");

		static readonly DataFieldInfo extend1Field = DataFieldInfo<TeUserExtendProfile>.Create("Extend1");

		static readonly DataFieldInfo extend2Field = DataFieldInfo<TeUserExtendProfile>.Create("Extend2");

		static readonly DataFieldInfo extend3Field = DataFieldInfo<TeUserExtendProfile>.Create("Extend3");

		static readonly DataFieldInfo extendProfileIdField = DataFieldInfo<TeUserExtendProfile>.Create("ExtendProfileId");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo Extend1Field
        {
            get {
                return extend1Field;
            }
        }

		public static DataFieldInfo Extend2Field
        {
            get {
                return extend2Field;
            }
        }

		public static DataFieldInfo Extend3Field
        {
            get {
                return extend3Field;
            }
        }

		public static DataFieldInfo ExtendProfileIdField
        {
            get {
                return extendProfileIdField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsIdentity = true, IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private int userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public int UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
            }
        }
		private string extend1;

		/// <summary>
		/// Extend1
		/// </summary>
		/// <value></value>
		[DataField("Extend1", IsNullable = true)]
        public string Extend1
        {
            get { 
            	return this.extend1; 
            }
            set { 
            	this.extend1 = value; 
            }
        }
		private string extend2;

		/// <summary>
		/// Extend2
		/// </summary>
		/// <value></value>
		[DataField("Extend2", IsNullable = true)]
        public string Extend2
        {
            get { 
            	return this.extend2; 
            }
            set { 
            	this.extend2 = value; 
            }
        }
		private string extend3;

		/// <summary>
		/// Extend3
		/// </summary>
		/// <value></value>
		[DataField("Extend3", IsNullable = true)]
        public string Extend3
        {
            get { 
            	return this.extend3; 
            }
            set { 
            	this.extend3 = value; 
            }
        }
		private int extendProfileId;

		/// <summary>
		/// ExtendProfileId
		/// </summary>
		/// <value></value>
		[DataField("ExtendProfileId")]
        public int ExtendProfileId
        {
            get { 
            	return this.extendProfileId; 
            }
            set { 
            	this.extendProfileId = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Te_UserLevel")]
    public partial class TeUserLevel : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TeUserLevel>.Create("Id");

		static readonly DataFieldInfo levelNameField = DataFieldInfo<TeUserLevel>.Create("LevelName");

		static readonly DataFieldInfo statusField = DataFieldInfo<TeUserLevel>.Create("Status");

		static readonly DataFieldInfo remarkField = DataFieldInfo<TeUserLevel>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo LevelNameField
        {
            get {
                return levelNameField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

		/// <summary>
		/// Id
		/// </summary>
		/// <value></value>
		[DataField("Id", IsPrimaryKey = true)]
        public int Id
        {
            get { 
            	return this.id; 
            }
            set { 
            	this.id = value; 
            }
        }
		private string levelName;

		/// <summary>
		/// LevelName
		/// </summary>
		/// <value></value>
		[DataField("LevelName")]
        public string LevelName
        {
            get { 
            	return this.levelName; 
            }
            set { 
            	this.levelName = value; 
            }
        }
		private int status;

		/// <summary>
		/// Status
		/// </summary>
		/// <value></value>
		[DataField("Status")]
        public int Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private string remark;

		/// <summary>
		/// Remark
		/// </summary>
		/// <value></value>
		[DataField("Remark", IsNullable = true)]
        public string Remark
        {
            get { 
            	return this.remark; 
            }
            set { 
            	this.remark = value; 
            }
        }
		#endregion
    }

}

