using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenMove : MonoBehaviour, ITween
{
    enum TWEENTYPE
    {
        Transform,
        RectTransform,
    }


    [SerializeField]
    Vector3 _start;
    [SerializeField]
    Vector3 _end;
    [SerializeField]
    float _time;
    [SerializeField]
    float _delay;
    [SerializeField]
    float _startDleay;
    [SerializeField]
    AnimationCurve _curve;
    [SerializeField]
    bool _isAwake;
    [SerializeField]
    bool _isLoop;

    Coroutine _crt;
    [SerializeField]
    TWEENTYPE _type;

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
            gameObject.transform.localPosition = _end;
        }
    }

    public void StartTween()
    {
        End();
        if (_isLoop)
        {
            if(_type == TWEENTYPE.RectTransform)
                _crt = StartCoroutine(Tween.instance.MoveLoop(GetComponent<RectTransform>(), _start, _end, _time, _delay, _startDleay, _curve));
            else if(_type == TWEENTYPE.Transform)
                _crt = StartCoroutine(Tween.instance.MoveLoop(gameObject, _start, _end, _time, _delay,_startDleay, _curve));
        }
        else
        {
            if(_type == TWEENTYPE.RectTransform)
                _crt = StartCoroutine(Tween.instance.Move(GetComponent<RectTransform>(), _start, _end, _time, _delay, _curve));
            else if(_type == TWEENTYPE.Transform)
                _crt = StartCoroutine(Tween.instance.Move(gameObject, _start, _end, _time, _delay, _curve));
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
