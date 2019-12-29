using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// 모든 팝업에 기본이 되는 베이스.
// enable, disable을 기본으로 가짐
// 재정의가 필요한 경우 base를 상속받고 enable, disable을 재정의.
public class POPUP_Base : MonoBehaviour, IPopUp
{
    public TweenScale _openTween;
    public GameObject obj
    {
        get { return gameObject; }
    }

    public void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
        if (_openTween != null)
        {
            gameObject.transform.localScale = Vector3.zero;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public virtual void Enable()
    {
        if (_openTween != null)
        {
            _openTween.Time = 0.25f;
            _openTween.StartTween();
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void Enable(object value)
    {
        Enable();
    }

    public virtual void Disable()
    {
        if (_openTween != null)
        {
            _openTween.Time = 0.25f;
            _openTween.ReversePlay();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
