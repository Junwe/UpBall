using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class POPUP_Main : POPUP_Base,IPopUp
{
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
