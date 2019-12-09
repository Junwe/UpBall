using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DontDestory : MonoBehaviour
{
    [SerializeField]    
    private TextAsset _levlingdata;
    void Awake()
    {
        DontDestroyOnLoad(this);
        LevelingData.Instance.SetInfo(_levlingdata.text);
        //string temp = JsonUtility.ToJson(LevelingData.Instance.info,true);
        //Debug.Log(temp);
        //StartCoroutine(GetLevingData());
    }

    public void ClickUpdate()
    {
        StartCoroutine(GetLevingData());
    }
    private IEnumerator GetLevingData()
    {
        var requset = new WWW("https://docs.google.com/uc?id=1mPKFwr5Xu_jLb4cDWa3FwYFJTn55zOR_&export=download"); // 바로 다운로드 링크 디버그용
        yield return requset;
        
        if(requset.error == null)
        {
            LevelingData.Instance.SetInfo(requset.text);
            PopUpManager.Instance.EnablePopUp("I_PopUp_SystemMsg","업데이트 되었습니다.");
            //temp.SetTextMsg("업데이트 되었습니다.");
        }
        else
        {
            PopUpManager.Instance.EnablePopUp("I_PopUp_SystemMsg","실패!, 에러메세지 : " + requset.error);
            //temp.SetTextMsg("실패!, 에러메세지 : " + requset.error);
        }
    }
}
