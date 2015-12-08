


using System;
using System.Collections.Generic;
using System.Text;
using Light.Data;

namespace Light.Data.Demo
{
    [Serializable]
    [DataTable("ScConfiguration")]
    public partial class ScConfiguration : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo controllerNameField = DataFieldInfo<ScConfiguration>.Create("ControllerName");

		static readonly DataFieldInfo syncSpanField = DataFieldInfo<ScConfiguration>.Create("SyncSpan");

		static readonly DataFieldInfo argumentsField = DataFieldInfo<ScConfiguration>.Create("Arguments");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo ControllerNameField
        {
            get {
                return controllerNameField;
            }
        }

		public static DataFieldInfo SyncSpanField
        {
            get {
                return syncSpanField;
            }
        }

		public static DataFieldInfo ArgumentsField
        {
            get {
                return argumentsField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string controllerName;

		/// <summary>
		/// 控制器名称
		/// </summary>
		/// <value></value>
		[DataField("ControllerName", IsPrimaryKey = true)]
        public string ControllerName
        {
            get { 
            	return this.controllerName; 
            }
            set { 
            	this.controllerName = value; 
            }
        }
		private int syncSpan;

		/// <summary>
		/// 同步时间间隔
		/// </summary>
		/// <value></value>
		[DataField("SyncSpan")]
        public int SyncSpan
        {
            get { 
            	return this.syncSpan; 
            }
            set { 
            	this.syncSpan = value; 
            }
        }
		private string arguments;

		/// <summary>
		/// 参数集
		/// </summary>
		/// <value></value>
		[DataField("Arguments")]
        public string Arguments
        {
            get { 
            	return this.arguments; 
            }
            set { 
            	this.arguments = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScDataLog")]
    public partial class ScDataLog : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDataLog>.Create("Id");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<ScDataLog>.Create("RecordTime");

		static readonly DataFieldInfo userIdField = DataFieldInfo<ScDataLog>.Create("UserId");

		static readonly DataFieldInfo flagField = DataFieldInfo<ScDataLog>.Create("Flag");

		static readonly DataFieldInfo midField = DataFieldInfo<ScDataLog>.Create("Mid");

		static readonly DataFieldInfo codeField = DataFieldInfo<ScDataLog>.Create("Code");

		static readonly DataFieldInfo urlField = DataFieldInfo<ScDataLog>.Create("Url");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo UserIdField
        {
            get {
                return userIdField;
            }
        }

		public static DataFieldInfo FlagField
        {
            get {
                return flagField;
            }
        }

		public static DataFieldInfo MidField
        {
            get {
                return midField;
            }
        }

		public static DataFieldInfo CodeField
        {
            get {
                return codeField;
            }
        }

		public static DataFieldInfo UrlField
        {
            get {
                return urlField;
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
		private string userId;

		/// <summary>
		/// UserId
		/// </summary>
		/// <value></value>
		[DataField("UserId")]
        public string UserId
        {
            get { 
            	return this.userId; 
            }
            set { 
            	this.userId = value; 
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
		private string mid;

		/// <summary>
		/// Mid
		/// </summary>
		/// <value></value>
		[DataField("Mid", IsNullable = true)]
        public string Mid
        {
            get { 
            	return this.mid; 
            }
            set { 
            	this.mid = value; 
            }
        }
		private string code;

		/// <summary>
		/// Code
		/// </summary>
		/// <value></value>
		[DataField("Code")]
        public string Code
        {
            get { 
            	return this.code; 
            }
            set { 
            	this.code = value; 
            }
        }
		private string url;

		/// <summary>
		/// Url
		/// </summary>
		/// <value></value>
		[DataField("Url", IsNullable = true)]
        public string Url
        {
            get { 
            	return this.url; 
            }
            set { 
            	this.url = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScDataStat")]
    public partial class ScDataStat : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo statDateField = DataFieldInfo<ScDataStat>.Create("StatDate");

		static readonly DataFieldInfo flagField = DataFieldInfo<ScDataStat>.Create("Flag");

		static readonly DataFieldInfo midField = DataFieldInfo<ScDataStat>.Create("Mid");

		static readonly DataFieldInfo codeField = DataFieldInfo<ScDataStat>.Create("Code");

		static readonly DataFieldInfo pvField = DataFieldInfo<ScDataStat>.Create("Pv");

		static readonly DataFieldInfo uvField = DataFieldInfo<ScDataStat>.Create("Uv");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo StatDateField
        {
            get {
                return statDateField;
            }
        }

		public static DataFieldInfo FlagField
        {
            get {
                return flagField;
            }
        }

		public static DataFieldInfo MidField
        {
            get {
                return midField;
            }
        }

		public static DataFieldInfo CodeField
        {
            get {
                return codeField;
            }
        }

		public static DataFieldInfo PvField
        {
            get {
                return pvField;
            }
        }

		public static DataFieldInfo UvField
        {
            get {
                return uvField;
            }
        }

    	#endregion

    	#region "Data Property"
		private DateTime statDate;

		/// <summary>
		/// StatDate
		/// </summary>
		/// <value></value>
		[DataField("StatDate", IsPrimaryKey = true)]
        public DateTime StatDate
        {
            get { 
            	return this.statDate; 
            }
            set { 
            	this.statDate = value; 
            }
        }
		private string flag;

		/// <summary>
		/// Flag
		/// </summary>
		/// <value></value>
		[DataField("Flag", IsPrimaryKey = true)]
        public string Flag
        {
            get { 
            	return this.flag; 
            }
            set { 
            	this.flag = value; 
            }
        }
		private string mid;

		/// <summary>
		/// Mid
		/// </summary>
		/// <value></value>
		[DataField("Mid", IsPrimaryKey = true)]
        public string Mid
        {
            get { 
            	return this.mid; 
            }
            set { 
            	this.mid = value; 
            }
        }
		private string code;

		/// <summary>
		/// Code
		/// </summary>
		/// <value></value>
		[DataField("Code", IsPrimaryKey = true)]
        public string Code
        {
            get { 
            	return this.code; 
            }
            set { 
            	this.code = value; 
            }
        }
		private int pv;

		/// <summary>
		/// Pv
		/// </summary>
		/// <value></value>
		[DataField("Pv")]
        public int Pv
        {
            get { 
            	return this.pv; 
            }
            set { 
            	this.pv = value; 
            }
        }
		private string uv;

		/// <summary>
		/// Uv
		/// </summary>
		/// <value></value>
		[DataField("Uv")]
        public string Uv
        {
            get { 
            	return this.uv; 
            }
            set { 
            	this.uv = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScDevicePackage")]
    public partial class ScDevicePackage : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDevicePackage>.Create("Id");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<ScDevicePackage>.Create("RecordTime");

		static readonly DataFieldInfo getTimeField = DataFieldInfo<ScDevicePackage>.Create("GetTime");

		static readonly DataFieldInfo packageNameField = DataFieldInfo<ScDevicePackage>.Create("PackageName");

		static readonly DataFieldInfo countField = DataFieldInfo<ScDevicePackage>.Create("Count");

		static readonly DataFieldInfo statusField = DataFieldInfo<ScDevicePackage>.Create("Status");

		static readonly DataFieldInfo dataField = DataFieldInfo<ScDevicePackage>.Create("Data");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo GetTimeField
        {
            get {
                return getTimeField;
            }
        }

		public static DataFieldInfo PackageNameField
        {
            get {
                return packageNameField;
            }
        }

		public static DataFieldInfo CountField
        {
            get {
                return countField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo DataField
        {
            get {
                return dataField;
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
		private DateTime? getTime;

		/// <summary>
		/// GetTime
		/// </summary>
		/// <value></value>
		[DataField("GetTime", IsNullable = true)]
        public DateTime? GetTime
        {
            get { 
            	return this.getTime; 
            }
            set { 
            	this.getTime = value; 
            }
        }
		private string packageName;

		/// <summary>
		/// PackageName
		/// </summary>
		/// <value></value>
		[DataField("PackageName")]
        public string PackageName
        {
            get { 
            	return this.packageName; 
            }
            set { 
            	this.packageName = value; 
            }
        }
		private int count;

		/// <summary>
		/// Count
		/// </summary>
		/// <value></value>
		[DataField("Count")]
        public int Count
        {
            get { 
            	return this.count; 
            }
            set { 
            	this.count = value; 
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
		private string data;

		/// <summary>
		/// Data
		/// </summary>
		/// <value></value>
		[DataField("Data", IsNullable = true)]
        public string Data
        {
            get { 
            	return this.data; 
            }
            set { 
            	this.data = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScDevicePackageHistory")]
    public partial class ScDevicePackageHistory : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDevicePackageHistory>.Create("Id");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<ScDevicePackageHistory>.Create("RecordTime");

		static readonly DataFieldInfo getTimeField = DataFieldInfo<ScDevicePackageHistory>.Create("GetTime");

		static readonly DataFieldInfo packageNameField = DataFieldInfo<ScDevicePackageHistory>.Create("PackageName");

		static readonly DataFieldInfo countField = DataFieldInfo<ScDevicePackageHistory>.Create("Count");

		static readonly DataFieldInfo statusField = DataFieldInfo<ScDevicePackageHistory>.Create("Status");

		static readonly DataFieldInfo dataField = DataFieldInfo<ScDevicePackageHistory>.Create("Data");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo GetTimeField
        {
            get {
                return getTimeField;
            }
        }

		public static DataFieldInfo PackageNameField
        {
            get {
                return packageNameField;
            }
        }

		public static DataFieldInfo CountField
        {
            get {
                return countField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo DataField
        {
            get {
                return dataField;
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
		private DateTime? getTime;

		/// <summary>
		/// GetTime
		/// </summary>
		/// <value></value>
		[DataField("GetTime", IsNullable = true)]
        public DateTime? GetTime
        {
            get { 
            	return this.getTime; 
            }
            set { 
            	this.getTime = value; 
            }
        }
		private string packageName;

		/// <summary>
		/// PackageName
		/// </summary>
		/// <value></value>
		[DataField("PackageName")]
        public string PackageName
        {
            get { 
            	return this.packageName; 
            }
            set { 
            	this.packageName = value; 
            }
        }
		private int count;

		/// <summary>
		/// Count
		/// </summary>
		/// <value></value>
		[DataField("Count")]
        public int Count
        {
            get { 
            	return this.count; 
            }
            set { 
            	this.count = value; 
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
		private string data;

		/// <summary>
		/// Data
		/// </summary>
		/// <value></value>
		[DataField("Data", IsNullable = true)]
        public string Data
        {
            get { 
            	return this.data; 
            }
            set { 
            	this.data = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScProfileInfo")]
    public partial class ScProfileInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScProfileInfo>.Create("Id");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<ScProfileInfo>.Create("RecordTime");

		static readonly DataFieldInfo flagField = DataFieldInfo<ScProfileInfo>.Create("Flag");

		static readonly DataFieldInfo domainField = DataFieldInfo<ScProfileInfo>.Create("Domain");

		static readonly DataFieldInfo pathField = DataFieldInfo<ScProfileInfo>.Create("Path");

		static readonly DataFieldInfo postDataRegexField = DataFieldInfo<ScProfileInfo>.Create("PostDataRegex");

		static readonly DataFieldInfo statusField = DataFieldInfo<ScProfileInfo>.Create("Status");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
            }
        }

		public static DataFieldInfo FlagField
        {
            get {
                return flagField;
            }
        }

		public static DataFieldInfo DomainField
        {
            get {
                return domainField;
            }
        }

		public static DataFieldInfo PathField
        {
            get {
                return pathField;
            }
        }

		public static DataFieldInfo PostDataRegexField
        {
            get {
                return postDataRegexField;
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
		private string domain;

		/// <summary>
		/// Domain
		/// </summary>
		/// <value></value>
		[DataField("Domain")]
        public string Domain
        {
            get { 
            	return this.domain; 
            }
            set { 
            	this.domain = value; 
            }
        }
		private string path;

		/// <summary>
		/// Path
		/// </summary>
		/// <value></value>
		[DataField("Path")]
        public string Path
        {
            get { 
            	return this.path; 
            }
            set { 
            	this.path = value; 
            }
        }
		private string postDataRegex;

		/// <summary>
		/// PostDataRegex
		/// </summary>
		/// <value></value>
		[DataField("PostDataRegex", IsNullable = true)]
        public string PostDataRegex
        {
            get { 
            	return this.postDataRegex; 
            }
            set { 
            	this.postDataRegex = value; 
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

}

