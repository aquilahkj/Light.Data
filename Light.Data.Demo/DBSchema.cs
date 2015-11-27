using System;
using System.Collections.Generic;
using System.Text;
using Light.Data;

namespace Light.Data.Demo
{
    [Serializable]
    [DataTable("RecommendData")]
    public partial class RecommendData : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<RecommendData>.Create("Id");

		static readonly DataFieldInfo recommendIdField = DataFieldInfo<RecommendData>.Create("RecommendId");

		static readonly DataFieldInfo contentField = DataFieldInfo<RecommendData>.Create("Content");

		static readonly DataFieldInfo contentTypeField = DataFieldInfo<RecommendData>.Create("ContentType");

		static readonly DataFieldInfo tagField = DataFieldInfo<RecommendData>.Create("Tag");

		static readonly DataFieldInfo priorityField = DataFieldInfo<RecommendData>.Create("Priority");

		static readonly DataFieldInfo recordTimeField = DataFieldInfo<RecommendData>.Create("RecordTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo RecommendIdField
        {
            get {
                return recommendIdField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo ContentTypeField
        {
            get {
                return contentTypeField;
            }
        }

		public static DataFieldInfo TagField
        {
            get {
                return tagField;
            }
        }

		public static DataFieldInfo PriorityField
        {
            get {
                return priorityField;
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
		private int recommendId;

		[DataField("RecommendId")]
        public int RecommendId
        {
            get { 
            	return this.recommendId; 
            }
            set { 
            	this.recommendId = value; 
            }
        }
		private string content;

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
		private short contentType;

		[DataField("ContentType")]
        public short ContentType
        {
            get { 
            	return this.contentType; 
            }
            set { 
            	this.contentType = value; 
            }
        }
		private string tag;

		[DataField("Tag")]
        public string Tag
        {
            get { 
            	return this.tag; 
            }
            set { 
            	this.tag = value; 
            }
        }
		private short priority;

		[DataField("Priority")]
        public short Priority
        {
            get { 
            	return this.priority; 
            }
            set { 
            	this.priority = value; 
            }
        }
		private DateTime recordTime;

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
    [DataTable("TaskBase")]
    public partial class TaskBase : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo taskIdField = DataFieldInfo<TaskBase>.Create("TaskId");

		static readonly DataFieldInfo positionField = DataFieldInfo<TaskBase>.Create("Position");

		static readonly DataFieldInfo priorityField = DataFieldInfo<TaskBase>.Create("Priority");

		static readonly DataFieldInfo startTimeField = DataFieldInfo<TaskBase>.Create("StartTime");

		static readonly DataFieldInfo endTimeField = DataFieldInfo<TaskBase>.Create("EndTime");

		static readonly DataFieldInfo addTimeField = DataFieldInfo<TaskBase>.Create("AddTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo TaskIdField
        {
            get {
                return taskIdField;
            }
        }

		public static DataFieldInfo PositionField
        {
            get {
                return positionField;
            }
        }

		public static DataFieldInfo PriorityField
        {
            get {
                return priorityField;
            }
        }

		public static DataFieldInfo StartTimeField
        {
            get {
                return startTimeField;
            }
        }

		public static DataFieldInfo EndTimeField
        {
            get {
                return endTimeField;
            }
        }

		public static DataFieldInfo AddTimeField
        {
            get {
                return addTimeField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string taskId;

		[DataField("TaskId", IsPrimaryKey = true)]
        public string TaskId
        {
            get { 
            	return this.taskId; 
            }
            set { 
            	this.taskId = value; 
            }
        }
		private short position;

		[DataField("Position")]
        public short Position
        {
            get { 
            	return this.position; 
            }
            set { 
            	this.position = value; 
            }
        }
		private ushort priority;

		[DataField("Priority")]
        public ushort Priority
        {
            get { 
            	return this.priority; 
            }
            set { 
            	this.priority = value; 
            }
        }
		private uint startTime;

		[DataField("StartTime")]
        public uint StartTime
        {
            get { 
            	return this.startTime; 
            }
            set { 
            	this.startTime = value; 
            }
        }
		private uint endTime;

		[DataField("EndTime")]
        public uint EndTime
        {
            get { 
            	return this.endTime; 
            }
            set { 
            	this.endTime = value; 
            }
        }
		private uint addTime;

		[DataField("AddTime")]
        public uint AddTime
        {
            get { 
            	return this.addTime; 
            }
            set { 
            	this.addTime = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("TaskContent")]
    public partial class TaskContent : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo taskIdField = DataFieldInfo<TaskContent>.Create("TaskId");

		static readonly DataFieldInfo contentIdField = DataFieldInfo<TaskContent>.Create("ContentId");

		static readonly DataFieldInfo priorityField = DataFieldInfo<TaskContent>.Create("Priority");

		static readonly DataFieldInfo contentTypeField = DataFieldInfo<TaskContent>.Create("ContentType");

		static readonly DataFieldInfo transField = DataFieldInfo<TaskContent>.Create("Trans");

		static readonly DataFieldInfo contentField = DataFieldInfo<TaskContent>.Create("Content");

		static readonly DataFieldInfo addTimeField = DataFieldInfo<TaskContent>.Create("AddTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo TaskIdField
        {
            get {
                return taskIdField;
            }
        }

		public static DataFieldInfo ContentIdField
        {
            get {
                return contentIdField;
            }
        }

		public static DataFieldInfo PriorityField
        {
            get {
                return priorityField;
            }
        }

		public static DataFieldInfo ContentTypeField
        {
            get {
                return contentTypeField;
            }
        }

		public static DataFieldInfo TransField
        {
            get {
                return transField;
            }
        }

		public static DataFieldInfo ContentField
        {
            get {
                return contentField;
            }
        }

		public static DataFieldInfo AddTimeField
        {
            get {
                return addTimeField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string taskId;

		[DataField("TaskId")]
        public string TaskId
        {
            get { 
            	return this.taskId; 
            }
            set { 
            	this.taskId = value; 
            }
        }
		private uint contentId;

		[DataField("ContentId", IsPrimaryKey = true)]
        public uint ContentId
        {
            get { 
            	return this.contentId; 
            }
            set { 
            	this.contentId = value; 
            }
        }
		private ushort priority;

		[DataField("Priority")]
        public ushort Priority
        {
            get { 
            	return this.priority; 
            }
            set { 
            	this.priority = value; 
            }
        }
		private ushort contentType;

		[DataField("ContentType")]
        public ushort ContentType
        {
            get { 
            	return this.contentType; 
            }
            set { 
            	this.contentType = value; 
            }
        }
		private ushort trans;

		[DataField("Trans")]
        public ushort Trans
        {
            get { 
            	return this.trans; 
            }
            set { 
            	this.trans = value; 
            }
        }
		private string content;

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
		private uint addTime;

		[DataField("AddTime")]
        public uint AddTime
        {
            get { 
            	return this.addTime; 
            }
            set { 
            	this.addTime = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("TaskRule")]
    public partial class TaskRule : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo taskIdField = DataFieldInfo<TaskRule>.Create("TaskId");

		static readonly DataFieldInfo ruleIdField = DataFieldInfo<TaskRule>.Create("RuleId");

		static readonly DataFieldInfo ruleTypeField = DataFieldInfo<TaskRule>.Create("RuleType");

		static readonly DataFieldInfo ruleContentField = DataFieldInfo<TaskRule>.Create("RuleContent");

		static readonly DataFieldInfo addTimeField = DataFieldInfo<TaskRule>.Create("AddTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo TaskIdField
        {
            get {
                return taskIdField;
            }
        }

		public static DataFieldInfo RuleIdField
        {
            get {
                return ruleIdField;
            }
        }

		public static DataFieldInfo RuleTypeField
        {
            get {
                return ruleTypeField;
            }
        }

		public static DataFieldInfo RuleContentField
        {
            get {
                return ruleContentField;
            }
        }

		public static DataFieldInfo AddTimeField
        {
            get {
                return addTimeField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string taskId;

		[DataField("TaskId")]
        public string TaskId
        {
            get { 
            	return this.taskId; 
            }
            set { 
            	this.taskId = value; 
            }
        }
		private uint ruleId;

		[DataField("RuleId", IsPrimaryKey = true)]
        public uint RuleId
        {
            get { 
            	return this.ruleId; 
            }
            set { 
            	this.ruleId = value; 
            }
        }
		private uint ruleType;

		[DataField("RuleType")]
        public uint RuleType
        {
            get { 
            	return this.ruleType; 
            }
            set { 
            	this.ruleType = value; 
            }
        }
		private string ruleContent;

		[DataField("RuleContent")]
        public string RuleContent
        {
            get { 
            	return this.ruleContent; 
            }
            set { 
            	this.ruleContent = value; 
            }
        }
		private uint addTime;

		[DataField("AddTime")]
        public uint AddTime
        {
            get { 
            	return this.addTime; 
            }
            set { 
            	this.addTime = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("TestUser")]
    public partial class TestUser : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo idField = DataFieldInfo<TestUser>.Create("Id");

		static readonly DataFieldInfo userNameField = DataFieldInfo<TestUser>.Create("UserName");

		static readonly DataFieldInfo passwordField = DataFieldInfo<TestUser>.Create("Password");

		static readonly DataFieldInfo statusField = DataFieldInfo<TestUser>.Create("Status");

		static readonly DataFieldInfo packetIdField = DataFieldInfo<TestUser>.Create("PacketId");

		static readonly DataFieldInfo regTimeField = DataFieldInfo<TestUser>.Create("RegTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo IdField
        {
            get {
                return idField;
            }
        }

		public static DataFieldInfo UserNameField
        {
            get {
                return userNameField;
            }
        }

		public static DataFieldInfo PasswordField
        {
            get {
                return passwordField;
            }
        }

		public static DataFieldInfo StatusField
        {
            get {
                return statusField;
            }
        }

		public static DataFieldInfo PacketIdField
        {
            get {
                return packetIdField;
            }
        }

		public static DataFieldInfo RegTimeField
        {
            get {
                return regTimeField;
            }
        }

    	#endregion

    	#region "Data Property"
		private int id;

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
		private string userName;

		[DataField("UserName")]
        public string UserName
        {
            get { 
            	return this.userName; 
            }
            set { 
            	this.userName = value; 
            }
        }
		private string password;

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
		private int? status;

		[DataField("Status", IsNullable = true)]
        public int? Status
        {
            get { 
            	return this.status; 
            }
            set { 
            	this.status = value; 
            }
        }
		private int? packetId;

		[DataField("PacketId", IsNullable = true)]
        public int? PacketId
        {
            get { 
            	return this.packetId; 
            }
            set { 
            	this.packetId = value; 
            }
        }
		private DateTime regTime;

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
		#endregion
    }

    [Serializable]
    [DataTable("UserNumberPackage")]
    public partial class UserNumberPackage : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo fileNameField = DataFieldInfo<UserNumberPackage>.Create("FileName");

		static readonly DataFieldInfo countField = DataFieldInfo<UserNumberPackage>.Create("Count");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<UserNumberPackage>.Create("LastModifyTime");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo FileNameField
        {
            get {
                return fileNameField;
            }
        }

		public static DataFieldInfo CountField
        {
            get {
                return countField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string fileName;

		[DataField("FileName", IsPrimaryKey = true)]
        public string FileName
        {
            get { 
            	return this.fileName; 
            }
            set { 
            	this.fileName = value; 
            }
        }
		private int count;

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
		private uint lastModifyTime;

		[DataField("LastModifyTime")]
        public uint LastModifyTime
        {
            get { 
            	return this.lastModifyTime; 
            }
            set { 
            	this.lastModifyTime = value; 
            }
        }
		#endregion
    }

}

