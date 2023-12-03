using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("mInvenItemInfoList", "gold", "invenItemInfoList")]
	public class ES3UserType_MyInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_MyInfo() : base(typeof(ClassDef.MyInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.MyInfo)obj;
			
			writer.WritePrivateField("mInvenItemInfoList", instance);
			writer.WriteProperty("gold", instance.gold, ES3Type_long.Instance);
			writer.WriteProperty("invenItemInfoList", instance.invenItemInfoList, ES3Internal.ES3TypeMgr.GetOrCreateES3Type(typeof(System.Collections.Generic.List<ClassDef.InvenItemInfo>)));
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.MyInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "mInvenItemInfoList":
					instance = (ClassDef.MyInfo)reader.SetPrivateField("mInvenItemInfoList", reader.Read<System.Collections.Generic.List<ClassDef.InvenItemInfo>>(), instance);
					break;
					case "gold":
						instance.gold = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "invenItemInfoList":
						instance.invenItemInfoList = reader.Read<System.Collections.Generic.List<ClassDef.InvenItemInfo>>();
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ClassDef.MyInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_MyInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_MyInfoArray() : base(typeof(ClassDef.MyInfo[]), ES3UserType_MyInfo.Instance)
		{
			Instance = this;
		}
	}
}