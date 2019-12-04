using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopUp 
{
    GameObject obj
    {
        get;
    }
    void Enable();

    void Disable();
    
}
