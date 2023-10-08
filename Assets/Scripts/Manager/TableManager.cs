using System;
using System.Collections.Generic;
using EasyExcel;
using SheetData;
using UnityEngine;

public partial class TableManager : MonoBehaviour
{
	static private TableManager mInstance = null;

	private EEDataManager _ee = new EEDataManager();
	
	
	private List<GameDataTable_Client> mGameDataList;
	public List<GameDataTable_Client> gameDataList { get { return mGameDataList; } }
	
	private List<BaseInterestRateTable_Client> mBaseInterestRateList;
	public List<BaseInterestRateTable_Client> baseInterestRateList { get { return mBaseInterestRateList; } }

	private List<ItemDataTable_Client> mItemDataTableList;
	public List<ItemDataTable_Client> itemDataTableList { get { return mItemDataTableList; } }

	static public TableManager Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = (TableManager) FindObjectOfType(typeof(TableManager));
				if (mInstance == null)
					return null;
				mInstance.Init();
			}

			if (mInstance == null)
				Debug.LogError("TableManager가 Hierarchy에 존재하지 않습니다.");

			return mInstance;
		}
	}

	public void Init()
	{		
		mGameDataList = _ee.GetListJson<GameDataTable_Client>();
		mBaseInterestRateList = _ee.GetListJson<BaseInterestRateTable_Client>();
		mItemDataTableList = _ee.GetListJson<ItemDataTable_Client>();
	}    

    public GameDataTable_Client FindGameDataTable(long _uid)
    {
	    GameDataTable_Client data =mGameDataList.Find(d => d.UID == _uid);
	    if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
	    return default;
    }

	public BaseInterestRateTable_Client FindBaseInterestRateTable(long _uid)
	{
		BaseInterestRateTable_Client data = mBaseInterestRateList.Find(d => d.UID == _uid);
		if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
		return default;
	}

	public ItemDataTable_Client FindItemDataTable_Client(long _uid)
	{
		ItemDataTable_Client data = mItemDataTableList.Find(d => d.UID == _uid);
		if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
		return default;
	}

	public float GetBaseInterestRate(DateTime _dt)
	{
		BaseInterestRateTable_Client data = mBaseInterestRateList.Find(d => d.Year == _dt.Year);
		if (data != default) {
			switch(_dt.Month) {
				case 1: return data.month1;
				case 2: return data.month2;
				case 3: return data.month3;
				case 4: return data.month4;
				case 5: return data.month5;
				case 6: return data.month6;
				case 7: return data.month7;
				case 8: return data.month8;
				case 9: return data.month9;
				case 10: return data.month10;
				case 11: return data.month11;
				case 12: return data.month12;
			}			
		}
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
		return 0f;
	}
}

