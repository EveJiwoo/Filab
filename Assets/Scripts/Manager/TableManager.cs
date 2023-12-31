using System;
using System.Collections.Generic;
using EasyExcel;
using SheetData;
using UnityEngine;

public partial class TableManager : MonoBehaviour
{
	static private TableManager mInstance = null;

	private EEDataManager _ee = new EEDataManager();

    /// <summary> 게임 설정 </summary>
    private List<GameDataTable_Client> mGameDataList;
	public List<GameDataTable_Client> gameDataList { get { return mGameDataList; } }

	/// <summary> 기준 금리 </summary>
	private List<InterestRateBaseTable_Client> mInterestRateBaseList;
	public List<InterestRateBaseTable_Client> interestRateBaseList { get { return mInterestRateBaseList; } }	

	/// <summary> 아이템 판매 구매 정보 </summary>
	private List<ItemDataTable_Client> mItemDataTableList;
	public List<ItemDataTable_Client> itemDataTableList { get { return mItemDataTableList; } }
	
    /// <summary> 아이템 물가 변동 정보 </summary>
	private List<ItemPriceRateTable_Client> mItemPriceRateTableList;
	public List<ItemPriceRateTable_Client> itemPriceRateTableList { get { return mItemPriceRateTableList; } }

    /// <summary> 신용 점수 정보 </summary>
    private List<OccupationDataTable_Client> mOccupationDataTableList;
    public List<OccupationDataTable_Client> occupationDataTableList { get { return mOccupationDataTableList; } }

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
		mInterestRateBaseList = _ee.GetListJson<InterestRateBaseTable_Client>();		
		mItemDataTableList = _ee.GetListJson<ItemDataTable_Client>();
		mItemPriceRateTableList = _ee.GetListJson<ItemPriceRateTable_Client>();
        mOccupationDataTableList = _ee.GetListJson<OccupationDataTable_Client>();
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
    
    public InterestRateBaseTable_Client FindBaseInterestRateTable(long _uid)
	{
		InterestRateBaseTable_Client data = mInterestRateBaseList.Find(d => d.UID == _uid);
		if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
		return default;
	}

	public ItemDataTable_Client FindItemDataTable(long _uid)
	{
		ItemDataTable_Client data = mItemDataTableList.Find(d => d.UID == _uid);
		if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
		return default;
	}

    public GameDataTable_Client GetGameDataTable(string _id)
    {
        GameDataTable_Client data = mGameDataList.Find(d => d.GameDataID == _id);
        if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
        return default;
    }

    public float GetBaseInterestRate(DateTime _dt)
	{
		InterestRateBaseTable_Client data = mInterestRateBaseList.Find(d => d.Year == _dt.Year);
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

	public float GetItemPriceRate(int _year, int _month)
    {
		var findData = mItemPriceRateTableList.Find(_p => _p.Year == _year);
		if(findData == null)
        {
			Debug.LogError($"{_year}년 {_month}월의 가격 변동 정보가 없습니다.");
			return 0f;
        }
		switch(_month){
			case 1:		return findData.month1;
			case 2:		return findData.month2;
			case 3:		return findData.month3;
			case 4:		return findData.month4;
			case 5:		return findData.month5;
			case 6:		return findData.month6;
			case 7:		return findData.month7;
			case 8:		return findData.month8;
			case 9:		return findData.month9;
			case 10:	return findData.month10;
			case 11:	return findData.month11;
			case 12:	return findData.month12;
		}

		return 0f;
	}

    public OccupationDataTable_Client FindOccupationDataTableList(long _uid)
    {
        OccupationDataTable_Client data = mOccupationDataTableList.Find(d => d.UID == _uid);
        if (data != default) return data;
#if LOG
	    Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
        return default;
    }

    public OccupationDataTable_Client GetOccupationDataTable(float _occupationScore)
    {
        OccupationDataTable_Client data = default;
        foreach( var occupationData in mOccupationDataTableList)
        {
            if (occupationData.CreditScore < _occupationScore)
                data = occupationData;
            else
                break;
        }

#if LOG
        if( data == default)
            Log.Error($"UID [{_uid}] 와 맞는 데이터가 없습니다");
#endif
        return data;
    }
}

