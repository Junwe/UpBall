﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public interface IPopUp 
{
    GameObject obj
    {
        get;
    }
    void Enable();

    void Disable();

    UnityAction<string> StartCallBack
    {
        get;
        set;
    }
    
}