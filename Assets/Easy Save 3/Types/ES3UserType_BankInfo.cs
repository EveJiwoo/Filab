using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("cdList")]
	public class ES3UserType_BankInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BankInfo() : base(typeof(ClassDef.BankInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.BankInfo)obj;
			
			writer.WritePrivateField("cdList", instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.BankInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "cdList":
					instance = (ClassDef.BankInfo)reader.SetPrivateField("cdList", reader.Read<System.Collections.Generic.List<ClassDef.CDProductInfo>>(), instance);
					break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ClassDef.BankInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_BankInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_BankInfoArray() : base(typeof(ClassDef.BankInfo[]), ES3UserType_BankInfo.Instance)
		{
			Instance = this;
		}
	}
}