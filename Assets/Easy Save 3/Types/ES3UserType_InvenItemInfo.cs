using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("uid", "count", "price")]
	public class ES3UserType_InvenItemInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3UserType_InvenItemInfo() : base(typeof(ClassDef.InvenItemInfo)){ Instance = this; priority = 1; }


		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (ClassDef.InvenItemInfo)obj;
			
			writer.WriteProperty("uid", instance.uid, ES3Type_long.Instance);
			writer.WriteProperty("count", instance.count, ES3Type_int.Instance);
			writer.WriteProperty("price", instance.avrPrice, ES3Type_long.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (ClassDef.InvenItemInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "uid":
						instance.uid = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					case "count":
						instance.count = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					case "price":
						instance.avrPrice = reader.Read<System.Int64>(ES3Type_long.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new ClassDef.InvenItemInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}


	public class ES3UserType_InvenItemInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_InvenItemInfoArray() : base(typeof(ClassDef.InvenItemInfo[]), ES3UserType_InvenItemInfo.Instance)
		{
			Instance = this;
		}
	}
}