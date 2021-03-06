﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchPower : MonoBehaviour
{

    public static TouchPower instance = null;

    public GameObject objStart;
    public GameObject objEnd;

    private Vector3 _startMousePos;

    private Vector3 _direction;
    private float _movePower;

    private float _maxPower = 13.2f;
    private float _maxDistance = 300f;

    public Vector3 Direction
    {
        get
        {
            return _direction;
        }

        set
        {
            _direction = value;
        }
    }

    public float MovePower
    {
        get
        {
            return _movePower;
        }

        set
        {
            _movePower = value;
        }
    }

    public float MaxPower
    {
        get
        {
            return _maxPower;
        }

        set
        {
            _maxPower = value;
        }
    }

    public Vector3 StartMousePos
    {
        get
        {
            return _startMousePos;
        }

        set
        {
            _startMousePos = value;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.Instance.IsDie || LevelingData.Instance.IsExit)
        {
            objEnd.SetActive(false);
            objStart.SetActive(false);
            return;
        }

        bool isTouchPossivle = true;
        bool _isDown = false;
        bool _isDrage = false;
        bool _isUp = false;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(0))
                {
                    isTouchPossivle = false;
                }
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    _isDown = true;
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    _isDrage = true;
                if (touch.phase == TouchPhase.Ended)
                    _isUp = true;
            }
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))
            {
                isTouchPossivle = false;
            }
            if (Input.GetMouseButtonDown(0))
                _isDown = true;
            if (Input.GetMouseButton(0))
                _isDrage = true;
            if (Input.GetMouseButtonUp(0))
                _isUp = true;
        }

        if (isTouchPossivle)
        {
            if (_isDown)
            {
                objStart.SetActive(true);
                objStart.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(StartMousePos).x, Camera.main.ScreenToWorldPoint(StartMousePos).y, -3f);
            }
            if (_isUp)
            {
                objStart.SetActive(false);
                objEnd.SetActive(false);
            }
            if (_isDrage)
            {
                Vector3 vEnd = GetObjEndPos(Input.mousePosition);
                objEnd.transform.localPosition = new Vector3(vEnd.x, vEnd.y, -3f);
                objEnd.SetActive(true);
            }
        }
    }
    public void SetUpEvent(Vector3 mousePosition)
    {
        objStart.SetActive(false);
        objEnd.SetActive(false);
    }

    public void SetDownEvent(Vector3 mousePosition)
    {
        StartMousePos = mousePosition;
        objStart.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(StartMousePos).x, Camera.main.ScreenToWorldPoint(StartMousePos).y, -3f);
    }

    public void SetStayEvent(Vector3 mousePosition)
    {
        Vector3 vEnd = GetObjEndPos(mousePosition);
        instance.objEnd.transform.localPosition = new Vector3(vEnd.x, vEnd.y, -3f);
        objEnd.SetActive(true);
        objStart.SetActive(true);

        SetTouchPowerInfo(mousePosition);
    }

    public void SetTouchPowerInfo(Vector3 _endMousePos)
    {
        Direction = (StartMousePos - _endMousePos).normalized;
        float distance = Mathf.Abs(Vector3.Distance(_endMousePos, StartMousePos));

        if (distance >= _maxDistance)
            distance = _maxDistance;

        // 얘는 월드 좌표랑 상관없으니 괜찮
        MovePower = (distance / _maxDistance) * MaxPower;
    }

    public Vector3 GetObjEndPos(Vector3 _endMousePos)
    {
        Vector3 endPos = (StartMousePos - _endMousePos).normalized;
        Vector3 zeroPos = Camera.main.ScreenToWorldPoint(Vector3.zero); // 0기준

        float distance = Vector3.Distance(_endMousePos, StartMousePos);

        if (distance >= _maxDistance)
            distance = _maxDistance;

        endPos *= distance;

        // 월드 좌표로 바꿔주고 0을 기준으로
        endPos = new Vector3(zeroPos.x - Camera.main.ScreenToWorldPoint(endPos).x, zeroPos.y - Camera.main.ScreenToWorldPoint(endPos).y, 0f);
        // startobj 좌표 기준으로 바꿔주기
        endPos = new Vector3(objStart.transform.localPosition.x + endPos.x, objStart.transform.localPosition.y + endPos.y);

        return endPos;
    }
}
