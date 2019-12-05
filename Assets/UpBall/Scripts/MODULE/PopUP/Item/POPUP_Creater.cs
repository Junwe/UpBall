using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POPUP_Creater : MonoBehaviour, IPopUp
{    
    public TweenScale _openTween;

    public GameObject obj
    {
        get { return gameObject;}
    }
    void Awake()
    {
        PopUpManager.Instance.AddPop(gameObject.name, this);
        gameObject.SetActive(false);
    }
    public void Enable()
    {
        _openTween.Time = 0.25f;
        gameObject.SetActive(true);
        _openTween.StartTween();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Game_Upball"))
        {
            LevelingData.IsExit = true;
        }
    }
 
    public void Disable()
    {
        _openTween.Time = 0.25f;
        _openTween.ReversePlay();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Game_Upball"))
        {
            LevelingData.IsExit = false;
            PlayerPrefs.SetInt("FistGuide", 1);
        }
    }
}
