using System;

namespace Light.Data.Demo
{
	public class VehicleModel
	{
		#region "Data Property"
		private string vehicleCode;

		/// <summary>
		/// 车辆编码
		/// </summary>
		/// <value></value>
		[DataField("VehicleCode")]
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

		#region "Data Property"

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
}

