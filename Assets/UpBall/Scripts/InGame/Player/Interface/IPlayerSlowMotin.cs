using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IPlayerSlowMotin
{
    GameObject objSlowDuration
    {
        get;
    }
    float SlowMotionValue
    {
        get;
    }
    void SetStateEvent(int mouseEvent, PlayerInfo info);

    void Oncollision(Collision2D collision);

    void ClearPlayer();
}
