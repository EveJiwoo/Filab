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
		mGameDataList= _ee.GetListJson<GameDataTable_Client>();
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
}

