



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
    [DataTable("ScDataHourStat")]
    public partial class ScDataHourStat : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDataHourStat>.Create("Id");

		static readonly DataFieldInfo statDateField = DataFieldInfo<ScDataHourStat>.Create("StatDate");

		static readonly DataFieldInfo hourField = DataFieldInfo<ScDataHourStat>.Create("Hour");

		static readonly DataFieldInfo flagField = DataFieldInfo<ScDataHourStat>.Create("Flag");

		static readonly DataFieldInfo midField = DataFieldInfo<ScDataHourStat>.Create("Mid");

		static readonly DataFieldInfo codeField = DataFieldInfo<ScDataHourStat>.Create("Code");

		static readonly DataFieldInfo pvField = DataFieldInfo<ScDataHourStat>.Create("Pv");

		static readonly DataFieldInfo uvField = DataFieldInfo<ScDataHourStat>.Create("Uv");

		static readonly DataFieldInfo pvRateField = DataFieldInfo<ScDataHourStat>.Create("PvRate");

		static readonly DataFieldInfo uvRateField = DataFieldInfo<ScDataHourStat>.Create("UvRate");

		static readonly DataFieldInfo statTimeField = DataFieldInfo<ScDataHourStat>.Create("StatTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo StatDateField
        {
            get {
                return statDateField;
            }
        }

		public static DataFieldInfo HourField
        {
            get {
                return hourField;
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

		public static DataFieldInfo PvRateField
        {
            get {
                return pvRateField;
            }
        }

		public static DataFieldInfo UvRateField
        {
            get {
                return uvRateField;
            }
        }

		public static DataFieldInfo StatTimeField
        {
            get {
                return statTimeField;
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
		private DateTime statDate;

		/// <summary>
		/// StatDate
		/// </summary>
		/// <value></value>
		[DataField("StatDate")]
        public DateTime StatDate
        {
            get { 
            	return this.statDate; 
            }
            set { 
            	this.statDate = value; 
            }
        }
		private int hour;

		/// <summary>
		/// Hour
		/// </summary>
		/// <value></value>
		[DataField("Hour")]
        public int Hour
        {
            get { 
            	return this.hour; 
            }
            set { 
            	this.hour = value; 
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
		[DataField("Mid")]
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
		private int uv;

		/// <summary>
		/// Uv
		/// </summary>
		/// <value></value>
		[DataField("Uv")]
        public int Uv
        {
            get { 
            	return this.uv; 
            }
            set { 
            	this.uv = value; 
            }
        }
		private double pvRate;

		/// <summary>
		/// PvRate
		/// </summary>
		/// <value></value>
		[DataField("PvRate")]
        public double PvRate
        {
            get { 
            	return this.pvRate; 
            }
            set { 
            	this.pvRate = value; 
            }
        }
		private double uvRate;

		/// <summary>
		/// UvRate
		/// </summary>
		/// <value></value>
		[DataField("UvRate")]
        public double UvRate
        {
            get { 
            	return this.uvRate; 
            }
            set { 
            	this.uvRate = value; 
            }
        }
		private DateTime statTime;

		/// <summary>
		/// StatTime
		/// </summary>
		/// <value></value>
		[DataField("StatTime")]
        public DateTime StatTime
        {
            get { 
            	return this.statTime; 
            }
            set { 
            	this.statTime = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("ScDataHourStatCalculate")]
    public partial class ScDataHourStatCalculate : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDataHourStatCalculate>.Create("Id");

		static readonly DataFieldInfo statDateField = DataFieldInfo<ScDataHourStatCalculate>.Create("StatDate");

		static readonly DataFieldInfo weekField = DataFieldInfo<ScDataHourStatCalculate>.Create("Week");

		static readonly DataFieldInfo hourField = DataFieldInfo<ScDataHourStatCalculate>.Create("Hour");

		static readonly DataFieldInfo flagField = DataFieldInfo<ScDataHourStatCalculate>.Create("Flag");

		static readonly DataFieldInfo midField = DataFieldInfo<ScDataHourStatCalculate>.Create("Mid");

		static readonly DataFieldInfo codeField = DataFieldInfo<ScDataHourStatCalculate>.Create("Code");

		static readonly DataFieldInfo stockNameField = DataFieldInfo<ScDataHourStatCalculate>.Create("StockName");

		static readonly DataFieldInfo d1OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D1Offset");

		static readonly DataFieldInfo d2OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D2Offset");

		static readonly DataFieldInfo d3OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D3Offset");

		static readonly DataFieldInfo d4OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D4Offset");

		static readonly DataFieldInfo d5OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D5Offset");

		static readonly DataFieldInfo d6OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D6Offset");

		static readonly DataFieldInfo d7OffsetField = DataFieldInfo<ScDataHourStatCalculate>.Create("D7Offset");

		static readonly DataFieldInfo contentField = DataFieldInfo<ScDataHourStatCalculate>.Create("Content");

		static readonly DataFieldInfo dataField = DataFieldInfo<ScDataHourStatCalculate>.Create("Data");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<ScDataHourStatCalculate>.Create("RecordTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo StatDateField
        {
            get {
                return statDateField;
            }
        }

		public static DataFieldInfo WeekField
        {
            get {
                return weekField;
            }
        }

		public static DataFieldInfo HourField
        {
            get {
                return hourField;
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

		public static DataFieldInfo StockNameField
        {
            get {
                return stockNameField;
            }
        }

		public static DataFieldInfo D1OffsetField
        {
            get {
                return d1OffsetField;
            }
        }

		public static DataFieldInfo D2OffsetField
        {
            get {
                return d2OffsetField;
            }
        }

		public static DataFieldInfo D3OffsetField
        {
            get {
                return d3OffsetField;
            }
        }

		public static DataFieldInfo D4OffsetField
        {
            get {
                return d4OffsetField;
            }
        }

		public static DataFieldInfo D5OffsetField
        {
            get {
                return d5OffsetField;
            }
        }

		public static DataFieldInfo D6OffsetField
        {
            get {
                return d6OffsetField;
            }
        }

		public static DataFieldInfo D7OffsetField
        {
            get {
                return d7OffsetField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo DataField
        {
            get {
                return dataField;
            }
        }

		public static DataFieldInfo RecordTimeField
        {
            get {
                return recordTimeField;
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
		private DateTime statDate;

		/// <summary>
		/// StatDate
		/// </summary>
		/// <value></value>
		[DataField("StatDate")]
        public DateTime StatDate
        {
            get { 
            	return this.statDate; 
            }
            set { 
            	this.statDate = value; 
            }
        }
		private int week;

		/// <summary>
		/// Week
		/// </summary>
		/// <value></value>
		[DataField("Week")]
        public int Week
        {
            get { 
            	return this.week; 
            }
            set { 
            	this.week = value; 
            }
        }
		private int hour;

		/// <summary>
		/// Hour
		/// </summary>
		/// <value></value>
		[DataField("Hour")]
        public int Hour
        {
            get { 
            	return this.hour; 
            }
            set { 
            	this.hour = value; 
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
		[DataField("Mid")]
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
		private string stockName;

		/// <summary>
		/// StockName
		/// </summary>
		/// <value></value>
		[DataField("StockName")]
        public string StockName
        {
            get { 
            	return this.stockName; 
            }
            set { 
            	this.stockName = value; 
            }
        }
		private double d1Offset;

		/// <summary>
		/// D1Offset
		/// </summary>
		/// <value></value>
		[DataField("D1Offset")]
        public double D1Offset
        {
            get { 
            	return this.d1Offset; 
            }
            set { 
            	this.d1Offset = value; 
            }
        }
		private double d2Offset;

		/// <summary>
		/// D2Offset
		/// </summary>
		/// <value></value>
		[DataField("D2Offset")]
        public double D2Offset
        {
            get { 
            	return this.d2Offset; 
            }
            set { 
            	this.d2Offset = value; 
            }
        }
		private double d3Offset;

		/// <summary>
		/// D3Offset
		/// </summary>
		/// <value></value>
		[DataField("D3Offset")]
        public double D3Offset
        {
            get { 
            	return this.d3Offset; 
            }
            set { 
            	this.d3Offset = value; 
            }
        }
		private double d4Offset;

		/// <summary>
		/// D4Offset
		/// </summary>
		/// <value></value>
		[DataField("D4Offset")]
        public double D4Offset
        {
            get { 
            	return this.d4Offset; 
            }
            set { 
            	this.d4Offset = value; 
            }
        }
		private double d5Offset;

		/// <summary>
		/// D5Offset
		/// </summary>
		/// <value></value>
		[DataField("D5Offset")]
        public double D5Offset
        {
            get { 
            	return this.d5Offset; 
            }
            set { 
            	this.d5Offset = value; 
            }
        }
		private double d6Offset;

		/// <summary>
		/// D6Offset
		/// </summary>
		/// <value></value>
		[DataField("D6Offset")]
        public double D6Offset
        {
            get { 
            	return this.d6Offset; 
            }
            set { 
            	this.d6Offset = value; 
            }
        }
		private double d7Offset;

		/// <summary>
		/// D7Offset
		/// </summary>
		/// <value></value>
		[DataField("D7Offset")]
        public double D7Offset
        {
            get { 
            	return this.d7Offset; 
            }
            set { 
            	this.d7Offset = value; 
            }
        }
		private string content;

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		[DataField("Content", IsNullable = true)]
        public string Content
        {
            get { 
            	return this.content; 
            }
            set { 
            	this.content = value; 
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
		#endregion
    }

    [Serializable]
    [DataTable("ScDataHourStatLog")]
    public partial class ScDataHourStatLog : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<ScDataHourStatLog>.Create("Id");

		static readonly DataFieldInfo statDateField = DataFieldInfo<ScDataHourStatLog>.Create("StatDate");

		static readonly DataFieldInfo hourField = DataFieldInfo<ScDataHourStatLog>.Create("Hour");

		static readonly DataFieldInfo processCountField = DataFieldInfo<ScDataHourStatLog>.Create("ProcessCount");

		static readonly DataFieldInfo processTimeField = DataFieldInfo<ScDataHourStatLog>.Create("ProcessTime");

		static readonly DataFieldInfo consumeTimeField = DataFieldInfo<ScDataHourStatLog>.Create("ConsumeTime");

		static readonly DataFieldInfo statusField = DataFieldInfo<ScDataHourStatLog>.Create("Status");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo StatDateField
        {
            get {
                return statDateField;
            }
        }

		public static DataFieldInfo HourField
        {
            get {
                return hourField;
            }
        }

		public static DataFieldInfo ProcessCountField
        {
            get {
                return processCountField;
            }
        }

		public static DataFieldInfo ProcessTimeField
        {
            get {
                return processTimeField;
            }
        }

		public static DataFieldInfo ConsumeTimeField
        {
            get {
                return consumeTimeField;
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
		private DateTime statDate;

		/// <summary>
		/// StatDate
		/// </summary>
		/// <value></value>
		[DataField("StatDate")]
        public DateTime StatDate
        {
            get { 
            	return this.statDate; 
            }
            set { 
            	this.statDate = value; 
            }
        }
		private int hour;

		/// <summary>
		/// Hour
		/// </summary>
		/// <value></value>
		[DataField("Hour")]
        public int Hour
        {
            get { 
            	return this.hour; 
            }
            set { 
            	this.hour = value; 
            }
        }
		private int processCount;

		/// <summary>
		/// ProcessCount
		/// </summary>
		/// <value></value>
		[DataField("ProcessCount")]
        public int ProcessCount
        {
            get { 
            	return this.processCount; 
            }
            set { 
            	this.processCount = value; 
            }
        }
		private DateTime processTime;

		/// <summary>
		/// ProcessTime
		/// </summary>
		/// <value></value>
		[DataField("ProcessTime")]
        public DateTime ProcessTime
        {
            get { 
            	return this.processTime; 
            }
            set { 
            	this.processTime = value; 
            }
        }
		private int consumeTime;

		/// <summary>
		/// ConsumeTime
		/// </summary>
		/// <value></value>
		[DataField("ConsumeTime")]
        public int ConsumeTime
        {
            get { 
            	return this.consumeTime; 
            }
            set { 
            	this.consumeTime = value; 
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
		private int uv;

		/// <summary>
		/// Uv
		/// </summary>
		/// <value></value>
		[DataField("Uv")]
        public int Uv
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

    [Serializable]
    [DataTable("ScStockInfo")]
    public partial class ScStockInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo midField = DataFieldInfo<ScStockInfo>.Create("Mid");

		static readonly DataFieldInfo codeField = DataFieldInfo<ScStockInfo>.Create("Code");

		static readonly DataFieldInfo stockNameField = DataFieldInfo<ScStockInfo>.Create("StockName");

    	#endregion

    	#region "Static DataFieldInfo"
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

		public static DataFieldInfo StockNameField
        {
            get {
                return stockNameField;
            }
        }

    	#endregion

    	#region "Data Property"
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
		private string stockName;

		/// <summary>
		/// StockName
		/// </summary>
		/// <value></value>
		[DataField("StockName")]
        public string StockName
        {
            get { 
            	return this.stockName; 
            }
            set { 
            	this.stockName = value; 
            }
        }
		#endregion
    }

}

