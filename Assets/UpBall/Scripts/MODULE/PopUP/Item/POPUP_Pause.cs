using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPUP_Pause : MonoBehaviour, IPopUp
{
    [SerializeField]
    private TweenScale _openTween;
    public GameObject obj
    {
        get { return gameObject;}
    }
    
    // Start is called before the first frame update
    
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);   // 팝업에 등록
    }
    public void Enable()
    {
        gameObject.SetActive(true);
        LevelingData.IsExit = true;
        _openTween.StartTween();
    }

    public void Disable()
    {
        Invoke("SetIsExit",0.05f);
        _openTween.ReversePlay();
    }

    private void SetIsExit()
    {
        LevelingData.IsExit = false;
    }

    public void ClickPlay()
    {
        Disable();
        LevelingData.IsExit = false;
    }

    public void ClickReTry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        LevelingData.IsExit = false;
    }

    public void ClickMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        LevelingData.IsExit = false;
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
