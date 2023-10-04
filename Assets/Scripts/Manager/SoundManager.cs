using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public const string IGNORE_SOUND_PATH = "NoneSound";
    
    static public SoundManager Instance = null;
    
    public enum Type {
        Effect,     // 출력 타입 효과음
        BGM         // 출력 타입 배경음
    }

    public enum EffectType
    {
        Stage,      // 스테이지 한번 출력
        StageLoop,  // 스테이지 반복 출력
        UI,         // UI 한번 출력
        UILoop,     // UI 반복 출력
    }

    //글로벌 볼륨 : 조정시 효과, 배경 동시 조절(0 ~ 1)
    float mGlobalVolumn = 1f;
    
    //전체 효과음 볼륨(0 ~ 1)
    float mEffectVolumn = 1f;
    //Stage 효과음 볼륨(0 ~ 1)
    float mStageEffectVolume = 1f;
    //UI 효과음 볼륨(0 ~ 1)
    float mUIEffectVolumn = 1f;
    
    string mCurBgmSoundPath = "";
    string mCurEffectSoundPath = "";

    //배경 전용 AudioSource
    AudioSource mBgmAudioSource;

    //효과 전용 AudioSource(일시 출력 전용)
    AudioSource mStageOneshotEffectAudioSource;
    //효과 전용 AudioSource(반복 출력 전용)
    List<AudioSource> mStageLoopEffectAudioSourceList = new List<AudioSource>();
    //효과 전용 AudioSource(일시 출력 전용)
    AudioSource mUIOneshotEffectAudioSource;
    //효과 전용 AudioSource(반복 출력 전용)
    List<AudioSource> mUILoopEffectAudioSourceList = new List<AudioSource>();

    //Loop Effect AudioSource 생성시 발급되는 ID 생성기
    int mLoopEffectIdGenerator = 0;
    //재생목록
    List<string> mEffectPlayList = new List<string>();

    //출력 했던 오디오 리소스들의 목록
    Dictionary<string, AudioClip> mAudioClipPoolList = new Dictionary<string, AudioClip>();
        
    //[Header("컨트롤 패널 생성 여부")]
    //public bool kIsGuiControlPanel = false;

    private void Awake()
    {
        Instance = this;

        GameObject leftBgmGo = new GameObject();
        leftBgmGo.transform.parent = transform;
        leftBgmGo.transform.localPosition = Vector3.zero;
        leftBgmGo.name = "BGM_AudioSource";
        mBgmAudioSource = leftBgmGo.AddComponent<AudioSource>();
        mBgmAudioSource.dopplerLevel = 0f;
        mBgmAudioSource.reverbZoneMix = 0f;
        mBgmAudioSource.loop = true;
        
        GameObject effectGo = new GameObject();
        effectGo.transform.parent = transform;
        effectGo.transform.localPosition = Vector3.zero;
        effectGo.name = "Effect_OneShot_AudioSource";
        mStageOneshotEffectAudioSource = effectGo.AddComponent<AudioSource>();
        mStageOneshotEffectAudioSource.loop = false;
        mStageOneshotEffectAudioSource.dopplerLevel = 0f;
        mStageOneshotEffectAudioSource.reverbZoneMix = 0f;
        
        GameObject exceptionEffectGo = new GameObject();
        exceptionEffectGo.transform.parent = transform;
        exceptionEffectGo.transform.localPosition = Vector3.zero;
        exceptionEffectGo.name = "Effect_Exception_AudioSource";
        mUIOneshotEffectAudioSource = exceptionEffectGo.AddComponent<AudioSource>();
        mUIOneshotEffectAudioSource.loop = false;
        mUIOneshotEffectAudioSource.dopplerLevel = 0f;
        mUIOneshotEffectAudioSource.reverbZoneMix = 0f;
    }

    //////////////////////////////////////////////////////////////////////////////////
    //*공용
    #region Common    
    /// <summary>오디오클립 선행 로드</summary>
    bool Load(string _path)
    {
        if (mAudioClipPoolList.ContainsKey(_path) == true) {
            Debug.Log("읽으려는 '" + _path + "'오디오클립 형태의 리소스가 이미 존재합니다.");
            return false;
        }

        AudioClip clip = Resources.Load<AudioClip>(_path);
        if (clip == null) {
            Debug.Log("읽으려는 '" + _path + "'오디오클립 형태의 리소스가 없습니다.");
            return false;
        }

        mAudioClipPoolList[_path] = clip;

        return true;
    }

    /// <summary>배경 오디오소스 검색</summary>
    AudioSource GetBgmAudioSource()
    {
        return mBgmAudioSource;
    }

    /// <summary>효과 오디오소스 검색</summary>
    AudioSource GetOncshotEffectAudioSource()
    {
        return mStageOneshotEffectAudioSource;
    }

    /// <summary>현재까지 읽어들이 모든 오디오클립 중 획득</summary>
    public AudioClip GetAudioClip(string _path)
    {
        if (mAudioClipPoolList.ContainsKey(_path) == false) {
            if (Load(_path) == false) {
                Debug.Log("'" + _path + "' 가져오는데 실패 했습니다.");
                return null;
            }
        }

        return mAudioClipPoolList[_path];
    }

    /// <summary>모든 볼륨 통합 조정</summary>
    public void SetGlobalVolumn(float _value)
    {
        mGlobalVolumn = _value;
        SetBgmVolume(_value);
        SetEffectVolume(_value);
    }

    public float GetGlobalVolumn()
    {
        return mGlobalVolumn;
    }

    /// <summary>모든 소리 일시 멈춤</summary>
    public void Pause(bool _isPause)
    {
        if (_isPause == true)
        {
            mBgmAudioSource.Pause();
            
            mStageOneshotEffectAudioSource.Pause();
            foreach (var audio in mStageLoopEffectAudioSourceList)
                audio.Pause();
            
            mUIOneshotEffectAudioSource.Pause();
            foreach (var audio in mUILoopEffectAudioSourceList)
                audio.Pause();
        }
        else
        {
            mBgmAudioSource.UnPause();
            
            mStageOneshotEffectAudioSource.UnPause();
            foreach (var audio in mStageLoopEffectAudioSourceList)
                audio.UnPause();
            
            mUIOneshotEffectAudioSource.UnPause();
            foreach (var audio in mUILoopEffectAudioSourceList)
                audio.UnPause();
        }
    }

    /// <summary>모든 소리 멈춤</summary>
    public void AllStop()
    {
        StopBGM();
        AllStopEffect();
    }

    public void Clear()
    {
        AllStop();
        ClearEffectPlayList();
        Release(mCurBgmSoundPath);
        mAudioClipPoolList.Clear();
    }
    
    public void EffectClear()
    {
        var releasePathList = mAudioClipPoolList.Where(_p => _p.Key != mCurBgmSoundPath).Select(_p=> _p.Key).ToList();
        foreach(var audioPath in releasePathList)
            Release(audioPath);
    }
    #endregion

    //////////////////////////////////////////////////////////////////////////////////
    //*BGM 관련
    #region Bgm
    
    /// <summary>배경 소리 크기 조절</summary>
    public void SetBgmVolume(float _value)
    {
        mBgmAudioSource.volume = _value;
    }

    public float GetBgmVolume()
    {
        return mBgmAudioSource.volume;
    }

    /// <summary>해당 키가 출력되고 있는 배경음인가?</summary>
    public bool IsPlayBgm(string _bmg)
    {
        if (mCurBgmSoundPath == _bmg && mBgmAudioSource.isPlaying == true)
            return true;
        
        return false;
    }

    /// <summary>배경음이 출력 중인가?</summary>
    public bool IsPlayBgm()
    {
        return mBgmAudioSource.isPlaying;
    }
    
    /// <summary>배경 재생</summary>
    public void PlayBgm(string _key)
    {
        if (IsPlayBgm(_key) == true)
            return;
        
        AudioClip clip = GetAudioClip(_key);
        if (clip == null)
            return;

        Release(mCurBgmSoundPath);
        
        mBgmAudioSource.clip = clip;
        mBgmAudioSource.loop = true;
        mBgmAudioSource.Play();

        mCurBgmSoundPath = _key;
    }

    /// <summary>배경 재생</summary>
    public void PlayBgm(AudioClip _clip)
    {
        if (IsPlayBgm(_clip.name) == true)
            return;
                
        if(mAudioClipPoolList.ContainsKey(_clip.name) == false)
            mAudioClipPoolList[_clip.name] = _clip;

        Release(mCurBgmSoundPath);

        mBgmAudioSource.clip = _clip;
        mBgmAudioSource.loop = true;
        mBgmAudioSource.Play();

        mCurBgmSoundPath = _clip.name;
    }

    public void StopBGM()
    {
        mBgmAudioSource.Stop();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    
    public void Release(string _key)
    {
       /*
       if (_key.IsNullOrEmpty() == true)
            return;
        
        mAudioClipPoolList.Remove(_key);
        Mng.add.ReleaseAudioAsset(_key);
       */
    }
    #endregion

    //////////////////////////////////////////////////////////////////////////////////
    //*Effect 관련
    #region Effect

    public void AddEffectList(string _key)
    {
        if (Load(_key) == true)
            mEffectPlayList.Add(_key);
    }

    public void RemoveEffectList(string _key)
    {
        mEffectPlayList.Remove(_key);
    }

    public void ClearEffectPlayList()
    {
        mEffectPlayList.Clear();
    }

    AudioSource CreateStageLoopEffectAudioSource()
    {
        //검색시 0은 mOneshotEffectAudiosource이며 mLoopEffectAudiosourceList의 key값 삽입에서 제외        
        GameObject effectGo = new GameObject();
        effectGo.transform.parent = transform;
        effectGo.transform.localPosition = Vector3.zero;
        effectGo.name = "Stage_Effect_Loop_AudioSource_ID_" + mLoopEffectIdGenerator.ToString();

        mLoopEffectIdGenerator++;
        
        AudioSource audio = effectGo.AddComponent<AudioSource>();
        audio.volume = mStageEffectVolume;
        mStageLoopEffectAudioSourceList.Add(audio);
        return audio;
    }

    AudioSource CreateUILoopEffectAudioSource()
    {
        //검색시 0은 mOneshotEffectAudiosource이며 mLoopEffectAudiosourceList의 key값 삽입에서 제외        
        GameObject effectGo = new GameObject();
        effectGo.transform.parent = transform;
        effectGo.transform.localPosition = Vector3.zero;
        effectGo.name = "UI_Effect_Loop_AudioSource_ID_" + mLoopEffectIdGenerator.ToString();

        mLoopEffectIdGenerator++;
        
        AudioSource audio = effectGo.AddComponent<AudioSource>();
        audio.volume = mUIEffectVolumn;
        mStageLoopEffectAudioSourceList.Add(audio);
        return audio;
    }
    
    public void SetEffectVolume(EffectType _type, float _value)
    {
        switch (_type)
        {
            case EffectType.Stage:
            case EffectType.StageLoop:
                mStageEffectVolume = _value;
                
                mStageOneshotEffectAudioSource.volume = _value;
                
                foreach (var audio in mStageLoopEffectAudioSourceList)
                    audio.volume = _value;
                break;
            case EffectType.UI:
            case EffectType.UILoop:
                mUIEffectVolumn = _value;
                
                mUIOneshotEffectAudioSource.volume = _value;
                
                foreach (var audio in mUILoopEffectAudioSourceList)
                    audio.volume = _value;
                break;
        }
    }
    
    public void SetEffectVolume(float _value)
    {
        mEffectVolumn = _value;
        
        mUIEffectVolumn = mEffectVolumn;
        mStageEffectVolume = mEffectVolumn;
        
        mStageOneshotEffectAudioSource.volume = mStageEffectVolume;
        foreach (var audio in mStageLoopEffectAudioSourceList)
            audio.volume = mStageEffectVolume;
        
        mUIOneshotEffectAudioSource.volume = mUIEffectVolumn;
        foreach (var audio in mUILoopEffectAudioSourceList)
            audio.volume = mUIEffectVolumn;
    }

    public float GetEffectVolume(EffectType _type)
    {
        switch (_type)
        {
            case EffectType.Stage:
            case EffectType.StageLoop:
                return mStageEffectVolume;
            case EffectType.UI:
            case EffectType.UILoop:
                return mUIEffectVolumn;
        }

        return 0f;
    }

    public float GetEffectVolume()
    {
        return mEffectVolumn;
    }
    
    /// <summary>가장 최근 출력된 효과음 경로</summary>
    public string GetCurrentPlayEffect()
    {
        return mCurEffectSoundPath;
    }

    public AudioSource PlayEffect(string _key, EffectType typeType = EffectType.Stage, float _pitch = 1f)
    {
        if (_key == IGNORE_SOUND_PATH)
            return null;
            
        AudioClip clip = GetAudioClip(_key);
        if (clip == null)
            return null;

        AudioSource audio = null;

        switch (typeType)
        {
            case EffectType.Stage: {
                AudioSource onceshotAudio = mStageOneshotEffectAudioSource;
                onceshotAudio.pitch = _pitch;
                onceshotAudio.PlayOneShot(clip);
                audio = onceshotAudio;
            } break;
            case EffectType.StageLoop: {
                AudioSource loopAudio = CreateStageLoopEffectAudioSource();
                loopAudio.volume = mStageOneshotEffectAudioSource.volume;
                loopAudio.loop = true;
                loopAudio.clip = clip;
                loopAudio.pitch = _pitch;
                loopAudio.Play();
                mStageLoopEffectAudioSourceList.Add(loopAudio);
                audio = loopAudio;
            } break;
            case EffectType.UI: {
                AudioSource oneshotAudio = mUIOneshotEffectAudioSource;
                oneshotAudio.pitch = _pitch;
                oneshotAudio.PlayOneShot(clip);
                audio = oneshotAudio;
            } break;
            case EffectType.UILoop: {
                AudioSource loopAudio = CreateUILoopEffectAudioSource();
                loopAudio.volume = mUIOneshotEffectAudioSource.volume;
                loopAudio.loop = true;
                loopAudio.clip = clip;
                loopAudio.pitch = _pitch;
                loopAudio.Play();
                mStageLoopEffectAudioSourceList.Add(loopAudio);
                audio = loopAudio;
            } break;
        }
        
        mCurEffectSoundPath = _key;

        if (mEffectPlayList.Contains(_key) == false)
            mEffectPlayList.Add(_key);

        //-1 : 재생 실패, 0 : 메인(OneShot), 1~ : 나머지는 생성(Loop)
        return audio;
    }
    public void StopEffect(EffectType _type, int _index)
    {
        switch (_type)
        {
            case EffectType.Stage:
                mStageOneshotEffectAudioSource.Stop();
                break;
            case EffectType.StageLoop: {
                if (_index >= mStageLoopEffectAudioSourceList.Count || _index < 0 ) {
                    Debug.Log("멈춤에 실패 했습니다.");
                    return;
                }
                
                AudioSource audio = mStageLoopEffectAudioSourceList[_index];
                audio.Stop();
                mStageLoopEffectAudioSourceList.RemoveAt(_index);
                Destroy(audio.gameObject);
            } break;
            case EffectType.UI:
                mUIOneshotEffectAudioSource.Stop();
                break;
            case EffectType.UILoop: {
                if (_index >= mUILoopEffectAudioSourceList.Count || _index < 0 ) {
                    Debug.Log("멈춤에 실패 했습니다.");
                    return;
                }
                
                AudioSource audio = mStageLoopEffectAudioSourceList[_index];
                audio.Stop();
                mStageLoopEffectAudioSourceList.RemoveAt(_index);
                Destroy(audio.gameObject);
            } break; 
        }
    }

    /// <summary>모든 효과음 중지</summary>
    public void AllStopEffect()
    {
        mStageOneshotEffectAudioSource.Stop();

        foreach (var audio in mStageLoopEffectAudioSourceList){
            audio.Stop();
            Destroy(audio.gameObject);
        }

        mStageLoopEffectAudioSourceList.Clear();
        
        mUIOneshotEffectAudioSource.Stop();

        foreach (var audio in mUILoopEffectAudioSourceList){
            audio.Stop();
            Destroy(audio.gameObject);
        }

        mUILoopEffectAudioSourceList.Clear();
    }
    #endregion
}
