using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class POPUP_Pause : MonoBehaviour, IPopUp
{
    [SerializeField]
    private TweenScale _openTween;
    public GameObject obj
    {
        get { return gameObject;}
    }

    private UnityAction<string> _startCallBack = (string value)=>{};

    public UnityAction<string> StartCallBack
    {
        get { return _startCallBack;}
        set {_startCallBack = value;}
    }
    
    // Start is called before the first frame update
    
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);   // 팝업에 등록
    }
    public void Enable()
    {
        gameObject.SetActive(true);
        LevelingData.Instance.IsExit = true;
        _openTween.StartTween();
        StartCallBack(null);
    }

    public void Disable()
    {
        Invoke("SetIsExit",0.05f);
        _openTween.ReversePlay();
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
        LevelingData.Instance.IsExit = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ClickMenu()
    {
        LevelingData.Instance.IsExit = false;
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
