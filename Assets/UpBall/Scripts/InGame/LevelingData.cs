using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingData : MonoSingleton<LevelingData>
{
    public LevelInfo info = new LevelInfo();
    public int minBlockCnt = 4;
    public int maxBlockCnt = 5;

    public float slowDurationTime = 0.8f;

    public bool IsExit = false;
    public bool IsDie = false;

    public int nLevelCount = 0;

    private string _datajson;

    public float curTimeScale = 1f;
    public void ReSetData()
    {

        SetInfo(_datajson);        
        // info.moveSpeed = -1f;
        // info.wallCreateTime = 3f;
        // minBlockCnt = 4;
        // maxBlockCnt = 4;
        // slowDurationTime = 0.5f;

        IsExit = false;
        IsDie = false;

        curTimeScale = 1f;

        nLevelCount = 0;
    }

    public void SetInfo(string json)
    {
        _datajson = json;
        info = JsonUtility.FromJson<LevelInfo>(_datajson);
    }

    public  void SetMoveSpeed(float moveSpeed)
    {
        moveSpeed = Mathf.Clamp(moveSpeed, info._moveSpeedMin, -0.75f);
    }

    public  void SetWallCreateTime(float createTime)
    {
        info.wallCreateTime = Mathf.Clamp(createTime, info._wallCreateTimeMin, 5f);
    }

    public  void SetBlockCnt(int min, int max)
    {
        minBlockCnt = Mathf.Clamp(min, 1, 5);
        maxBlockCnt = Mathf.Clamp(max, min, 5);
    }

    public  void SetSlowTime(float slowTime)
    {
        slowDurationTime = Mathf.Clamp(slowTime, info._slowDurationTimeMin, 0.8f);
    }

    public  void SetNextLevel(int score)
    {
        SetMoveSpeed(info.moveSpeed - info._moveSpeedDecrease);
        SetWallCreateTime(info.wallCreateTime - info._wallCreateTimeDecrease);

        if (score % 30 == 0)
        {
            SetBlockCnt(minBlockCnt - 1, minBlockCnt + 1);
            SetSlowTime(slowDurationTime - info._slowDurationDecrease);
            nLevelCount++;
            Mathf.Clamp(nLevelCount, 0, 5);
        }

    }

}
