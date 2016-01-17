



using System;
using System.Collections.Generic;
using System.Text;
using Light.Data;

namespace Light.Data.Demo
{
    [Serializable]
    [DataTable("Bus_FleetInfo")]
    public partial class BusFleetInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo fleetCodeField = DataFieldInfo<BusFleetInfo>.Create("FleetCode");

		static readonly DataFieldInfo nameField = DataFieldInfo<BusFleetInfo>.Create("Name");

		static readonly DataFieldInfo isShieldField = DataFieldInfo<BusFleetInfo>.Create("IsShield");

		static readonly DataFieldInfo recordStatusField = DataFieldInfo<BusFleetInfo>.Create("RecordStatus");

		static readonly DataFieldInfo createTimeField = DataFieldInfo<BusFleetInfo>.Create("CreateTime");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<BusFleetInfo>.Create("LastModifyTime");

		static readonly DataFieldInfo remarkField = DataFieldInfo<BusFleetInfo>.Create("Remark");

		static readonly DataFieldInfo areaField = DataFieldInfo<BusFleetInfo>.Create("Area");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo FleetCodeField
        {
            get {
                return fleetCodeField;
            }
        }

		public static DataFieldInfo NameField
        {
            get {
                return nameField;
            }
        }

		public static DataFieldInfo IsShieldField
        {
            get {
                return isShieldField;
            }
        }

		public static DataFieldInfo RecordStatusField
        {
            get {
                return recordStatusField;
            }
        }

		public static DataFieldInfo CreateTimeField
        {
            get {
                return createTimeField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
            }
        }

		public static DataFieldInfo RemarkField
        {
            get {
                return remarkField;
            }
        }

		public static DataFieldInfo AreaField
        {
            get {
                return areaField;
            }
        }

    	#endregion

    	#region "Data Property"
		private string fleetCode;

		/// <summary>
		/// 车队编码
		/// </summary>
		/// <value></value>
		[DataField("FleetCode", IsPrimaryKey = true)]
        public string FleetCode
        {
            get { 
            	return this.fleetCode; 
            }
            set { 
            	this.fleetCode = value; 
            }
        }
		private string name;

		/// <summary>
		/// 车队名称
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
		private bool isShield;

		/// <summary>
		/// 是否屏蔽
		/// </summary>
		/// <value></value>
		[DataField("IsShield")]
        public bool IsShield
        {
            get { 
            	return this.isShield; 
            }
            set { 
            	this.isShield = value; 
            }
        }
		private int recordStatus;

		/// <summary>
		/// 状态
		/// </summary>
		/// <value></value>
		[DataField("RecordStatus")]
        public int RecordStatus
        {
            get { 
            	return this.recordStatus; 
            }
            set { 
            	this.recordStatus = value; 
            }
        }
		private DateTime createTime;

