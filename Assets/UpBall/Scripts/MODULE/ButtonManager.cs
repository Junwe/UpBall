using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    void Awake()
    {
        instance = null;
    }
    public void GmaeScene()
    {
        SceneManager.LoadScene("Game_Upball");
    }

    public void ClickMainOption()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Main");
    }

    public void ClickGameOption()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Pause");
    }

    public void ClickGameGuide()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Guide");
    }
}
