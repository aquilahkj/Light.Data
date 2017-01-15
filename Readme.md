#LightData 使用文档

##实体类特性
```cs
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
* DefaultValue 默认值，可空类型字段在新增数据时，如果数据为空，自动使用该默认值；如数据类型是datetime，空值或最小值时可使用DefaultTime.Now表示默认值为当前时间、DefaultTime.Today表示默认值为当天

##Truncate Table
 ` 
 context.TruncateTable<TeUser> (); 
 ` 

 注意：该操作直接整表清空
 


##主键／自增字段查询数据
通过自增ID查数据

```cs
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
```

通过主键查数据

```cs
int id = 1;
TeUser user = context.SelectSingleFromKey<TeUser> (id);
```

##增加数据
自增ID会在Insert后自动赋值

```cs
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

```cs
BatchInsert<T>(IEnumerable<T> datas, int batchCount = 10)
```
##更新数据
```cs
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
user.Status = 2;
context.Update (user);
```
批量更新数据方法

```cs
BatchUpdate<T>(IEnumerable<T> datas, int batchCount = 10)
```

##删除数据
```cs
int id = 1;
TeUser user = context.SelectSingleFromId<TeUser> (id);
context.Delete (user);
```
批量删除数据方法

```cs
BatchDelete<T>(IEnumerable<T> datas, int batchCount = 10)
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
| WhereWithOr(Expression\<Func\<T, bool>> expression)	| 把Query中查询条件以And方式连接当前查询条件 |
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
| RangeReset	 	| 把Query中输出结果范围重置|
| SetDistinct(bool distinct) | 设定是否使用Distinct方式输出结果|
| ToList()	| 结果以List<T>的方式输出	|
| ToArray()	| 结果以T[]的方式输出	|
| First()	| 输出的查询结果的首个数据结果对象,如无数据则为null	|
| ElementAt(int index)| 输出的查询结果的指定位数数据结果对象,如无数据则为null	|
| Exists	| 判断该Query是否有数据	|
| Count	| 返回该Query的数据长度,返回类型为int|
| LongCount		| 返回该Query的数据长度,返回类型为long|


####全查询

```cs
List<TeUser> list = context.Query<TeUser> ().ToList ();
```
####组合查询

```cs
List<TeUser> list = context.Query<TeUser> ().Where (x => x.Id > 1).OrderBy (x => x.Id).Take(10).ToList ();
```
###条件查询(Where)
***
使用Where方法加入查询条件，查询参数为Lambda表达式
```cs
context.Query<T> ().Where(LambdaExpression)
```

####普通条件查询

```cs
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.Id >= 5 && x.Id <= 10).ToList ();
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.Id < 5 || x.Id > 10).ToList ();
```

####In条件查询
使用List\<T>的Contains方法,not查询在条件前面加"!"号

```cs
int [] arrayx = new int [] { 3, 5, 7 };
List<int> listx = new List<int> (arrayx);
//in
List<TeUser> list1 = context.Query<TeUser> ().Where (x => listx.Contains (x.Id)).ToList ();
//not in
List<TeUser> list2 = context.Query<TeUser> ().Where (x => ！listx.Contains (x.Id)).ToList ();
```

####Like条件查询
只支持string类型,使用string类的StartsWith、EndsWith、Contains方法查询,可支持反向查,not查询在条件前面加"!"号

```cs
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

```cs
//null查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => x.RefereeId == null).ToList ();
//非null查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => x.RefereeId != null).ToList ();
```
如非可空类型可用扩展查询方式
`ExtendQuery.IsNull (x.Id)`

```cs
//null查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.IsNull (x.Id)).ToList ();
//非null查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.IsNull (x.Id)).ToList ();
```

####布尔值字段查询
查询字段需为布尔(boolean)类型

```cs
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

```cs
//Exist查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
//Not Exist查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Status == 1)).ToList ();
```
关联条件查询

```cs
//Exist查询
List<TeUser> list1 = context.Query<TeUser> ().Where (x => ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
//Not Exist查询
List<TeUser> list2 = context.Query<TeUser> ().Where (x => !ExtendQuery.Exists<TeUserLevel> (y => y.Id == x.LevelId)).ToList ();
```

###排序(OrderBy)
***
####正向排序

```cs
List<TeUser> list = context.Query<TeUser> ().OrderBy (x => x.Id).ToList ();
```
####反向排序

```cs
List<TeUser> list = context.Query<TeUser> ().OrderByDescending (x => x.Id).ToList ();
```