using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class POPUP_SystemMsg : MonoBehaviour, IPopUp
{    
    public TweenScale _openTween;

    private UnityAction<string> _startCallBack;

    public UnityAction<string> StartCallBack
    {
        get { return _startCallBack;}
        set {_startCallBack = value;}
    }

    [SerializeField]
    private Text _textMsg;
    public GameObject obj
    {
        get { return gameObject;}
    }
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
        gameObject.SetActive(false);
        _startCallBack = (string value)=> {SetTextMsg(value);};
    }
    public void Enable()
    {
        _openTween.Time = 0.25f;
        gameObject.SetActive(true);
        _openTween.StartTween();
    }
 
    public void Disable()
    {
        _openTween.Time = 0.25f;
        _openTween.ReversePlay();
    }
    
    public void SetTextMsg(string msg)
    {
        _textMsg.text = msg;
    }
}
