using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class POPUP_SystemMsg : POPUpHasAction, IPopUp
{    
    [SerializeField]
    private Text _textMsg;

    new void Awake()
    {
        base.Awake();
        SetAction(new StartAction(SetTextMsg));
    }
    
    public void SetTextMsg(object msg)
    {
        _textMsg.text = msg.ToString();
    }
}
