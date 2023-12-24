using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("city", "loanGold", "interestPayGold", "interestRate", "curInterestGold", "term", "maturityDate", "contractDate", "nextPaymentDate", "payCount")]
	public class ES3UserType_LoanCondition : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_LoanCondition() : base(typeof(ClassDef.LoanCondition)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.LoanCondition)obj;
			
			writer.WriteProperty("city", instance.city, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(EnumDef.CityType)));
			writer.WriteProperty("loanGold", instance.loanGold, ES3Type_long.Instance);
			writer.WriteProperty("interestPayGold", instance.interestPayGold, ES3Type_long.Instance);
			writer.WriteProperty("interestRate", instance.interestRate, ES3Type_float.Instance);
			writer.WriteProperty("curInterestGold", instance.curInterestGold, ES3Type_long.Instance);
			writer.WriteProperty("term", instance.term, ES3Type_int.Instance);
			writer.WriteProperty("maturityDate", instance.maturityDate, ES3Type_DateTime.Instance);
			writer.WriteProperty("contractDate", instance.contractDate, ES3Type_DateTime.Instance);
			writer.WriteProperty("nextPaymentDate", instance.nextPaymentDate, ES3Type_DateTime.Instance);
			writer.WriteProperty("payCount", instance.interestayCount, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.LoanCondition)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "city":
						instance.city = reader.Read<EnumDef.CityType>(ES3Type_enum.Instance);
						break;
					case "loanGold":
						instance.loanGold = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "interestPayGold":
						instance.interestPayGold = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "interestRate":
						instance.interestRate = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "curInterestGold":
						instance.curInterestGold = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "term":
						instance.term = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "maturityDate":
						instance.maturityDate = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					case "contractDate":
						instance.contractDate = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					case "nextPaymentDate":
						instance.nextPaymentDate = reader.Read<System.DateTime>(ES3Type_DateTime.Instance);
						break;
					case "payCount":
						instance.interestayCount = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ClassDef.LoanCondition();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_LoanConditionArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_LoanConditionArray() : base(typeof(ClassDef.LoanCondition[]), ES3UserType_LoanCondition.Instance)
		{
			Instance = this;
		}
	}
}