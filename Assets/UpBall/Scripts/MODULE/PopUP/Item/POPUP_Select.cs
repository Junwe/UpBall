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
    public List<SelectItem> listSelectItmes;
    
    // Start is called before the first frame update
    public void Awake()
    {
        base.Awake();
        SetAction(new StartAction(SetPlayer));
    }

    public void ClickPlayer(int num)
    {
        PlayerPrefs.SetInt("PlayerSelect",num);
        SetPlayer();
        //PopUpManager.Instance.DisablePopUp(name);
    }

    private void SetPlayer()
    {
        int select = PlayerPrefs.GetInt("PlayerSelect",0);
        listSelectItmes.ForEach(item => { item.SetSelect(false); });
        listSelectItmes[select].SetSelect(true);

    }
}
