﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpManager : MonoSingleton<PopUpManager>
{
    private Dictionary<string,IPopUp> _popupToStringInScene = new Dictionary<string, IPopUp>();

    private Stack<IPopUp> _popUpLlistStack = new Stack<IPopUp>();

    private Button _btnBackGourndClose;

    public GameObject _objBackGroundBtn;
    public void AddPop(string name, IPopUp popup)
    {
        if(_popupToStringInScene.ContainsKey(name))
        {
            _popupToStringInScene.Remove(name);
        }
        _popupToStringInScene.Add(name,popup);
    }

    private void SetBackGroundHierarchy(Transform target)
    {
        _btnBackGourndClose.gameObject.SetActive(true);
        _btnBackGourndClose.gameObject.transform.SetParent(target.parent); 
        _btnBackGourndClose.gameObject.transform.localPosition = Vector3.zero;
        _btnBackGourndClose.gameObject.transform.SetSiblingIndex(target.GetSiblingIndex() - 1);
    }

    public void EnablePopUp(string name)
    {
        if (_btnBackGourndClose == null)
        {
            CreateBackGourndBtn();
        }
        SetBackGroundHierarchy(_popupToStringInScene[name].obj.transform);
        _popUpLlistStack.Push(_popupToStringInScene[name]);
        _popupToStringInScene[name].Enable();
    }
    public void DisablePopUp(string name)
    {            
        _btnBackGourndClose.gameObject.SetActive(false);
        _popupToStringInScene[name].Disable();
        ClearPopUpStack();
    }

    public void ClearPopUpStack()
    {
        _popUpLlistStack.Pop();
        if (_popUpLlistStack.Count == 0)
        {
             _btnBackGourndClose.gameObject.SetActive(false);
             return;
        }
    }

    public void DisableTopPopUp()
    {
        try
        {
            _btnBackGourndClose.gameObject.SetActive(false);
            if (_popUpLlistStack.Count == 0)
            {
               // _btnBackGourndClose.gameObject.SetActive(false);
                return;
            }
            _popUpLlistStack.Pop().Disable();
            SetBackGroundHierarchy(_popUpLlistStack.Peek().obj.transform);
        }
        catch(System.Exception e)
        {
            //Debug.Log(e);
        }
    }

    private void CreateBackGourndBtn()
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/B_BackGround") as GameObject,transform);
        _btnBackGourndClose = obj.GetComponent<Button>();
        _btnBackGourndClose.onClick.AddListener(() => { DisableTopPopUp(); });
    }

}
