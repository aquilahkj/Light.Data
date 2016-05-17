using System;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Light.Data.Demo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			DataContext context = DataContext.Default;
			CommandOutput output = new CommandOutput ();
			output.OutputFullCommand = true;
			
			context.SetCommanfOutput (output);


//			MqDeviceInfo info = context.CreateNew<MqDeviceInfo> ();
//			info.Imei = "00";
//			info.Flag = "ww";
//			info.RegisterTime = DateTime.Now;
//			info.Status = 0;
//			info.Valid = 1;
//			info.Save ();
			List<TeRelateA_BE> relateList = context.LQuery<TeRelateA_BE> ().ToList ();
			Console.ReadLine ();
		}
	}
}
