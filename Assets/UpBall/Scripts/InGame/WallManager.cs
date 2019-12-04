﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WallManager : MonoBehaviour
{
    public static WallManager instance = null;
    public GameObject pfWall;
    public SpriteRenderer sprGround;

    private List<Wall> ListWall = new List<Wall>();
    private List<Wall> _currentUseWallList = new List<Wall>();

    private float _createCount = 3f;

    public GameObject objLeftWall;
    public GameObject objRightWall;

    private Dictionary<string, Sprite> _spriteToStringDictionary = new Dictionary<string, Sprite>();
    [SerializeField]
    private Sprite[] sprWalls;

    private void Awake()
    {
        instance = this;
        PlayerPrefs.SetInt("IsFirst", 1);
    }

    // Use this for initialization
    void Start()
    {

        _createCount = LevelingData.wallCreateTime;
        for (int i = 0; i < 10; ++i)
        {
            GameObject obj = Instantiate(pfWall);

            obj.transform.parent = gameObject.transform;
            obj.transform.localPosition = new Vector3(100f, 100f, 0f);
            ListWall.Add(obj.GetComponent<Wall>());
        }

        foreach (Sprite s in sprWalls)
        {
            _spriteToStringDictionary.Add(s.name, s);
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (LevelingData.IsDie || LevelingData.IsExit)
            return;

        _createCount += Time.deltaTime;

        if (_createCount >= LevelingData.wallCreateTime)
        {
            int count = Random.Range(LevelingData.minBlockCnt, LevelingData.maxBlockCnt);
            Wall curWall = GetUseWall();

            curWall.SetWallInfo(count, LevelingData.moveSpeed * 1.5f,
                new Vector3(Random.Range(GetWallMinPositionX(count), GetWallMaxPositionX(count)), 9.68f, 0f), _spriteToStringDictionary);
            _currentUseWallList.Add(curWall);

            _createCount = 0f;
        }
    }

    public void DeleteWall(Wall deleteWall)
    {
        _currentUseWallList.Remove(deleteWall);
    }

    public void CheckWllScore(Wall scoreWall)
    {
        int idx = _currentUseWallList.IndexOf(scoreWall);
        for (int i = idx; i >= 0; --i)
        {
            if (_currentUseWallList[i].IsScore == false)
            {
                UIManager.instance.PlusScroe();
                _currentUseWallList[i].IsScore = true;
            }
        }
    }

    private float GetWallMinPositionX(int count)
    {
        return -2.6f;
    }
    private float GetWallMaxPositionX(int count)
    {
        return (2.6f - (5 - count) * 1f);
    }

    public void HideGround()
    {
        sprGround.GetComponent<ITween>().StartTween();
        //StartCoroutine(Tween.instance.SetAlpha(sprGround, 1f, 0f, 1.0f));
        sprGround.GetComponent<BoxCollider2D>().enabled = false;
    }

    private Wall GetUseWall()
    {
        for (int i = 0; i < ListWall.Count; ++i)
        {
            if (ListWall[i].IsUse == false)
            {
                ListWall[i].IsUse = true;
                return ListWall[i];
            }
        }

        return null;
    }

    public bool IsLeftWallCol(Vector3 pos)
    {
        if ((pos.x + 0.25f) >= objLeftWall.transform.position.x - 0.5f &&
            (pos.x - 0.25f) <= objLeftWall.transform.position.x + 0.5f &&
            pos.y >= objLeftWall.transform.position.y - 11.205f &&
            pos.y <= objLeftWall.transform.position.y + 11.205f)
        {
            return true;
        }
        return false;
    }

    public bool IsRightWallCol(Vector3 pos)
    {
        if ((pos.x + 0.25f) >= objRightWall.transform.position.x - 0.5f &&
          (pos.x - 0.25f) <= objRightWall.transform.position.x + 0.5f &&
           pos.y >= objRightWall.transform.position.y - 11.205f &&
           pos.y <= objRightWall.transform.position.y + 11.205f)
        {
            return true;
        }
        return false;
    }
}
