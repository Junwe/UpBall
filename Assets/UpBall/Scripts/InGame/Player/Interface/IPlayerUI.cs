using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerUI
{
    GameObject objTrailRender
    {
        get;
    }
    void Oncollision();

    void SetGage(float value);
}
