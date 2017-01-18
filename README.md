#LightData 使用文档

##实体类特性
```csharp
 [DataTable("Te_User")]
 public partial class TeUser
 {
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
        ....
 }
```
DataTable 指定对应数据表的表名

DataField 指定对应数据字段的字段名，其中：

* IsIdentity 字段是否自增
* IsPrimaryKey 字段是否主键
* IsNullable 字段是否可为空
* DefaultValue 默认值，可空类型字段在新增数据时，如果数据为空，自动使用该默认值；如数据类型是datetime，空值或最小值时可使用`DefaultTime.Now`表示默认值为当前时间、`DefaultTime.Today`表示默认值为当天

##Truncate Table
 ` 
 context.TruncateTable<TeUser> (); 
 ` 

 注意：该操作直接整表清空
 


##主键／自增字段查询数据
通过自增ID查数据

```csharp
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
```

通过主键查数据

```csharp
int id = 1;
TeUser user = context.SelectSingleFromKey<TeUser> (id);
```

##增加数据
自增ID会在Insert后自动赋值

```csharp
TeUser user = new TeUser ();
user.Account = "test";
user.Birthday = new DateTime (2001, 10, 20);
user.Email = "test@test.com";
user.Gender = GenderType.Female;
user.LevelId = 1;
user.NickName = "nicktest";
user.Password = "imtest";
user.RegTime = new DateTime (2015, 12, 30, 18, 0, 0);
user.Status = 1;
user.Telephone = "12345678";
user.HotRate = 1.0d;
context.Insert (user);
```


批量新增数据方法

```csharp
BatchInsert<T>(IEnumerable<T> datas, int batchCount = 10)
```
##更新数据
```csharp
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
user.Status = 2;
context.Update (user);
```
批量更新数据方法

```csharp
BatchUpdate<T>(IEnumerable<T> datas, int batchCount = 10)
```

##删除数据
```csharp
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
context.Delete (user);
```
批量删除数据方法

```csharp
BatchDelete<T>(IEnumerable<T> datas, int batchCount = 10)
```

<h2 id="transaction">事务处理</h2>
对增删改多个操作需要在同一事务中做操作,通过`DataContext`的`CreateTransDataContext`方法生成事务`TransDataContext`,并使用其事务方法进行事务操作

* BeginTrans 开始事务,每次事务开始前需执行
* CommitTrans 提交事务,在using作用域中如果只有一次提交,可不需执行
* RollbackTrans 回滚事务,在using作用域逻辑上需要回滚才执行,抛异常时会自动回滚


```csharp
//单次提交
using (TransDataContext trans = context.CreateTransDataContext ()) {
		trans.BeginTrans ();
		TeUser user1 = trans.SelectSingleFromId<TeUser> (3);
		user1.Account = "testmodify";
		trans.Update(user1);
		TeUser user2 = trans.SelectSingleFromId<TeUser> (4);
		trans.Delete(user2);
}
//多次提交
using (TransDataContext trans = context.CreateTransDataContext ()) {
		trans.BeginTrans ();
		TeUser user1 = trans.SelectSingleFromId<TeUser> (1);
		user1.Account = "testmodify";
		trans.Update(user1);
		trans.CommitTrans ();
		trans.BeginTrans ();
		TeUser user2 = trans.SelectSingleFromId<TeUser> (2);
		trans.Delete(user2);
		trans.CommitTrans ();
}
//回滚
using (TransDataContext trans = context.CreateTransDataContext ()) {
		trans.BeginTrans ();
		TeUser user1 = new <TeUser> ();
		user1.Account = "testmodify";
		trans.Insert (user1);
		if(user1.Id > 5){
			trans.RollbackTrans ();
		}
}
```

<h2 id="field_extend">字段扩展</h2>
查询或汇总数据时经常需要指定字段里的某部分数据进行提出查询或汇总,例如从时间字段提出日期统计,可以使用预设定的方法或属性进行提出汇总.另外数字型字段支持使用`+`(加)`-`(减)`*`(乘)`/`(除)`%`(求余)与其他数学字段或常量数值做相应数学处理,字符串支持使用`+`(加)做字符串拼接.

####时间字段方法(DateTime)

| 方法         	 					| 说明	|
|:-------------------------------	|:-----|
| ToString（format）				   	|格式化的日期字符串,年yyyy,月MM,日dd|

| 属性         	 					| 说明	|
|:-------------------------------	|:-----|
| Date									|日期时间格式|
| Year									|时间中的年部分|
| Month								|时间中的月部分|
| Day									|时间中的日部分|
| Hour									|时间中的时部分|
| Minute								|时间中的分部分|
| Second								|时间中的秒部分|
| Week									|时间中的周部分|
| DayOfWeek							|时间为当周第几天|
| DayOfYear							|时间为当年第几天|

