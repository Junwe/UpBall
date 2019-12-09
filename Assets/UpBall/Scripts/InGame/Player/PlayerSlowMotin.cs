using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlowMotin : MonoBehaviour, IPlayerSlowMotin
{
    private GameObject objSlowDuration;

    private bool _isSlowing = false;        // 슬로우 중인지 체크
    private float _SlowDurationCount = 0f;  // 슬로우 시간 체크
    private float _slowScale = 0.15f;
    private float _slowMotionValue = 1f;

    private Coroutine _slowCrt = null;

    public float SlowMotionValue
    {
        get { return _slowMotionValue; }
    }

    GameObject IPlayerSlowMotin.objSlowDuration
    {
        get
        { return objSlowDuration; }
    }

    // Start is called before the first frame update
    void Start()
    {
        objSlowDuration = GameObject.Find("Ball_Dr");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // direction : true +, flase -
    IEnumerator SetSlowDurtion(bool direction,PlayerInfo info)
    {
        _SlowDurationCount = 0f;

        _slowMotionValue = 1f;

        while (_SlowDurationCount < 1.0f)
        {
            _SlowDurationCount = Mathf.Clamp01(_SlowDurationCount + Time.deltaTime / LevelingData.Instance.info.slowDurationTime);
            if (direction)
            {
                objSlowDuration.transform.localScale = new Vector3(1.0f * _SlowDurationCount, 1.0f * _SlowDurationCount, 0f);
                _slowMotionValue = 1.0f - (1.0f * _SlowDurationCount);
            }
            yield return null;
        }
        info._ballState = BALLSTATE.NONEMOVEING;
        _slowMotionValue = 0f;
        ClearSlowMotion();

        TouchPower.instance.objStart.SetActive(false);
        TouchPower.instance.objEnd.SetActive(false);
    }

    private void SetSlowMotionTime(PlayerInfo info)
    {
        _isSlowing = true;

        Time.timeScale = _slowScale;
        LevelingData.Instance.curTimeScale = _slowScale;
        Sound.Instance.SetPlaySpeed(0.7f);
        _slowCrt = StartCoroutine(SetSlowDurtion(true, info));
    }

    private void ClearSlowMotion()
    {
        _isSlowing = false;
        Time.timeScale = 1.0f;
        LevelingData.Instance.curTimeScale = 1.0f;
        Sound.Instance.SetPlaySpeed(1.0f);
        objSlowDuration.transform.localScale = Vector3.zero;
        DisableDurationEdge();
        GetComponent<trajectory>().DisableTragectoryPonints();

        if (_slowCrt != null)
        {
            StopCoroutine(_slowCrt);
            _slowCrt = null;
        }
    }

    private void DisableDurationEdge()
    {
        _slowMotionValue = 1f;
    }

    public void SetStateEvent(int mouseEvent, PlayerInfo _info)
    {
        if (mouseEvent == 0) // up
        {
            DisableDurationEdge();
            ClearSlowMotion();
        }
        else
        {
            if (_info._ballState == BALLSTATE.JUMP_ONCE && _isSlowing == false)
            {
                SetSlowMotionTime(_info);
            }
        }
    }

    public void Oncollision(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            ClearSlowMotion();
        }
    }

    public void ClearPlayer()
    {
        ClearSlowMotion();
        DisableDurationEdge();
    }
}
