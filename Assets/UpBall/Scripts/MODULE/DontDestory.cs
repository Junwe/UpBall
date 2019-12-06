using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DontDestory : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        //string temp = JsonUtility.ToJson(LevelingData.Instance.info,true);
        //Debug.Log(temp);
        StartCoroutine(GetLevingData());
    }

    private IEnumerator GetLevingData()
    {
        var requset = new WWW("https://docs.google.com/uc?id=1mPKFwr5Xu_jLb4cDWa3FwYFJTn55zOR_&export=download"); // 바로 다운로드 링크 디버그용
        yield return requset;
        
        if(requset != null)
        {
            LevelingData.Instance.SetInfo(requset.text);
        }
    }
}
