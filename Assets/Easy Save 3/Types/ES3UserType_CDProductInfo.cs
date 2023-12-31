using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("depositeGold", "interestRate", "term", "maturityDate")]
	public class ES3UserType_CDProductInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_CDProductInfo() : base(typeof(ClassDef.CDProductInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.CDProductInfo)obj;
			
			writer.WriteProperty("depositeGold", instance.depositeGold, ES3Type_long.Instance);
			writer.WriteProperty("interestRate", instance.interestRate, ES3Type_float.Instance);
			writer.WriteProperty("term", instance.term, ES3Type_int.Instance);
			writer.WriteProperty("maturityDate", instance.maturityDate, ES3Type_DateTime.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.CDProductInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "depositeGold":
						instance.depositeGold = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "interestRate":
						instance.interestRate = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "term":
						instance.term = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "maturityDate":
						instance.maturityDate = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ClassDef.CDProductInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_CDProductInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_CDProductInfoArray() : base(typeof(ClassDef.CDProductInfo[]), ES3UserType_CDProductInfo.Instance)
		{
			Instance = this;
		}
	}
}