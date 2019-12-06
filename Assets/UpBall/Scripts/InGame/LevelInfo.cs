using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelInfo
{
    public float moveSpeed = 0.75f;
    public float wallCreateTime = 5f;
    public float slowDurationTime = 0.8f;
    public float _moveSpeedDecrease = -0.1f;
    public float _wallCreateTimeDecrease = -0.45f;
    public float _slowDurationDecrease = -0.1f;
    public float _moveSpeedMin = -3.0f;
    public float _wallCreateTimeMin = 2f;
    public float _slowDurationTimeMin = 0.3f;

    public void Set(LevelInfo info)
    {
        moveSpeed = info.moveSpeed;
    }

}
