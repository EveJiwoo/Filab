﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EasyExcel.
//     Runtime Version: 4.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using EasyExcel;

namespace SheetData
{
	[Serializable]
	public class ItemDataTable_Server : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private long _UID;
		public long UID { get { return _UID; } set{_UID=value; } }

		[SerializeField]
		private string _Name;
		public string Name { get { return _Name; } set{_Name=value; } }

		[SerializeField]
		private int _OrignalBuyPrice;
		public int OrignalBuyPrice { get { return _OrignalBuyPrice; } set{_OrignalBuyPrice=value; } }

		[SerializeField]
		private int _OrignalSellPrice;
		public int OrignalSellPrice { get { return _OrignalSellPrice; } set{_OrignalSellPrice=value; } }

		[SerializeField]
		private bool _City1Sell;
		public bool City1Sell { get { return _City1Sell; } set{_City1Sell=value; } }

		[SerializeField]
		private bool _City2Sell;
		public bool City2Sell { get { return _City2Sell; } set{_City2Sell=value; } }

		[SerializeField]
		private bool _City3Sell;
		public bool City3Sell { get { return _City3Sell; } set{_City3Sell=value; } }

		[SerializeField]
		private bool _City4Sell;
		public bool City4Sell { get { return _City4Sell; } set{_City4Sell=value; } }

		[SerializeField]
		private bool _City5Sell;
		public bool City5Sell { get { return _City5Sell; } set{_City5Sell=value; } }

		[SerializeField]
		private bool _City6Sell;
		public bool City6Sell { get { return _City6Sell; } set{_City6Sell=value; } }

		[SerializeField]
		private bool _City7Sell;
		public bool City7Sell { get { return _City7Sell; } set{_City7Sell=value; } }

		[SerializeField]
		private bool _City8Sell;
		public bool City8Sell { get { return _City8Sell; } set{_City8Sell=value; } }

		[SerializeField]
		private bool _City9Sell;
		public bool City9Sell { get { return _City9Sell; } set{_City9Sell=value; } }

		[SerializeField]
		private bool _City10Sell;
		public bool City10Sell { get { return _City10Sell; } set{_City10Sell=value; } }

		[SerializeField]
		private bool _City11Sell;
		public bool City11Sell { get { return _City11Sell; } set{_City11Sell=value; } }

		[SerializeField]
		private bool _City12Sell;
		public bool City12Sell { get { return _City12Sell; } set{_City12Sell=value; } }

		[SerializeField]
		private bool _City13Sell;
		public bool City13Sell { get { return _City13Sell; } set{_City13Sell=value; } }

		[SerializeField]
		private bool _City14Sell;
		public bool City14Sell { get { return _City14Sell; } set{_City14Sell=value; } }

		[SerializeField]
		private bool _City15Sell;
		public bool City15Sell { get { return _City15Sell; } set{_City15Sell=value; } }

		[SerializeField]
		private bool _City16Sell;
		public bool City16Sell { get { return _City16Sell; } set{_City16Sell=value; } }

		[SerializeField]
		private bool _City17Sell;
		public bool City17Sell { get { return _City17Sell; } set{_City17Sell=value; } }

		[SerializeField]
		private bool _City18Sell;
		public bool City18Sell { get { return _City18Sell; } set{_City18Sell=value; } }

		[SerializeField]
		private bool _City19Sell;
		public bool City19Sell { get { return _City19Sell; } set{_City19Sell=value; } }

		[SerializeField]
		private bool _City20Sell;
		public bool City20Sell { get { return _City20Sell; } set{_City20Sell=value; } }

		[SerializeField]
		private string _AtlasName;
		public string AtlasName { get { return _AtlasName; } set{_AtlasName=value; } }

		[SerializeField]
		private string _SpriteName;
		public string SpriteName { get { return _SpriteName; } set{_SpriteName=value; } }

		[SerializeField]
		private string _Description;
		public string Description { get { return _Description; } set{_Description=value; } }


		public ItemDataTable_Server()
		{
		}

#if UNITY_EDITOR
		public ItemDataTable_Server(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _UID);
			TryParse(sheet[row][column++], out _Name);
			TryParse(sheet[row][column++], out _OrignalBuyPrice);
			TryParse(sheet[row][column++], out _OrignalSellPrice);
			TryParse(sheet[row][column++], out _City1Sell);
			TryParse(sheet[row][column++], out _City2Sell);
			TryParse(sheet[row][column++], out _City3Sell);
			TryParse(sheet[row][column++], out _City4Sell);
			TryParse(sheet[row][column++], out _City5Sell);
			TryParse(sheet[row][column++], out _City6Sell);
			TryParse(sheet[row][column++], out _City7Sell);
			TryParse(sheet[row][column++], out _City8Sell);
			TryParse(sheet[row][column++], out _City9Sell);
			TryParse(sheet[row][column++], out _City10Sell);
			TryParse(sheet[row][column++], out _City11Sell);
			TryParse(sheet[row][column++], out _City12Sell);
			TryParse(sheet[row][column++], out _City13Sell);
			TryParse(sheet[row][column++], out _City14Sell);
			TryParse(sheet[row][column++], out _City15Sell);
			TryParse(sheet[row][column++], out _City16Sell);
			TryParse(sheet[row][column++], out _City17Sell);
			TryParse(sheet[row][column++], out _City18Sell);
			TryParse(sheet[row][column++], out _City19Sell);
			TryParse(sheet[row][column++], out _City20Sell);
			TryParse(sheet[row][column++], out _AtlasName);
			TryParse(sheet[row][column++], out _SpriteName);
			TryParse(sheet[row][column++], out _Description);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class ItemDataTable_ItemDataTable_Server : EERowDataCollection
	{
		
		public List<ItemDataTable_Server> elements = new List<ItemDataTable_Server>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as ItemDataTable_Server);
		}

		public override int GetDataCount()
		{
			return elements.Count;
		}

		public override EERowData GetData(int index)
		{
			return elements[index];
		}

		public override void OnAfterSerialized()
		{
			foreach (var element in elements)
				element.OnAfterSerialized();
		}
	}
}
