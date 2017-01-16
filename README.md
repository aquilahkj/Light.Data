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

##事务处理
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

##查询数据
使用context的Query方法生成IQuery对象,范型T为查询表的映射类
`IQuery query = context.Query<T>()`
`IQuery`通过Builder模式添加查询要素,最终可通过自身枚举方式或ToList()方式输出查询数据.

IQuery主要查询用方法:

| 方法         	 	| 说明		|
|:----------------	|:-----	|
| Where(Expression\<Func\<T, bool>> expression)| 把Query中查询条件置为当前查询条件,如Where(x=>x.Id>1)|
| WhereWithAnd(Expression\<Func\<T, bool>> expression)| 把Query中查询条件以And方式连接当前查询条件 |
| WhereWithOr(Expression\<Func\<T, bool>> expression)	| 把Query中查询条件以Or方式连接当前查询条件 |
| WhereReset ()	 	|把Query中查询条件重置|
| OrderBy\<TKey> (Expression\<Func\<T, TKey>> expression)	 	|把Query中排序替换为当前字段正序排序,如OrderBy(x=>x.Id)|
| OrderByDescending\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把Query中排序置为当前字段正序排序,如OrderByDescending(x=>x.Id)	|
| OrderByCatch\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把Query中排序连接当前字段正序排序|
| OrderByDescendingCatch\<TKey> (Expression\<Func\<T, TKey>> expression)	 	| 把Query中排序连接当前字段倒序排序|
| OrderByReset()	 	| 把Query中排序重置|
| OrderByRandom()	 	| 把Query中排序置为随机排序|
| Take(int count)	 	| 设定Query输出结果的数量|
| Skip(int count)	 	| 设定Query输出结果需要跳过的数量|
| Range(int from, int to)	 	| 设定Query输出结果的从from位到to位|
| PageSize(int page, int size)	 	| 设定Query输出结果的分页结果,page:从1开始页数,size:每页数量|
| RangeReset()	 	| 把Query中输出结果范围重置|
| SetDistinct(bool distinct) | 设定是否使用Distinct方式输出结果|
| ToList()	| 结果以List<T>的方式输出	|
| ToArray()	| 结果以T[]的方式输出	|
| First()	| 输出的查询结果的首个数据结果对象,如无数据则为null	|
| ElementAt(int index)| 输出的查询结果的指定位数数据结果对象,如无数据则为null	|
| Exists	| 判断该Query是否有数据	|
| Count	| 返回该Query的数据长度,返回类型为int|
| LongCount		| 返回该Query的数据长度,返回类型为long|


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