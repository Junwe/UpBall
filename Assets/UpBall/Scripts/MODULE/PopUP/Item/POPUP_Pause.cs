using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class POPUP_Pause : POPUP_Base, IPopUp
{
    public override void Enable()
    {
        base.Enable();
        LevelingData.Instance.IsExit = true;
    }

    public override void Disable()
    {
        base.Disable();
        Invoke("SetIsExit",0.05f);
    }

    private void SetIsExit()
    {
        LevelingData.Instance.IsExit = false;
    }

    public void ClickPlay()
    {
        PopUpManager.Instance.DisablePopUp(gameObject.name);
    }

    public void ClickReTry()
    {
        LevelingData.Instance.ReSetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ClickMenu()
    {
        LevelingData.Instance.ReSetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ClickCalnle()
    {
        PopUpManager.Instance.DisablePopUp(name);
    }

    public void ClickQut()
    {
        Application.Quit();
    }
}
