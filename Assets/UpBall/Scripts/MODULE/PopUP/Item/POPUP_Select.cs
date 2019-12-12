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
public class POPUP_Select : POPUpHasAction ,IPopUp
{
     public Image[] ImageList;
     public Sprite[] spriteList;
    
    // Start is called before the first frame update
    public void Awake()
    {
        base.Awake();
        SetAction(new StartAction(SetPlayer));
    }

    public void ClickPlayer(int num)
    {
        PlayerPrefs.SetInt("PlayerSelect",num);
        PopUpManager.Instance.DisablePopUp(name);
    }

    private void SetPlayer()
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