		/// <summary>
		/// 创建时间
		/// </summary>
		/// <value></value>
		[DataField("CreateTime")]
        public DateTime CreateTime
        {
            get { 
            	return this.createTime; 
            }
            set { 
            	this.createTime = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// 最后修改时间
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
		private string remark;

		/// <summary>
		/// 备注
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
		private string area;

		/// <summary>
		/// Area
		/// </summary>
		/// <value></value>
		[DataField("Area", IsNullable = true)]
        public string Area
        {
            get { 
            	return this.area; 
            }
            set { 
            	this.area = value; 
            }
        }
		#endregion
    }

    [Serializable]
    [DataTable("Bus_VehicleInfo")]
    public partial class BusVehicleInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo vehicleCodeField = DataFieldInfo<BusVehicleInfo>.Create("VehicleCode");

		static readonly DataFieldInfo fleetCodeField = DataFieldInfo<BusVehicleInfo>.Create("FleetCode");

		static readonly DataFieldInfo transportCharacterField = DataFieldInfo<BusVehicleInfo>.Create("TransportCharacter");

		static readonly DataFieldInfo transportTypeField = DataFieldInfo<BusVehicleInfo>.Create("TransportType");

		static readonly DataFieldInfo typeCodeField = DataFieldInfo<BusVehicleInfo>.Create("TypeCode");

		static readonly DataFieldInfo issueDateField = DataFieldInfo<BusVehicleInfo>.Create("IssueDate");

		static readonly DataFieldInfo modelField = DataFieldInfo<BusVehicleInfo>.Create("Model");

		static readonly DataFieldInfo plateNoField = DataFieldInfo<BusVehicleInfo>.Create("PlateNo");

		static readonly DataFieldInfo licenseField = DataFieldInfo<BusVehicleInfo>.Create("License");

		static readonly DataFieldInfo engineNoField = DataFieldInfo<BusVehicleInfo>.Create("EngineNo");

		static readonly DataFieldInfo vinField = DataFieldInfo<BusVehicleInfo>.Create("Vin");

		static readonly DataFieldInfo photographField = DataFieldInfo<BusVehicleInfo>.Create("Photograph");

		static readonly DataFieldInfo parkCodeField = DataFieldInfo<BusVehicleInfo>.Create("ParkCode");

		static readonly DataFieldInfo insuranceFeeField = DataFieldInfo<BusVehicleInfo>.Create("InsuranceFee");

		static readonly DataFieldInfo priceField = DataFieldInfo<BusVehicleInfo>.Create("Price");

		static readonly DataFieldInfo oldLevelField = DataFieldInfo<BusVehicleInfo>.Create("OldLevel");

		static readonly DataFieldInfo managementFeeField = DataFieldInfo<BusVehicleInfo>.Create("ManagementFee");

		static readonly DataFieldInfo maintainFeeField = DataFieldInfo<BusVehicleInfo>.Create("MaintainFee");

		static readonly DataFieldInfo parkingFeeField = DataFieldInfo<BusVehicleInfo>.Create("ParkingFee");

		static readonly DataFieldInfo descriptionField = DataFieldInfo<BusVehicleInfo>.Create("Description");

		static readonly DataFieldInfo recordStatusField = DataFieldInfo<BusVehicleInfo>.Create("RecordStatus");

		static readonly DataFieldInfo createTimeField = DataFieldInfo<BusVehicleInfo>.Create("CreateTime");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<BusVehicleInfo>.Create("LastModifyTime");

		static readonly DataFieldInfo remarkField = DataFieldInfo<BusVehicleInfo>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo VehicleCodeField
        {
            get {
                return vehicleCodeField;
            }
        }

		public static DataFieldInfo FleetCodeField
        {
            get {
                return fleetCodeField;
            }
        }

		public static DataFieldInfo TransportCharacterField
        {
            get {
                return transportCharacterField;
            }
        }

		public static DataFieldInfo TransportTypeField
        {
            get {
                return transportTypeField;
            }
        }

		public static DataFieldInfo TypeCodeField
        {
            get {
                return typeCodeField;
            }
        }

		public static DataFieldInfo IssueDateField
        {
            get {
                return issueDateField;
            }
        }

		public static DataFieldInfo ModelField
        {
            get {
                return modelField;
            }
        }

		public static DataFieldInfo PlateNoField
        {
            get {
                return plateNoField;
            }
        }

		public static DataFieldInfo LicenseField
        {
            get {
                return licenseField;
            }
        }

		public static DataFieldInfo EngineNoField
        {
            get {
                return engineNoField;
            }
        }

		public static DataFieldInfo VinField
        {
            get {
                return vinField;
            }
        }

		public static DataFieldInfo PhotographField
        {
            get {
                return photographField;
            }
        }

		public static DataFieldInfo ParkCodeField
        {
            get {
                return parkCodeField;
            }
        }

		public static DataFieldInfo InsuranceFeeField
        {
            get {
                return insuranceFeeField;
            }
        }

		public static DataFieldInfo PriceField
        {
            get {
                return priceField;
            }
        }

		public static DataFieldInfo OldLevelField
        {
            get {
                return oldLevelField;
            }
        }

		public static DataFieldInfo ManagementFeeField
        {
            get {
                return managementFeeField;
            }
        }

		public static DataFieldInfo MaintainFeeField
        {
            get {
                return maintainFeeField;
            }
        }

		public static DataFieldInfo ParkingFeeField
        {
            get {
                return parkingFeeField;
            }
        }

		public static DataFieldInfo DescriptionField
        {
            get {
                return descriptionField;
            }
        }

		public static DataFieldInfo RecordStatusField
        {
            get {
                return recordStatusField;
            }
        }

		public static DataFieldInfo CreateTimeField
        {
            get {
                return createTimeField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
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
		private string vehicleCode;

		/// <summary>
		/// 车辆编码
		/// </summary>
		/// <value></value>
		[DataField("VehicleCode", IsPrimaryKey = true)]
        public string VehicleCode
        {
            get { 
            	return this.vehicleCode; 
            }
            set { 
            	this.vehicleCode = value; 
            }
        }
		private string fleetCode;

		/// <summary>
		/// 所属车队编码
		/// </summary>
		/// <value></value>
		[DataField("FleetCode")]
        public string FleetCode
        {
            get { 
            	return this.fleetCode; 
            }
            set { 
            	this.fleetCode = value; 
            }
        }
		private int transportCharacter;

		/// <summary>
		/// 营运性质,0:省际包牌1:市际包牌2:县际包牌3:租赁车量
		/// </summary>
		/// <value></value>
		[DataField("TransportCharacter")]
        public int TransportCharacter
        {
            get { 
            	return this.transportCharacter; 
            }
            set { 
            	this.transportCharacter = value; 
            }
        }
		private int transportType;

		/// <summary>
		/// 过境牌照,0:无;1:中港;2:中澳;
		/// </summary>
		/// <value></value>
		[DataField("TransportType")]
        public int TransportType
        {
            get { 
            	return this.transportType; 
            }
            set { 
            	this.transportType = value; 
            }
        }
		private string typeCode;

		/// <summary>
		/// 车型编码
		/// </summary>
		/// <value></value>
		[DataField("TypeCode")]
        public string TypeCode
        {
            get { 
            	return this.typeCode; 
            }
            set { 
            	this.typeCode = value; 
            }
        }
		private DateTime issueDate;

		/// <summary>
		/// 行驶证发证日期
		/// </summary>
		/// <value></value>
		[DataField("IssueDate")]
        public DateTime IssueDate
        {
            get { 
            	return this.issueDate; 
            }
            set { 
            	this.issueDate = value; 
            }
        }
		private string model;

		/// <summary>
		/// 车辆品牌
		/// </summary>
		/// <value></value>
		[DataField("Model", IsNullable = true)]
        public string Model
        {
            get { 
            	return this.model; 
            }
            set { 
            	this.model = value; 
            }
        }
		private string plateNo;

		/// <summary>
		/// 车牌号码
		/// </summary>
		/// <value></value>
		[DataField("PlateNo")]
        public string PlateNo
        {
            get { 
            	return this.plateNo; 
            }
            set { 
            	this.plateNo = value; 
            }
        }
		private string license;

		/// <summary>
		/// 行驶证
		/// </summary>
		/// <value></value>
		[DataField("License")]
        public string License
        {
            get { 
            	return this.license; 
            }
            set { 
            	this.license = value; 
            }
        }
		private string engineNo;

		/// <summary>
		/// 发动机号
		/// </summary>
		/// <value></value>
		[DataField("EngineNo")]
        public string EngineNo
        {
            get { 
            	return this.engineNo; 
            }
            set { 
            	this.engineNo = value; 
            }
        }
		private string vin;

		/// <summary>
		/// 车架号
		/// </summary>
		/// <value></value>
		[DataField("VIN")]
        public string Vin
        {
            get { 
            	return this.vin; 
            }
            set { 
            	this.vin = value; 
            }
        }
		private string photograph;

		/// <summary>
		/// 图片,多图以|分隔
		/// </summary>
		/// <value></value>
		[DataField("Photograph", IsNullable = true)]
        public string Photograph
        {
            get { 
            	return this.photograph; 
            }
            set { 
            	this.photograph = value; 
            }
        }
		private string parkCode;

		/// <summary>
		/// 车辆停放地点
		/// </summary>
		/// <value></value>
		[DataField("ParkCode")]
        public string ParkCode
        {
            get { 
            	return this.parkCode; 
            }
            set { 
            	this.parkCode = value; 
            }
        }
		private decimal insuranceFee;

		/// <summary>
		/// 每年保险费
		/// </summary>
		/// <value></value>
		[DataField("InsuranceFee")]
        public decimal InsuranceFee
        {
            get { 
            	return this.insuranceFee; 
            }
            set { 
            	this.insuranceFee = value; 
            }
        }
		private decimal price;

		/// <summary>
		/// 车辆总价
		/// </summary>
		/// <value></value>
		[DataField("Price")]
        public decimal Price
        {
            get { 
            	return this.price; 
            }
            set { 
            	this.price = value; 
            }
        }
		private int oldLevel;

		/// <summary>
		/// 新旧程度
		/// </summary>
		/// <value></value>
		[DataField("OldLevel")]
        public int OldLevel
        {
            get { 
            	return this.oldLevel; 
            }
            set { 
            	this.oldLevel = value; 
            }
        }
		private decimal managementFee;

		/// <summary>
		/// 每月管理费
		/// </summary>
		/// <value></value>
		[DataField("ManagementFee")]
        public decimal ManagementFee
        {
            get { 
            	return this.managementFee; 
            }
            set { 
            	this.managementFee = value; 
            }
        }
		private decimal maintainFee;

		/// <summary>
		/// 每月保养费
		/// </summary>
		/// <value></value>
		[DataField("MaintainFee")]
        public decimal MaintainFee
        {
            get { 
            	return this.maintainFee; 
            }
            set { 
            	this.maintainFee = value; 
            }
        }
		private decimal parkingFee;

		/// <summary>
		/// 每月停车费
		/// </summary>
		/// <value></value>
		[DataField("ParkingFee")]
        public decimal ParkingFee
        {
            get { 
            	return this.parkingFee; 
            }
            set { 
            	this.parkingFee = value; 
            }
        }
		private string description;

		/// <summary>
		/// 描述
		/// </summary>
		/// <value></value>
		[DataField("Description", IsNullable = true)]
        public string Description
        {
            get { 
            	return this.description; 
            }
            set { 
            	this.description = value; 
            }
        }
		private int recordStatus;

		/// <summary>
		/// 车辆状态(0.删除,1.正常,2.坏车,3.报废)
		/// </summary>
		/// <value></value>
		[DataField("RecordStatus")]
        public int RecordStatus
        {
            get { 
            	return this.recordStatus; 
            }
            set { 
            	this.recordStatus = value; 
            }
        }
		private DateTime createTime;

		/// <summary>
		/// 创建时间
		/// </summary>
		/// <value></value>
		[DataField("CreateTime")]
        public DateTime CreateTime
        {
            get { 
            	return this.createTime; 
            }
            set { 
            	this.createTime = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// 最后修改时间
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
		private string remark;

		/// <summary>
		/// 备注
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
    [DataTable("Bus_VehicleStructInfo")]
    public partial class BusVehicleStructInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo structCodeField = DataFieldInfo<BusVehicleStructInfo>.Create("StructCode");

		static readonly DataFieldInfo nameField = DataFieldInfo<BusVehicleStructInfo>.Create("Name");

		static readonly DataFieldInfo recordStatusField = DataFieldInfo<BusVehicleStructInfo>.Create("RecordStatus");

		static readonly DataFieldInfo createTimeField = DataFieldInfo<BusVehicleStructInfo>.Create("CreateTime");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<BusVehicleStructInfo>.Create("LastModifyTime");

		static readonly DataFieldInfo remarkField = DataFieldInfo<BusVehicleStructInfo>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo StructCodeField
        {
            get {
                return structCodeField;
            }
        }

		public static DataFieldInfo NameField
        {
            get {
                return nameField;
            }
        }

		public static DataFieldInfo RecordStatusField
        {
            get {
                return recordStatusField;
            }
        }

		public static DataFieldInfo CreateTimeField
        {
            get {
                return createTimeField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
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
		private string structCode;

		/// <summary>
		/// 车辆结构编码
		/// </summary>
		/// <value></value>
		[DataField("StructCode", IsPrimaryKey = true)]
        public string StructCode
        {
            get { 
            	return this.structCode; 
            }
            set { 
            	this.structCode = value; 
            }
        }
		private string name;

		/// <summary>
		/// 名称
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
		private int recordStatus;

		/// <summary>
		/// 状态
		/// </summary>
		/// <value></value>
		[DataField("RecordStatus")]
        public int RecordStatus
        {
            get { 
            	return this.recordStatus; 
            }
            set { 
            	this.recordStatus = value; 
            }
        }
		private DateTime createTime;

		/// <summary>
		/// 创建时间
		/// </summary>
		/// <value></value>
		[DataField("CreateTime")]
        public DateTime CreateTime
        {
            get { 
            	return this.createTime; 
            }
            set { 
            	this.createTime = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// 最后修改时间
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
		private string remark;

		/// <summary>
		/// 备注
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
    [DataTable("Bus_VehicleTypeInfo")]
    public partial class BusVehicleTypeInfo : DataTableEntity
    {
    	#region "Static Field"
		static readonly DataFieldInfo vehicleTypeCodeField = DataFieldInfo<BusVehicleTypeInfo>.Create("VehicleTypeCode");

		static readonly DataFieldInfo structCodeField = DataFieldInfo<BusVehicleTypeInfo>.Create("StructCode");

		static readonly DataFieldInfo seatsField = DataFieldInfo<BusVehicleTypeInfo>.Create("Seats");

		static readonly DataFieldInfo optimalProfitField = DataFieldInfo<BusVehicleTypeInfo>.Create("OptimalProfit");

		static readonly DataFieldInfo fuelConsumptionField = DataFieldInfo<BusVehicleTypeInfo>.Create("FuelConsumption");

		static readonly DataFieldInfo recordStatusField = DataFieldInfo<BusVehicleTypeInfo>.Create("RecordStatus");

		static readonly DataFieldInfo createTimeField = DataFieldInfo<BusVehicleTypeInfo>.Create("CreateTime");

		static readonly DataFieldInfo lastModifyTimeField = DataFieldInfo<BusVehicleTypeInfo>.Create("LastModifyTime");

		static readonly DataFieldInfo remarkField = DataFieldInfo<BusVehicleTypeInfo>.Create("Remark");

    	#endregion

    	#region "Static DataFieldInfo"
		public static DataFieldInfo VehicleTypeCodeField
        {
            get {
                return vehicleTypeCodeField;
            }
        }

		public static DataFieldInfo StructCodeField
        {
            get {
                return structCodeField;
            }
        }

		public static DataFieldInfo SeatsField
        {
            get {
                return seatsField;
            }
        }

		public static DataFieldInfo OptimalProfitField
        {
            get {
                return optimalProfitField;
            }
        }

		public static DataFieldInfo FuelConsumptionField
        {
            get {
                return fuelConsumptionField;
            }
        }

		public static DataFieldInfo RecordStatusField
        {
            get {
                return recordStatusField;
            }
        }

		public static DataFieldInfo CreateTimeField
        {
            get {
                return createTimeField;
            }
        }

		public static DataFieldInfo LastModifyTimeField
        {
            get {
                return lastModifyTimeField;
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
		private string vehicleTypeCode;

		/// <summary>
		/// 车型编码
		/// </summary>
		/// <value></value>
		[DataField("VehicleTypeCode", IsPrimaryKey = true)]
        public string VehicleTypeCode
        {
            get { 
            	return this.vehicleTypeCode; 
            }
            set { 
            	this.vehicleTypeCode = value; 
            }
        }
		private string structCode;

		/// <summary>
		/// 车辆结构编码
		/// </summary>
		/// <value></value>
		[DataField("StructCode")]
        public string StructCode
        {
            get { 
            	return this.structCode; 
            }
            set { 
            	this.structCode = value; 
            }
        }
		private int seats;

		/// <summary>
		/// 座位数
		/// </summary>
		/// <value></value>
		[DataField("Seats")]
        public int Seats
        {
            get { 
            	return this.seats; 
            }
            set { 
            	this.seats = value; 
            }
        }
		private decimal optimalProfit;

		/// <summary>
		/// 最佳利润
		/// </summary>
		/// <value></value>
		[DataField("OptimalProfit")]
        public decimal OptimalProfit
        {
            get { 
            	return this.optimalProfit; 
            }
            set { 
            	this.optimalProfit = value; 
            }
        }
		private decimal fuelConsumption;

		/// <summary>
		/// 单位油耗(公里)
		/// </summary>
		/// <value></value>
		[DataField("FuelConsumption")]
        public decimal FuelConsumption
        {
            get { 
            	return this.fuelConsumption; 
            }
            set { 
            	this.fuelConsumption = value; 
            }
        }
		private int recordStatus;

		/// <summary>
		/// 状态
		/// </summary>
		/// <value></value>
		[DataField("RecordStatus")]
        public int RecordStatus
        {
            get { 
            	return this.recordStatus; 
            }
            set { 
            	this.recordStatus = value; 
            }
        }
		private DateTime createTime;

		/// <summary>
		/// 创建时间
		/// </summary>
		/// <value></value>
		[DataField("CreateTime")]
        public DateTime CreateTime
        {
            get { 
            	return this.createTime; 
            }
            set { 
            	this.createTime = value; 
            }
        }
		private DateTime lastModifyTime;

		/// <summary>
		/// 最后修改时间
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
		private string remark;

		/// <summary>
		/// 备注
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
		private int gender;

		/// <summary>
		/// Gender
		/// </summary>
		/// <value></value>
		[DataField("Gender")]
        public int Gender
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
		private double? hotRate;

		/// <summary>
		/// HotRate
		/// </summary>
		/// <value></value>
		[DataField("HotRate", IsNullable = true)]
        public double? HotRate
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