####字符串字段方法(String)

| 方法         	 					| 说明	|
|:-------------------------------	|:-----|
| Substring（index,length）			|截取字符串|
| IndexOf（value,index）			|获取字符串位置|
| Replace（oldString,newString）	|替换字符串|
| ToLower（）							|转换大写|
| ToUpper（）							|转换小写|
| Trim（）								|清空前后空格|
| Concat（obj1,obj2,obj3...)		|静态函数,连接字符串|	

| 属性         	 					| 说明	|
|:-------------------------------	|:-----|
| Length								|字符串长度|

####数学方法(Math静态函数)

| 方法         	 	| 说明		|
|:----------------	|:-----	|
| Abs||
| Sign||
| Sin||
| Cos||
| Tan||
| Atan||
| ASin||
| ACos||
| Atan2||
| Ceiling||
| Floor||
| Round||
| Truncate||
| Log||
| Log10||
| Exp||
| Pow||
| Sqrt||
| Max||
| Min||

<h2 id="query">查询数据</h2>
使用context的Query方法生成IQuery对象,范型T为查询表的映射类
`IQuery query = context.Query<T>()`
`IQuery`通过Builder模式添加查询要素,最终可通过自身枚举方式或ToList()方式输出查询数据.

IQuery主要查询用方法:

| 方法         	 	| 说明		|
|:----------------	|:-----	|
| Where(Expression\<Func\<T, bool>> expression)| 把IQuery中查询条件置为当前查询条件,如Where(x=>x.Id>1)|
| WhereWithAnd(Expression\<Func\<T, bool>> expression)| 把IQuery中查询条件以And方式连接当前查询条件 |
| WhereWithOr(Expression\<Func\<T, bool>> expression)	| 把IQuery中查询条件以Or方式连接当前查询条件 |
| WhereReset ()	 	|把IQuery中查询条件重置|
| OrderBy\<TKey> (Expression\<Func\<T, TKey>> expression)	 	|把IQuery中排序替换为当前字段正序排序,如OrderBy(x=>x.Id)|
| OrderByDescending\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把IQuery中排序置为当前字段正序排序,如OrderByDescending(x=>x.Id)	|
| OrderByCatch\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把IQuery中排序连接当前字段正序排序|
| OrderByDescendingCatch\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把IQuery中排序连接当前字段倒序排序|
| OrderByReset()	 	| 把IQuery中排序重置|
| OrderByRandom()	 	| 把IQuery中排序置为随机排序|
| Take(int count)	 	| 设定IQuery输出结果的数量|
| Skip(int count)	 	| 设定IQuery输出结果需要跳过的数量|
| Range(int from, int to)	 	| 设定IQuery输出结果的从from位到to位|
| PageSize(int page, int size)	 	| 设定IQuery输出结果的分页结果,page:从1开始页数,size:每页数量|
| RangeReset()	 	| 把IQuery中输出结果范围重置|
| SetDistinct(bool distinct) | 设定是否使用Distinct方式输出结果|
| ToList()	| 结果以List<T>的方式输出	|
| ToArray()	| 结果以T[]的方式输出	|
| First()	| 输出的查询结果的首个数据结果对象,如无数据则为null	|
| ElementAt(int index)| 输出的查询结果的指定位数数据结果对象,如无数据则为null	|
| Exists	| 判断该IQuery是否有数据	|
| Count	| 返回该IQuery的数据长度,返回类型为int|
| LongCount		| 返回该IQuery的数据长度,返回类型为long|


####全查询

```csharp
List<TeUser> list = context.Query<TeUser> ().ToList ();
```
####组合查询

```csharp
List<TeUser> list = context.Query<TeUser> ().Where (x => x.Id > 1).OrderBy (x => x.Id).Take(10).ToList ();
```
###条件查询(Where)
***
使用`IQuery<T>.Where(lambda)`方法加入查询条件,查询参数为Lambda表达式,有Where,WhereWithAnd,WhereWithOr,WhereReset四个方法
```csharp
context.Query<T> ().Where(x => x.Id > 1)
```

####普通条件查询

```csharp
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.Id >= 5 && x.Id <= 10).ToList ();
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.Id < 5 || x.Id > 10).ToList ();
```

####In条件查询
使用`List<T>.Contains`方法,not查询在条件前面加"!"号

