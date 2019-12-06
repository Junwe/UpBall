using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GROUNDTYPE
{
    Ground = 0,
    Flower,
    Ice,
    Lava,
    MAX
}

public enum GROUNDPOS
{
    left = 0,
    mid1 = 1,
    mid2 = 2,
    right = 3,
}
public class Wall : MonoBehaviour
{
    public GameObject[] arrWall;
    public BoxCollider2D collider2d;

    private bool _isScore = false; // score를 더해준 벽인지 체크하는 변수
    private bool _isUse = false;
    private float _moveSpeed = 0f;
    private int _count = 0;

    public bool IsUse
    {
        get
        {
            return _isUse;
        }

        set
        {
            _isUse = value;
        }
    }

    public bool IsScore
    {
        get
        {
            return _isScore;
        }

        set
        {
            _isScore = value;
        }
    }

    public int Count
    {
        get
        {
            return _count;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelingData.Instance.IsExit)
            return;
            
        if (IsUse)
        {
            transform.localPosition += new Vector3(0f, (LevelingData.Instance.info.moveSpeed * 1.5f) * Time.deltaTime, 0f);

            if (transform.localPosition.y <= -11.97f)
            {
                if (WallManager.instance.sprGround.color.a != 0f)
                {
                    WallManager.instance.HideGround();
                }
                ClearWall();
            }
        }
    }

    private void ClearWall()
    {
        WallManager.instance.DeleteWall(this);
        _count = 0;
        IsUse = false;
        IsScore = false;
        Disable();

        collider2d.enabled = false;
    }

    public void SetWallInfo(int Count, float MoveSpeed, Vector3 startPos, Dictionary<string, Sprite> sprtieDictionary)
    {
        _count = Count;
        _moveSpeed = MoveSpeed;
        transform.localPosition = startPos;

        SetWallObjCnt(Count);
        collider2d.enabled = true;

        SetSprtie(Count, sprtieDictionary);
    }

    private void SetSprtie(int count, Dictionary<string, Sprite> sprtieDictionary)
    {
        StringFinder finder = new StringFinder();
        List<string> keys = new List<string>(sprtieDictionary.Keys);
        List<string> ContatinsKeysToGroundTpye = finder.ContatinsToGroundTypeInList(keys, (GROUNDTYPE)LevelingData.Instance.nLevelCount);

        // 데이터 레벨과 맞는 groundtype를 사용
        if (count == 1)
        {
            string ContatinsKeysToGroundPos = finder.ContatinsToGroundPosInList(ContatinsKeysToGroundTpye, (GROUNDPOS)Random.Range((int)GROUNDPOS.mid1, (int)GROUNDPOS.mid2));
            arrWall[0].GetComponent<SpriteRenderer>().sprite = sprtieDictionary[ContatinsKeysToGroundPos];
        }
        else
        {
            string ContatinsKeysToGroundPos = string.Empty;
            for (int i = 0; i < count; ++i)
            {
                if (i == 0)
                {
                    ContatinsKeysToGroundPos = finder.ContatinsToGroundPosInList(ContatinsKeysToGroundTpye, GROUNDPOS.left);
                }
                else if (i == count - 1)
                {
                    ContatinsKeysToGroundPos = finder.ContatinsToGroundPosInList(ContatinsKeysToGroundTpye, GROUNDPOS.right);
                }
                else
                {
                    ContatinsKeysToGroundPos = finder.ContatinsToGroundPosInList(ContatinsKeysToGroundTpye, (GROUNDPOS)Random.Range((int)GROUNDPOS.mid1, (int)GROUNDPOS.mid2));
                }

                arrWall[i].GetComponent<SpriteRenderer>().sprite = sprtieDictionary[ContatinsKeysToGroundPos];
            }
        }
    }

    public void Disable()
    {
        for (int i = 0; i < arrWall.Length; ++i)
        {
            arrWall[i].gameObject.SetActive(false);
        }
    }

    private void SetWallObjCnt(int Count)
    {
        if (arrWall.Length <= Count)
        {
            return;
        }
        for (int i = 0; i < arrWall.Length; ++i)
        {
            arrWall[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Count; ++i)
        {
            arrWall[i].gameObject.SetActive(true);
        }

        collider2d.size = new Vector2(5f - (5 - Count) * 1, 0.8f);
        collider2d.offset = new Vector2(0 - (5 - Count) * 0.5f, -0.27f);
    }
}
