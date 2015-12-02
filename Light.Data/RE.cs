using System;

namespace Light.Data
{
	internal static class RE
	{
		/// <summary>
		/// 聚合函数字段{0}已存在
		/// </summary>
		public const string AggregateFunctionFieldIsExists = "aggregate function field {0} is exists";
		/// <summary>
		/// 聚合类型非指定的类型{0}
		/// </summary>
		public const string AggregateTypeIsNotSpecifyType = "aggregate type {0} is not specify type";
		/// <summary>
		/// 统计字段{0}类型不合法
		/// </summary>
		public const string AggregationFieldIsInvaild = "aggregation field {0} is invaild";
		/// <summary>
		/// 统计映射表中没有统计字段
		/// </summary>
		public const string AggregationFieldsIsNotExists = "aggregation fields is not exists";
		/// <summary>
		/// 统计映射不能进行单列查询操作
		/// </summary>
		public const string AggregationMappingCanNotSelectSingle = "aggregation mapping can not select single";
		/// <summary>
		/// 统计表关联错误
		/// </summary>
		public const string AggregationMappingRelateError = "aggregation mapping relate error";
		/// <summary>
		/// 统计结果为空
		/// </summary>
		public const string AggregationResultIsNull = "aggregation result is null";
		/// <summary>
		/// 复合字段没有子字段
		/// </summary>
		public const string ComplexFieldHaveNotSubFields = "complex field have not sub fields";
		/// <summary>
		/// "{0}"的数据字段"{1}"不存在
		/// </summary>
		public const string ConfigDataFieldIsNotExists = "\"{0}\" config data field {1} is not exists";
		/// <summary>
		/// "{0}"的数据字段名为空值
		/// </summary>
		public const string ConfigDataFieldNameIsEmpty = "\"{0}\" config data field name is empty";
		/// <summary>
		/// 配置数据读取出错,数据节点并非"{0}"
		/// </summary>
		public const string ConfigDataLoadError = "\"{0}\" config data load error";
		/// <summary>
		/// 配置数据类型{0}的值为空
		/// </summary>
		public const string ConfigDataTypeValueIsEmpty = "config data type {0} value is empty";
		/// <summary>
		/// 数据库连接配置不存在
		/// </summary>
		public const string ConnectionSettingIsNotExists = "connection setting is not exists";
		/// <summary>
		/// 该数据库不支持内分页
		/// </summary>
		public const string DataBaseNotSupportInnerPage = "database not support inner page";
		/// <summary>
		/// 数据连接上下文不存在
		/// </summary>
		public const string DataContextIsNotExists = "DataContext is not exists";
		/// <summary>
		/// 数据映射表中没设定数据字段
		/// </summary>
		public const string DataFieldsIsNotExists = "data fields is not exists";
		/// <summary>
		/// 数据表与统计字段不匹配
		/// </summary>
		public const string DataMappingIsNotMatchAggregateField = "data mapping is not match aggregate field";
		/// <summary>
		/// 指定的查询表与统计表达式不匹配
		/// </summary>
		public const string DataMappingIsNotMatchAggregationExpression = "data mapping is not match aggregation expression";
		/// <summary>
		/// 指定的查询表与排序表达式不匹配
		/// </summary>
		public const string DataMappingIsNotMatchOrderExpression = "data mapping is not match order expression";
		/// <summary>
		/// 指定的查询表与查询表达式不匹配
		/// </summary>
		public const string DataMappingIsNotMatchQueryExpression = "data mapping is not match query expression";
		/// <summary>
		/// 表达式的查询表不一致
		/// </summary>
		public const string DataMappingOfExpressionIsNotMatch = "data mapping of expression is not match";
		/// <summary>
		/// 数据表不允许多少自增字段
		/// </summary>
		public const string DataTableNotAllowMoreIdentityField = "data table not allow more identity field";
		/// <summary>
		/// 数据表没有指定自增ID
		/// </summary>
		public const string DataTableNotIdentityField = "data table not identity field";
		/// <summary>
		/// {0}的值不能为空值
		/// </summary>
		public const string DataValueIsNotAllowEmply = "data value {0} is not allow emply";
		/// <summary>
		/// 动态汇总字段不存在
		/// </summary>
		public const string DynamicAggregateFieldIsNotExists = "dynamic aggregate field is not exists";
		/// <summary>
		/// 数据枚举值不允许为0
		/// </summary>
		public const string EnumerableLengthNotAllowIsZero = "enumerable length not allow is zero";
		/// <summary>
		/// 字段数据映射类型不匹配
		/// </summary>
		public const string FieldIsNotMatchDataMapping = "field is not match data mapping";
		/// <summary>
		/// 映射字段{0}不存在
		/// </summary>
		public const string FieldMappingIsNotExists = "mapping field {0} is not exists";
		/// <summary>
		/// 字段名称"{0}"不合法
		/// </summary>
		public const string FieldNameIsInvalid = "field name {0} is invalid";
		/// <summary>
		/// 分组字段名{0}已存在
		/// </summary>
		public const string GroupNameFieldIsExists = "group name field {0} is exists";
		/// <summary>
		/// 输入值不允许为空值
		/// </summary>
		public const string InputValueIsNotAllowNull = "input value is not allow null";
		/// <summary>
		/// 加载数据表配置失败
		/// </summary>
		public const string LoadDataTableConfigFaild = "load data table config faild";
		/// <summary>
		/// 无定义数据映射特性
		/// </summary>
		public const string NotDefineDataMappingAttribute = "not define data mapping attribute";
		/// <summary>
		/// 非原始字段不能使用单列查询
		/// </summary>
		public const string OnlyPrimitiveFieldCanSelectSingle = "only primitive field can select single";
		/// <summary>
		/// 随机排序字段不存在
		/// </summary>
		public const string RandomOrderFieldIsNotExists = "random order field is not exists";
		/// <summary>
		/// 随机排序不允许做排序组合
		/// </summary>
		public const string RandomOrderForbitCatch = "random order forbit catch";
		/// <summary>
		/// 取值范围数量值不合法
		/// </summary>
		public const string RegionSizeInvaild = "region size invaild";
		/// <summary>
		/// 取值范围开始值不合法
		/// </summary>
		public const string RegionStartInvaild = "region start invaild";
		/// <summary>
		/// 关联键值{0}并非原始数据字段
		/// </summary>
		public const string RelationKeyIsNotPrimitiveField = "relation key {0} is not primitive field";
		/// <summary>
		/// 关联映射{0}不存在
		/// </summary>
		public const string RelationMappingIsNotExists = "relation mapping {0} is not exists";
		/// <summary>
		/// 关联映射主键已存在
		/// </summary>
		public const string RelationMasterKeyIsExists = "relation master key is exists";
		/// <summary>
		/// 单列查询类型错误
		/// </summary>
		public const string SingleFieldSelectTypeError = "single field select type error";
		/// <summary>
		/// 指定的类型并非统计表类型
		/// </summary>
		public const string TheDataMappingIsNotAggregationMapping = "the data mapping is not aggregation mapping";
		/// <summary>
		/// 指定的类型并非实体表类型
		/// </summary>
		public const string TheDataMappingIsNotDataEntityMapping = "the data mapping is not data entity mapping";
		/// <summary>
		/// 指定的类型并非数据表类型
		/// </summary>
		public const string TheDataMappingIsNotDataTableMapping = "the data mapping is not data table mapping";
		/// <summary>
		/// 指定表并非数据表,不能批量删除
		/// </summary>
		public const string TheDataMappingNotAllowDeleteMass = "the data mapping not allow delete mass";
		/// <summary>
		/// 主键数目不匹配
		/// </summary>
		public const string TheNumberOfPrimaryKeysIsNotMatch = "the number of primary keys is not match";
		/// <summary>
		/// 关联类的集合类型非IList
		/// </summary>
		public const string TheRelationTypeNotIList = "the relation type not IList";
		/// <summary>
		/// 统计汇总字段类型不正确
		/// </summary>
		public const string TheTypeOfAggregationFieldIsNotRight = "the type of aggregation field is not right";
		/// <summary>
		/// 字段类型不正确
		/// </summary>
		public const string TheTypeOfDataFieldIsNotRight = "the type of data field is not right";
		/// <summary>
		/// 表的自增字段类型必须为Int32和Int64
		/// </summary>
		public const string TheTypeOfIdentityFieldError = "the type of identity field error";
		/// <summary>
		/// 数据类型{0}继承错误
		/// </summary>
		public const string TypeInheritError = "type {0} inherit error";
		/// <summary>
		/// 数据类型{0}继承错误,并非继承自{1}
		/// </summary>
		public const string TypeNotInheritFromSpecialType = "type {0} not inherit from special type {1}";
		/// <summary>
		/// 字段{0}非字符类型
		/// </summary>
		public const string DataFieldIsNotStringType = "data field {0} is not string type";
		/// <summary>
		/// 不支持日期格式{0}
		/// </summary>
		public const string UnsupportDateFormat = "unsupport date format {0}";
		/// <summary>
		/// 不支持谓词{0}的查询
		/// </summary>
		public const string UnSupportPredicate = "unsupport predicate {0}";
		/// <summary>
		/// 更新字段不存在
		/// </summary>
		public const string UpdateFieldIsNotExists = "update field is not exists";
		/// <summary>
		/// 更新字段类型出错,
		/// </summary>
		public const string UpdateFieldTypeIsError = "update field type is error";
		/// <summary>
		/// 数值"{0}"不存在与枚举集合{1}中
		/// </summary>
		public const string ValueNotInEnumType = "value {0} not in enum type {1}";
		/// <summary>
		/// 不在汇总字段中
		/// </summary>
		public const string NotExistsAggregateField = "not exists aggregate field";
		/// <summary>
		/// 该类型字段不支持{0}转换
		/// </summary>
		public const string TypeUnsupportTheTransform = "type {0} unsupport the transform";
		/// <summary>
		/// 未知的排序格式
		/// </summary>
		public const string UnknowOrderType = "unknow order type";
		/// <summary>
		/// 类型{0}并非数据库类型
		/// </summary>
		public const string TypeIsNotDatabase = "type {0} is not database";
		/// <summary>
		/// 数据库连接配置名"{0}"不存在
		/// </summary>
		public const string ConnectionSettingNameIsNotExists = "connection setting name \"{0}\" is not exists";
		/// <summary>
		/// 主键字段不存在
		/// </summary>
		public const string PrimaryKeyIsNotExist = "primary key is not exist";
		/// <summary>
		/// "select"字段数与"insert"字段数不相等.
		/// </summary>
		public const string SelectFiledsCountNotEquidInsertFiledCount = "select fileds count not equid insert filed count";

		public const string SelectFiledsNotInJoinTables = "select fileds not in join tables";
	}
}