```csharp
int [] arrayx = new int [] { 3, 5, 7 };
List<int> listx = new List<int> (arrayx);
//in
List<TeUser> list1 = context.Query<TeUser> ().Where (x => listx.Contains (x.Id)).ToList ();
//not in
List<TeUser> list2 = context.Query<TeUser> ().Where (x => ！listx.Contains (x.Id)).ToList ();
```

####Like条件查询
只支持string类型,使用`string.StartsWith`、`string.EndsWith`、`string.Contains`方法查询,可支持反向查,not查询在条件前面加"!"号

```csharp
//后模糊
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.Account.StartsWith ("test")).ToList ();
//前模糊
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.Account.EndsWith ("1")).ToList ();
//前后模糊
List<TeUser> list3 = context.Query<TeUser> ().Where (x => x.Account.Contains ("es")).ToList ();
//反向查
List<TeUser> list1 = context.Query<TeUser> ().Where (x => "mytest2".EndsWith (x.Account)).ToList ();
//not 查
List<TeUser> list1 = context.Query<TeUser> ().Where (x => !x.Account.StartsWith ("test")).ToList ();
```

####null查询
查询字段需为可空类型(如int?)或string类型

```csharp
//null查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.RefereeId == null).ToList ();
//非null查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.RefereeId != null).ToList ();
```
如非可空类型可用扩展查询方式
`ExtendQuery.IsNull (x.Id)`

```csharp
//null查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.IsNull (x.Id)).ToList ();
//非null查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.IsNull (x.Id)).ToList ();
```

####布尔值字段查询
查询字段需为布尔(boolean)类型

```csharp
//是查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.DeleteFlag).ToList ();
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.DeleteFlag == true).ToList ();
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.DeleteFlag != false).ToList ();
//非查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !x.DeleteFlag).ToList ();
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.DeleteFlag != true).ToList ();
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.DeleteFlag == false).ToList ();
```
####跨表Exists查询
固定条件查询

```csharp
//Exist查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
//Not Exist查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
```
关联条件查询

```csharp
//Exist查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
//Not Exist查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
```

###排序(OrderBy)
***
使用`IQuery.OrderBy(lambda)`方法加入查询条件,查询参数为Lambda表达式,有OrderBy, OrderByDescending,OrderByCatch,OrderByDescendingCatch, OrderByReset,OrderByRandom六个方法

```csharp
context.Query<T> ().OrderBy(x => x.Id)
```
####正向排序

```csharp
List<TeUser> list = context.Query<TeUser> ().OrderBy (x => x.Id).ToList ();
```
####反向排序

```csharp
List<TeUser> list = context.Query<TeUser> ().OrderByDescending (x => x.Id).ToList ();
```

###选择指定字段(Select)
***
使用`IQuery<T>.Select(lambda)`查询时指定字段输出新的结构类,支持匿名类输出,使用Lambda表达式中的new方式定义新结构类.

```csharp
List<TeUserSimple> users = context.Query<TeUser> ()
				.Select (x => new TeUserSimple () {
					Id = x.Id,
					Account = x.Account,
					LevelId = x.LevelId,
					RegTime = x.RegTime})
				.ToList ();
//匿名类
var users2 = context.Query<TeUser> ()
				.Select (x => new {
					x.Id,
					x.Account,
					x.LevelId,
					x.RegTime})
				.ToList ();
```

使用`IQuery<T>.Select(lambda)`查询时指定字段输出新的结构类,支持匿名类输出,使用Lambda表达式中的new方式定义新结构类.

```csharp
List<TeUserSimple> users = context.Query<TeUser> ()
				.Select (x => new TeUserSimple () {
					Id = x.Id,
					Account = x.Account,
					LevelId = x.LevelId,
					RegTime = x.RegTime})
				.ToList ();
```
查询单字段列表

```csharp
List<int> list = context.Query<TeUser> ().SelectField (x => x.Id).ToList ();
```


###查询批量更新
***
使用`IQuery<T>.Update(lambda)`对查询数据进行批量更新操作,以lambda表达式中的new方式定义数据的更新字段与更新内容,左侧为更新字段名,右侧为更新内容,内容可为原字段.

```csharp
context.Query<TeUser> ()
		.Update (x => new TeUser {
				LastLoginTime = DateTime.Now,
				Status = 2
		});
//更新内容为原字段
context.Query<TeUser2> ()
		.Update (x => new TeUser2 {
				LastLoginTime = x.RecordTime,
				Status = x.Status + 1
			});
```
###查询批量删除
***
使用`IQuery<T>.Delete()`对查询数据进行批量删除操作

```csharp
context.Query<TeUser> ().Where(x => x.Id > 1).Delete();
```

