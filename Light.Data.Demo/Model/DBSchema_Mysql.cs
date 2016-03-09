



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
		[DataField("Check_LevelTypeString", IsNullable = true)]
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
		[DataField("Check_LevelTypeString", IsNullable = true)]
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
		[DataField("Check_LevelTypeString", IsNullable = true)]
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

