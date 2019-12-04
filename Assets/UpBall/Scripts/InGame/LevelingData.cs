using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelingData
{
    public static float moveSpeed = -0.75f;
    public static float wallCreateTime = 5f;

    public static int minBlockCnt = 4;
    public static int maxBlockCnt = 5;

    public static float slowDurationTime = 0.8f;

    public static bool IsExit = false;
    public static bool IsDie = false;

    public static int nLevelCount = 0;


    public static float curTimeScale = 1f;
    public static void ReSetData()
    {
        LevelingData.moveSpeed = -1f;
        LevelingData.wallCreateTime = 3f;
        LevelingData.minBlockCnt = 4;
        LevelingData.maxBlockCnt = 4;
        slowDurationTime = 0.5f;

        LevelingData.IsExit = false;
        LevelingData.IsDie = false;

        curTimeScale = 1f;

        nLevelCount = 0;
    }

    public static void SetMoveSpeed(float moveSpeed)
    {
        LevelingData.moveSpeed = Mathf.Clamp(moveSpeed, -3.0f, -0.75f);
    }

    public static void SetWallCreateTime(float createTime)
    {
        wallCreateTime = Mathf.Clamp(createTime, 2f, 5f);
    }

    public static void SetBlockCnt(int min, int max)
    {
        minBlockCnt = Mathf.Clamp(min, 1, 5);
        maxBlockCnt = Mathf.Clamp(max, min, 5);
    }

    public static void SetSlowTime(float slowTime)
    {
        slowDurationTime = Mathf.Clamp(slowTime, 0.3f, 0.8f);
    }

    public static void SetNextLevel(int score)
    {
        SetMoveSpeed(moveSpeed - 0.1f);
        SetWallCreateTime(wallCreateTime - 0.45f);

        if (score % 30 == 0)
        {
            SetBlockCnt(minBlockCnt - 1, minBlockCnt + 1);
            SetSlowTime(slowDurationTime - 0.1f);
            nLevelCount++;
            Mathf.Clamp(nLevelCount, 0, 5);
        }

    }

}
