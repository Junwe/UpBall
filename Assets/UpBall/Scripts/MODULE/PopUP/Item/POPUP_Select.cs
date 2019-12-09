using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
enum PLAYERTYPE
{
    PLAYER01 = 0,
    PLAYER02 = 2
}
public class POPUP_Select : MonoBehaviour,IPopUp
{
     public TweenScale _openTween;

     public Image[] ImageList;
     public Sprite[] spriteList;

    private UnityAction<string> _startCallBack = (string value) => { };

    public UnityAction<string> StartCallBack
    {
        get { return _startCallBack;}
        set {_startCallBack = value;}
    }
    public GameObject obj
    {
        get { return gameObject;}
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);   // 팝업에 등록
        _startCallBack = (string msg) => { SetPlayer(string.Empty); };
             gameObject.SetActive(false);
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

    public void ClickPlayer(int num)
    {
        PlayerPrefs.SetInt("PlayerSelect",num);
        SetPlayer(string.Empty);
        PopUpManager.Instance.DisablePopUp(name);
    }

    private void SetPlayer(string msg)
    {
        int select = PlayerPrefs.GetInt("PlayerSelect",0);

        if(select == 0)
        {
            ImageList[GetIndex(PLAYERTYPE.PLAYER01)].sprite = GetSelectSprite((int)PLAYERTYPE.PLAYER01, true);
            ImageList[GetIndex(PLAYERTYPE.PLAYER02)].sprite = GetSelectSprite((int)PLAYERTYPE.PLAYER02, false);
        }
        else
        {
            ImageList[GetIndex(PLAYERTYPE.PLAYER01)].sprite = GetSelectSprite((int)PLAYERTYPE.PLAYER01, false);
            ImageList[GetIndex(PLAYERTYPE.PLAYER02)].sprite = GetSelectSprite((int)PLAYERTYPE.PLAYER02, true);
        }
    }


    private Sprite GetSelectSprite(int num,bool onoff)
    {
        return spriteList[num + (onoff ? 0 : 1)];
    }

    private int GetIndex(PLAYERTYPE type)
    {
        return (int)type - ((int)type / 2);
    }

}
