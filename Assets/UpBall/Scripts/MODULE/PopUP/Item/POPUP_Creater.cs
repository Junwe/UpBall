using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class POPUP_Creater : POPUP_Base, IPopUp
{    
    public new void Enable()
    {
        base.Enable();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Game_Upball"))
        {
            LevelingData.Instance.IsExit = true;
        }
    }
 
    public new void Disable()
    {
        base.Disable();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Game_Upball"))
        {
            Invoke("SetIsExit", 0.05f);
            PlayerPrefs.SetInt("FistGuide", 1);
        }
    }

    private void SetIsExit()
    {
        LevelingData.Instance.IsExit = false;
    }
}
