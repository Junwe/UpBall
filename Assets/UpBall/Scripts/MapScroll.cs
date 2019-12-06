using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 무한맵 스크롤.
/// </summary>

public class MapScroll : MonoBehaviour
{
    public GameObject[] objBG;
    public float speed;
    public float LimitY;
    public float DisntaceY;

    private GameObject _objBottomMove;
    private GameObject _objTopMove;

    // Start is called before the first frame update
    void Start()
    {
        _objBottomMove = objBG[0];
        _objTopMove = objBG[1];
    }

    private void SwapObj()
    {
        GameObject temp = _objBottomMove;
        _objBottomMove = _objTopMove;
        _objTopMove = temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.Instance.IsExit)
            return;

        _objBottomMove.transform.localPosition += new Vector3(0f, LevelingData.Instance.info.moveSpeed * Time.deltaTime, 0f);
        _objTopMove.transform.localPosition = new Vector3(0f, _objBottomMove.transform.localPosition.y + DisntaceY, 1f);

        if (_objBottomMove.transform.localPosition.y <= LimitY)
        {
            _objBottomMove.transform.localPosition = new Vector3(0f, _objTopMove.transform.localPosition.y + DisntaceY, 1f);
            SwapObj();
        }
    }
}

