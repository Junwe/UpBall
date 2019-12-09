using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingData : MonoSingleton<LevelingData>
{
    public LevelInfo info;
    public int minBlockCnt = 5;
    public int maxBlockCnt = 5;

    public bool IsExit = false;
    public bool IsDie = false;

    public int nLevelCount = 0;

    private string _datajson;

    public float curTimeScale = 1f;
    public void ReSetData()
    {
        if (_datajson.Length >= 1)
        {
            SetInfo(_datajson);
        }
        else
        {
            info = new LevelInfo();
        }       

         minBlockCnt = 5;
         maxBlockCnt = 5;

        IsExit = false;
        IsDie = false;

        curTimeScale = 1f;

        nLevelCount = 0;
    }

    public void SetInfo(string json = "")
    {
        _datajson = json;
        if (_datajson.Length >= 1)
        {
            info = JsonUtility.FromJson<LevelInfo>(_datajson);
        }
        else
        {
            info = new LevelInfo();
        }
    }

    private  void SetMoveSpeed(float moveSpeed)
    {
        info.moveSpeed = Mathf.Clamp(moveSpeed, info._moveSpeedMin, 0.0f);
    }

    private  void SetWallCreateTime(float createTime)
    {
        info.wallCreateTime = Mathf.Clamp(createTime, info._wallCreateTimeMin, 10f);
        
    }

    private  void SetBlockCnt(int min, int max)
    {
        minBlockCnt = Mathf.Clamp(min, 1, 5);
        maxBlockCnt = Mathf.Clamp(max, min, 5);
    }

    private  void SetSlowTime(float slowTime)
    {
        info.slowDurationTime = Mathf.Clamp(slowTime, info._slowDurationTimeMin, 0.8f);
    }

    public  void SetSmallNextLevel()
    {
        Debug.Log(info.moveSpeed + info._moveSpeedDecrease);
        SetMoveSpeed(info.moveSpeed + info._moveSpeedDecrease);
        SetWallCreateTime(info.wallCreateTime + info._wallCreateTimeDecrease);
    }

    public void SetBigNextLevel()
    {
        SetBlockCnt(minBlockCnt - 1, minBlockCnt + 1);
        SetSlowTime(info.slowDurationTime + info._slowDurationDecrease);
        nLevelCount++;
        nLevelCount = Mathf.Clamp(nLevelCount, 0, info.nextLevelScoreList.Length - 1);
    }

}
