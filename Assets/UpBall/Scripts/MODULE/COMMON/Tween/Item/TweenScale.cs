using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenScale : MonoBehaviour, ITween
{
    enum TWEENSCALE
    {
        RECTTRAN,
        TRAN,
    }
    [SerializeField]
    Vector2 _start;
    [SerializeField]
    Vector2 _end;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    AnimationCurve _curve;
    [SerializeField]
    bool _isAwake;
    [SerializeField]
    bool _isLoop;
    [SerializeField]
    TWEENSCALE _type;

    Coroutine _crt;

    public float Time
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
        }
    }

    public void End()
    {
        if (_crt != null)
        {
            StopCoroutine(_crt);
            GetComponent<RectTransform>().localScale = _end;
        }
    }

    public void StartTween()
    {
        End();
        if (_isLoop)
        {
            if (_type == TWEENSCALE.RECTTRAN)
                _crt = StartCoroutine(Tween.instance.SetScaleLoop(GetComponent<RectTransform>(), _start, _end, _time, _delay, _curve));
            else if(_type == TWEENSCALE.TRAN)
                _crt = StartCoroutine(Tween.instance.SetScaleLoop(transform, _start, _end, _time, _delay, _curve));
        }
        else
        {
            if (_type == TWEENSCALE.RECTTRAN)
                _crt = StartCoroutine(Tween.instance.SetScale(GetComponent<RectTransform>(), _start, _end, _time, _delay, _curve));
            else if (_type == TWEENSCALE.TRAN)
                _crt = StartCoroutine(Tween.instance.SetScale(transform, _start, _end, _time, _delay, _curve));
        }
    }

    public void ReversePlay()
    {
        End();
        _start.Swap(ref _end);
        StartTween();
        _start.Swap(ref _end);
    }

    void Start()
    {
        if (_isAwake)
            StartTween();
    }

}
