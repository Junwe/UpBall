using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
     public string _EffString;
    public string _BgmStinrg;

    public TweenMove  _effTween;
    public TweenMove  _bgmTween;
    public Text  _effText;
    public Text  _bgmOfftext;

    private Dictionary<string,TweenMove> _SoundTweenToString = new Dictionary<string, TweenMove>();

    private Dictionary<string,Text>      _soundTextToString = new Dictionary<string, Text>(); 
    // Start is called before the first frame update
    void Start()
    {
        _SoundTweenToString.Add(_EffString, _effTween);
        _SoundTweenToString.Add(_BgmStinrg, _bgmTween);

        _soundTextToString.Add(_EffString, _effText);
        _soundTextToString.Add(_BgmStinrg, _bgmOfftext);

        foreach(var s in _soundTextToString)
        {
            if(PlayerPrefs.GetInt(s.Key,0) == 1)
            {
                _SoundTweenToString[s.Key].gameObject.transform.localPosition = new Vector3(267f,8f,0f);
                _soundTextToString[s.Key].text = "ON";
            }
            else
            {
                _SoundTweenToString[s.Key].gameObject.transform.localPosition = new Vector3(423f,8f,0f);
                _soundTextToString[s.Key].text = "OFF";
            }
        }
    }

    public void ClickSoundBtn(string btnText)
    {
        // 0 : on ,,, on -> off로 가는 상황
        if (PlayerPrefs.GetInt(btnText, 0) == 1)
        {
            _SoundTweenToString[btnText].ReversePlay();
            _soundTextToString[btnText].text = "OFF";
            Sound.Instance.SetMute(0, btnText);
        }
        else
        {
            _SoundTweenToString[btnText].StartTween();
            _soundTextToString[btnText].text = "On";
            Sound.Instance.SetMute(1, btnText);
        }
    }
}
