using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    IPlayerPhysical physical
    {
        get;
    }

    IPlayerAnimation animation
    {
        get;
    }

    IPlayerUI ui
    {
        get;
    }

    IPlayerSlowMotin slowmotin
    {
        get;
    }
}

