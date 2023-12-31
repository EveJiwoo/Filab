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
	public class OccupationDataTable_Client : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private long _UID;
		public long UID { get { return _UID; } set{_UID=value; } }

		[SerializeField]
		private string _OccupationType;
		public string OccupationType { get { return _OccupationType; } set{_OccupationType=value; } }

		[SerializeField]
		private float _CreditScore;
		public float CreditScore { get { return _CreditScore; } set{_CreditScore=value; } }

		[SerializeField]
		private int _MaxMonthlyCreditIncrease;
		public int MaxMonthlyCreditIncrease { get { return _MaxMonthlyCreditIncrease; } set{_MaxMonthlyCreditIncrease=value; } }

		[SerializeField]
		private long _LoanLimit;
		public long LoanLimit { get { return _LoanLimit; } set{_LoanLimit=value; } }

		[SerializeField]
		private float _ExtraInterestMin;
		public float ExtraInterestMin { get { return _ExtraInterestMin; } set{_ExtraInterestMin=value; } }

		[SerializeField]
		private float _ExtraInterestMax;
		public float ExtraInterestMax { get { return _ExtraInterestMax; } set{_ExtraInterestMax=value; } }

		[SerializeField]
		private int _MaxDuration;
		public int MaxDuration { get { return _MaxDuration; } set{_MaxDuration=value; } }

		[SerializeField]
		private float _CreditIncreaseVariable;
		public float CreditIncreaseVariable { get { return _CreditIncreaseVariable; } set{_CreditIncreaseVariable=value; } }


		public OccupationDataTable_Client()
		{
		}

#if UNITY_EDITOR
		public OccupationDataTable_Client(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _UID);
			TryParse(sheet[row][column++], out _OccupationType);
			TryParse(sheet[row][column++], out _CreditScore);
			TryParse(sheet[row][column++], out _MaxMonthlyCreditIncrease);
			TryParse(sheet[row][column++], out _LoanLimit);
			TryParse(sheet[row][column++], out _ExtraInterestMin);
			TryParse(sheet[row][column++], out _ExtraInterestMax);
			TryParse(sheet[row][column++], out _MaxDuration);
			TryParse(sheet[row][column++], out _CreditIncreaseVariable);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class OccupationDataTable_OccupationDataTable_Client : EERowDataCollection
	{
		
		public List<OccupationDataTable_Client> elements = new List<OccupationDataTable_Client>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as OccupationDataTable_Client);
		}

		public override int GetDataCount()
		{
			return elements.Count;
		}

	
public List<OccupationDataTable_Client> OnGetAllData()
		{		
return elements;
		}		public override EERowData GetData(int index)
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
