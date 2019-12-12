using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class POPUP_Base : MonoBehaviour, IPopUp
{
    public TweenScale _openTween;
    public GameObject obj
    {
        get { return gameObject;}
    }

    public void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
        gameObject.transform.localScale = Vector3.zero;
    }

    public void Enable()
    {
        _openTween.Time = 0.25f;
        _openTween.StartTween();
    }

    public void Enable(object value)
    {
        Enable();
    }

    public void Disable()
    {
        _openTween.Time = 0.25f;
        _openTween.ReversePlay();
    }
}
