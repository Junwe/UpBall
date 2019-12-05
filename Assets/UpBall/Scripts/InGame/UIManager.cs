using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public Text txtScore;

    public Image imgExit;
    public Image imgDie;
    public Text txtDie;
    public Image imgDieBG;
    public Text txtBestScore;

    public RectTransform objDieUI;
    public RectTransform objDownUI;

    public TweenAlpha[] _dieTweenListAlpha;
    public TweenMove[] _dieTweenListMove;
    public TweenScale[] _dieTweenListScale;

    private int _score = 0;
    private bool _ExitToggle = false;

    private List<ITween> _dieTweenList = new List<ITween>();



    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
        }
    }

    private void Awake()
    {
        instance = this;
        LevelingData.IsExit = false;
        LevelingData.IsDie = false;
    }
    // Use this for initialization
    void Start()
    {
        objDieUI.gameObject.SetActive(false);
        foreach (var i in _dieTweenListAlpha)
            _dieTweenList.Add(i);
        foreach (var i in _dieTweenListMove)
            _dieTweenList.Add(i);
        foreach (var i in _dieTweenListScale)
            _dieTweenList.Add(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (LevelingData.IsExit == false)
            {
                PopUpManager.Instance.EnablePopUp("I_PopUp_Pause");
            }
            else
            {
                PopUpManager.Instance.DisablePopUp("I_PopUp_Pause");
            }
        }
    }
    
    public void PlusScroe()
    {
        _score++;
        txtScore.text = (_score * 10 ).ToString();
        if (_score % 10 == 0)
        {
            LevelingData.SetNextLevel(_score);
        }
    }

    public void YouDied()
    {
        if (_score > GetBestScore())
            SetBestScore(_score);
        int score = GetBestScore() * 10;
        txtBestScore.text = "BEST " + score.ToString();

        Sound.Instance.PlayEffSound(SOUND.S_DIE);

        //TouchPower.instance.objStart.SetActive(false);
        //TouchPower.instance.objEnd.SetActive(false);

        objDieUI.gameObject.SetActive(true);

        foreach(var i in _dieTweenList)
        {
            i.StartTween();
        }
    }

    public void ClickReTry()
    {
        LevelingData.ReSetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ClickHome()
    {
        LevelingData.ReSetData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ShakeCamera(float power, float time)
    {
        StartCoroutine(ShakeCamera(power, time, Camera.main.transform.position));
    }

    private IEnumerator ShakeCamera(float power, float time, Vector3 originPos)
    {
        float t = 0f;
        while (t <= time)
        {
            t += Time.unscaledDeltaTime;
            Camera.main.transform.position = (Vector3)Random.insideUnitCircle * power + originPos;
            yield return null;
        }
        Camera.main.transform.position = originPos;
    }

    private int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore", 0);
    }

    private void SetBestScore(int score)
    {
        PlayerPrefs.SetInt("BestScore", score);
    }

    public void ExitGameYes()
    {
        Application.Quit();
    }

    public void ExitGameNo()
    {
        imgExit.gameObject.SetActive(false);
        Time.timeScale = LevelingData.curTimeScale;
        LevelingData.IsExit = false;
    }

    public void ClickOption()
    {
        PopUpManager.Instance.EnablePopUp("I_PopUp_Pause");
    }
}