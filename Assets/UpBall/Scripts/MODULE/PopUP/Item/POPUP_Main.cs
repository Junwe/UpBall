using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POPUP_Main : MonoBehaviour,IPopUp
{
    [SerializeField]
    private TweenScale _openTween;
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

    }
    
    public void ClickCalnle()
    {
        PopUpManager.Instance.DisablePopUp(name);
    }

}
