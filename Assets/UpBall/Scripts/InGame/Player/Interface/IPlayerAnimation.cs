using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAnimation
{
    Animator Animator
    {
        get;
    }

    Animation Animation
    {
        get;
    }

    void SetStateEvent(int mouseEvent);

    void Oncollision(Collision2D collision, BALLSTATE _ballState);

}
