using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour,IPlayerUI
{
    private GameObject objTrailRender;

    private Coroutine _trailCrt;

    private GameObject _objGauge;
    private GameObject _objGaugeBG;

    GameObject IPlayerUI.objTrailRender
    {
        get
        {
            return objTrailRender;
        }
    }



    // Start is called before the first frame update
    void Awake()
    {
        objTrailRender = GameObject.Find("Trail");
        _objGauge = GameObject.Find("Gauge");
        _objGaugeBG = GameObject.Find("Gauge_BG");
        DisableTrail();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void DisableTrail()
    {
        objTrailRender.gameObject.SetActive(false);
    }

    public void Oncollision()
    {
        _trailCrt = StartCoroutine(Tween.instance.DelayMethod(0.3f, DisableTrail));

    }

    public void SetGage(float value)
    {
        if(value < 1.0f)
        {
            _objGaugeBG.gameObject.SetActive(true);
        }
        else
        {
            _objGaugeBG.gameObject.SetActive(false);
        }
        _objGauge.transform.localScale = new Vector3(value, 1f, 1f);
    }

    public void SetStateEvent(int mouseEvent)
    {
        throw new System.NotImplementedException();
    }

    public void Oncollision(Collision2D collision, BALLSTATE _ballState)
    {
        throw new System.NotImplementedException();
    }
}