###查询批量插入
***
对查询数据进行全字段或指定字段插入指定的数据表,直接通过数据库的`insert into t1(x1,x2,x3...)select y1,y2,y3 from t2`方式直接插入数据
####全字段插入
使用`IQuery<T>.Insert<K>()`全数据插入,查询表T的字段必须与插入表K的字段一一对应,如果T与K有同位字段是自增字段,则插入时,K的自增字段数据为自增.

```csharp
context.Query<TeDataLog> ().Where (x => x.Id <= 20).Insert<TeDataLogHistory> ();
```

####指定字段插入
使用`IQuery<T>.SelectInsert<K>(lambda)`选择指定字段举行插入,lambda表达式中的new方式定义数据的插入表字段与查询表选择字段,左侧为插入表字段,查询表选择字段,字段可以为常量.

```csharp
context.Query<TeDataLog> ().SelectInsert (x => new TeDataLogHistory () {
				Id = x.Id,
				UserId = x.UserId,
				ArticleId = x.ArticleId,
				RecordTime = x.RecordTime,
				Status = x.Status,
				Action = x.Action,
				RequestUrl = x.RequestUrl,
				CheckId = 3,
});
```

<h2 id="aggregate">汇总统计数据</h2>
使用`IQuery<T>.GroupBy<K>(lambda)`进行汇总统计数据,lambda表达式中的new方式定义数据的统计字段与汇总函数,输出类型K可以为匿名类. GroupBy函数返回`IAggregate<K>`接口,用于后续处理.

主要汇总方法

| 方法         	 	| 说明		|
|:----------------	|:-----	|
| Having(Expression\<Func\<K, bool>> expression)| 把IAggreate中过滤条件置为当前过滤条件,如Having(x=>x.Count>1)|
| HavingWithAnd(Expression\<Func\<K, bool>> expression)| 把IAggreate中过滤条件以And方式连接当前过滤条件 |
| HavingWithOr(Expression\<Func\<K, bool>> expression)	| 把IAggreate中查询条件以Or方式连接当前过滤条件 |
| HavingReset ()	 	|把IAggreate中过滤条件重置|
| OrderBy\<KKey> (Expression\<Func\<K, TKey>> expression)	 	|把IAggreate中排序替换为当前字段正序排序,如OrderBy(x=>x.Id)|
| OrderByDescending\<KKey> (Expression\<Func\<K, TKey>> expression)	 	| 把IAggreate中排序置为当前字段正序排序,如OrderByDescending(x=>x.Id)	|
| OrderByCatch\<KKey> (Expression\<Func\<K, TKey>> expression)	 	| 把IAggreate中排序连接当前字段正序排序|
| OrderByDescendingCatch\<KKey> (Expression\<Func\<K, TKey>> expression)	 	| 把IAggreate中排序连接当前字段倒序排序|
| OrderByReset()	 	| 把IAggreate中排序重置|
| OrderByRandom()	 	| 把IAggreate中排序置为随机排序|
| Take(int count)	 	| 设定IAggreate输出结果的数量|
| Skip(int count)	 	| 设定IAggreate输出结果需要跳过的数量|
| Range(int from, int to)	 	| 设定IAggreate输出结果的从from位到to位|
| PageSize(int page, int size)	 	| 设定IAggreate输出结果的分页结果,page:从1开始页数,size:每页数量|
| RangeReset()	 	| 把IAggreate中输出结果范围重置|
| ToList()	| 结果以List<K>的方式输出	|
| ToArray()	| 结果以K[]的方式输出	|
| First()	| 输出的汇总结果的首个数据结果对象,如无数据则为null	|

###汇总函数

汇总函数由`Function`类的静态函数实现

| 函数         	 					| 说明	|
|:-------------------------------	|:-----|
| Count（）							|数据行计数汇总,返回int类型结果|
| LongCount（）						|数据行计数汇总,返回long类型结果|
| CountCondition（condition）		|条件判断该数据行计数汇总,返回int类型结果|
| LongCountCondition（condition）	|条件判断该数据行计数汇总,返回long类型结果|
| Count (field) 						|指定字段计数汇总,返回int类型结果|
| LongCount (field)					|指定字段计数汇总,返回long类型结果|
| DistinctCount (field)				|指定字段去重复后计数汇总,返回int类型结果|
| DistinctLongCount	(field)		|指定字段去重复后计数汇总,返回long类型结果|
| Sum (field)							|指定字段数值累加汇总,返回汇总字段类型结果|
| LongSum	(field)					|指定字段数值累加汇总,返回long类型结果|
| DistinctSum	 (field)				|指定字段去重复后累加汇总,汇总字段类型结果|
| DistinctLongSum (field)			|指定字段去重复后累加汇总, long类型结果|
| Avg (field)							|指定字段数值平均值汇总,返回double类型结果|
| DistinctAvg	 (field)				|指定字段去重复后数值平均值汇总,返回double类型结果|
| Max (field)							|指定字段的最大值|
| Min (field)							|指定字段的最小最|


