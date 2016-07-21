using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Light.Data.Demo
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			Test ();

			//DataContext context = DataContext.Create ("sqlite");
			//CommandOutput output = new CommandOutput ();
			//output.OutputFullCommand = true;

			//context.SetCommanfOutput (output);


			//			MqDeviceInfo info = context.CreateNew<MqDeviceInfo> ();
			//			info.Imei = "00";
			//			info.Flag = "ww";
			//			info.RegisterTime = DateTime.Now;
			//			info.Status = 0;
			//			info.Valid = 1;
			//			info.Save ();
			//List<TeRelateA_BE> relateList = context.LQuery<TeRelateA_BE> ().Where (x => x.Id == 2).ToList ();
			Console.ReadLine ();
		}


		public static void Test ()
		{
			//ConstantExpression _consNum = System.Linq.Expressions.Expression.Constant (5, typeof (int));
			//UnaryExpression _unaryPlus = System.Linq.Expressions.Expression.Decrement (_consNum);
			//Expression<Func<int>> _unaryLam = System.Linq.Expressions.Expression.Lambda<Func<int>> (_unaryPlus);
			//Console.WriteLine ("表达式：  " + _unaryLam);
			//Console.WriteLine (_unaryLam.Compile () ());


			Expression<Func<TeUser, bool>> filter = n => !n.DeleteFlag;
			BinaryExpression lt = (BinaryExpression)filter.Body;
			BinaryExpression mult = (BinaryExpression)lt.Left;
			ParameterExpression en = (ParameterExpression)mult.Left;
			ConstantExpression three = (ConstantExpression)mult.Right;
			ConstantExpression five = (ConstantExpression)lt.Right;
			var One = filter.Compile ();
			//Console.WriteLine ("Result: {0},{1}", One (5), One (1));
			Console.WriteLine ("({0} ({1} {2} {3}) {4})", lt.NodeType,
					 mult.NodeType, en.Name, three.Value, five.Value);
		}
	}
}
