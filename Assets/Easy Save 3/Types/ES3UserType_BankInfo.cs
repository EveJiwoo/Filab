using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("cdList", "loan")]
	public class ES3UserType_BankInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_BankInfo() : base(typeof(ClassDef.BankInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.BankInfo)obj;
			
			writer.WriteProperty("cdList", instance.cdList, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<ClassDef.CDProductInfo>)));
			writer.WriteProperty("loan", instance.loan, ES3UserType_LoanCondition.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.BankInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "cdList":
						instance.cdList = reader.Read<System.Collections.Generic.List<ClassDef.CDProductInfo>>();
						break;
					case "loan":
						instance.loan = reader.Read<ClassDef.LoanCondition>(ES3UserType_LoanCondition.Instance);
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