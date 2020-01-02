using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localizationData : MonoSingleton<localizationData>
{
    public string Key;

    public void SetLanguage(string Key)
    {
        Debug.Log(Key);
        this.Key = Key;
    }
}