####数据行计数汇总

```csharp
//普通汇总
List<LevelIdAgg> list = context.Query<TeUser> ()
					.Where (x => x.Id >= 5)
					.GroupBy (x => new LevelIdAgg () {
								LevelId = x.LevelId,
								Data = Function.Count ()
					}).ToList ();
//使用匿名类汇总
var list = context.Query<TeUser> ()
					.Where (x => x.Id >= 5)
					.GroupBy (x => new {
								LevelId = x.LevelId,
								Data = Function.Count ()
					}).ToList ();
//条件判断汇总
var list = context.Query<TeUser> ()
					.Where (x => x.Id >= 5)
					.GroupBy (x => new {
								LevelId = x.LevelId,
								Valid = Function.CountCondition (x.Status = 1),
								Invalid = Function.CountCondition (x.Status != 1)
					}).ToList ();

```
####指定字段计数汇总

```csharp
//统计指定字段
List<LevelIdAgg> list = context.Query<TeUser> ()
					 .GroupBy (x => new LevelIdAgg_T () {
							LevelId = x.LevelId,
							Data = Function.Count (x.Area)
					}).ToList ();
//条件判断统计指定字段
var list = context.Query<TeUser> ()
					.Where (x => x.Id >= 5)
					.GroupBy (x => new {
								LevelId = x.LevelId,
								Valid = Function.Count (x.Status = 1 ? x.Area : null),
								Invalid = Function.Count (x.Status != 1 ? x.Area : null)
					}).ToList ();

```

###汇总数据过滤(Having)
使用`IAggreate<K>.Having(lambda)`方法加入汇总条件,对汇总数据做二次过滤,查询参数为Lambda表达式,有Having,HavingWithAnd,HavingWithOr,HavingReset四个方法

```csharp
List<LevelIdAgg> list = context.Query<TeUser> ()
			.GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Sum (x.LoginTimes)
			})
			.Having (y => y.Data > 15)
			.ToList ();
```
###汇总数据排序(OrderBy)
使用`IAggreate<K>.OrderBy(lambda)`方法加入汇总条件,查询参数为Lambda表达式,有OrderBy, OrderByDescending,OrderByCatch,OrderByDescendingCatch, OrderByReset,OrderByRandom六个方法

```csharp
//汇总字段排序
List<LevelIdAgg> list = context.Query<TeUser> ()
			.GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			})
			.OrderBy (x => x.LevelId)
			.ToList ();
//汇总结果排序
List<LevelIdAgg> list = context.Query<TeUser> ()
			.GroupBy (x => new LevelIdAgg () {
				LevelId = x.LevelId,
				Data = Function.Count ()
			})
			.OrderBy (x => x.Data)
			.ToList ();
```

###汇总字段扩展
汇总数据时经常需要指定字段里的某部分数据进行提出汇总,例如从时间字段提出日期统计,可以使用字段扩展方式进行提出汇总.详见
[字段扩展](#field_extend)

####日期类统计

```csharp
//Date统计
List<RegDateAgg> list = context.Query<TeUser> ()
				.GroupBy (x => new RegDateAgg () {
					RegDate = x.RegTime.Date,
					Data = Function.Count ()
				}).ToList ();
//日期格式化统计
List<RegDateFormatAgg> list = context.Query<TeUser> ()
				.GroupBy (x => new RegDateFormatAgg () {
					RegDateFormat = x.RegTime.ToString("yyyy-MM-dd"),
					Data = Function.Count ()
				}).ToList ();	
//年统计
List<NumDataAgg> list = context.Query<TeUser> ()
				.GroupBy (x => new NumDataAgg () {
					Name = x.RegTime.Year,
					Data = Function.Count ()
				}).ToList ();	
```
####字符串类统计

```csharp
//截取字符串统计
List<StringDataAgg> list = context.Query<TeUser> ().
				GroupBy (x => new StringDataAgg () {
					Name = x.Account.Substring (0, 5),
					Data = Function.Count ()
				}).ToList ();
//字符串长度统计
List<NumDataAgg> list = context.Query<TeUser> ().
				GroupBy (x => new NumDataAgg () {
					Name = x.Account.Length,
					Data = Function.Count ()
				}).ToList ();
```