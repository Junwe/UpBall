using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class POPUP_Main : MonoBehaviour,IPopUp
{
    [SerializeField]
    private TweenScale _openTween;

    private UnityAction<string> _startCallBack = (string value) => { };

    public UnityAction<string> StartCallBack
    {
        get { return _startCallBack;}
        set {_startCallBack = value;}
    }
    // Start is called before the first frame update
    public GameObject obj
    {
        get { return gameObject;}
    }
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        _openTween.StartTween();
    }

    public void Disable()
    {
        _openTween.ReversePlay();
    }

    public void ClickCreater()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Creater");
    }

    public void ClickGuide()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Guide");
    }
    
    public void ClickCalnle()
    {
        PopUpManager.Instance.DisablePopUp(name);
    }

}
