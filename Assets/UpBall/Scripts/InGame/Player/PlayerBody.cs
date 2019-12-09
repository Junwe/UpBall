using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BALLSTATE
{
    IDLE = 0,
    JUMP_ONCE,
    JUMPING_SLOWEND,
    JUMP_TWICE,
    NONEMOVEING,
}
public class PlayerBody : MonoBehaviour, IPlayer
{
    protected PlayerAnimation _playerAnimation;
    protected PlayerPhysical _playerPhysical;
    protected PlayerUI _playerUI;
    protected PlayerSlowMotin _playerSlowMotin;

    public GameObject objParticleStar; 

    IPlayerAnimation IPlayer.animation
    {
        get
        {
            return _playerAnimation;
        }
    }
    IPlayerPhysical IPlayer.physical
    {
        get
        {
            return _playerPhysical;
        }
    }
    IPlayerSlowMotin IPlayer.slowmotin
    {
        get
        {
            return _playerSlowMotin;
        }
    }
    IPlayerUI IPlayer.ui
    {
        get
        {
            return _playerUI;
        }
    }

    private PlayerInfo _info = new PlayerInfo();
    private bool _isCheck = false;
    private bool _isFirstTouch = true;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PlayerAnimation>();
        gameObject.AddComponent<PlayerPhysical>();
        gameObject.AddComponent<PlayerUI>();
        gameObject.AddComponent<PlayerSlowMotin>();

        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerPhysical = GetComponent<PlayerPhysical>();
        _playerUI = GetComponent<PlayerUI>();
        _playerSlowMotin = GetComponent<PlayerSlowMotin>();

        _playerAnimation.CreateParticleStar(objParticleStar);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.Instance.IsDie || LevelingData.Instance.IsExit)
        {
            return;
        }
        bool isTouchPossivle = true;
        bool _isDown = false;
        bool _isDrage = false;
        bool _isUp = false;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                if (EventSystem.current.IsPointerOverGameObject(0))
                {
                    isTouchPossivle = false;
                }
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    _isDown = true;
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    _isDrage = true;
                if (touch.phase == TouchPhase.Ended)
                    _isUp = true;
            }
        }
        else
        {
            if (EventSystem.current.IsPointerOverGameObject(-1))
            {
                isTouchPossivle = false;
            }
            if (Input.GetMouseButtonDown(0))
                _isDown = true;
            if (Input.GetMouseButton(0))
                _isDrage = true;
            if (Input.GetMouseButtonUp(0))
                _isUp = true;
        }

        if (isTouchPossivle)
        {
            if (_isDown)
            {
                TouchPower.instance.SetDownEvent(Input.mousePosition);
            }

            Time.fixedDeltaTime = Time.timeScale * .02f;
            if (_info._ballState == BALLSTATE.IDLE || _info._ballState == BALLSTATE.JUMP_ONCE)
            {
                if (_isDrage)
                {
                    _playerPhysical.SetMouseButton(TouchPower.instance.Direction, TouchPower.instance.MovePower);
                    SetStateEvent(1);
                }

                if (_isUp)
                {
                    TouchPower.instance.SetUpEvent(Input.mousePosition);
                    _playerPhysical.SetMouseButtonUp(TouchPower.instance.Direction, TouchPower.instance.MovePower);
                    SetStateEvent(0);
                }
            }
        }


        if (transform.position.y <= -10.79f) // die
        {
            ClearPlayer();
        }

        _playerUI.SetGage(_playerSlowMotin.SlowMotionValue);
        _playerPhysical.CheckUnderWall(_info._ballState, ref _isCheck);
    }



    private void SetStateEvent(int mouseEvent)
    {
        if (mouseEvent == 0)
        {
            _playerAnimation.SetStateEvent(mouseEvent);
            Sound.Instance.PlayEffSound(SOUND.S_JUMP);
            if (_info._ballState == BALLSTATE.IDLE)
            {
                _info._ballState = BALLSTATE.JUMP_ONCE;
                // 0.1초 딜레이동안 WALL과 충돌중이면 상태를 IDLE로 변경
                Invoke("SetStateIDLE", 0.1f);
            }
            else if (_info._ballState == BALLSTATE.JUMP_ONCE)
            {
                _info._ballState = BALLSTATE.JUMP_TWICE;
                if(_playerSlowMotin.SlowMotionValue <= 0f)
                    _info._ballState = BALLSTATE.NONEMOVEING;
            }

            if (_isFirstTouch)
            {
                _isFirstTouch = false;
                WallManager.instance.HideGround();
            }
        }
        else
        {
        }
        _playerSlowMotin.SetStateEvent(mouseEvent, _info);
    }

    private void SetStateIDLE()
    {
        if (_isCheck)
            _info._ballState = BALLSTATE.IDLE;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            _playerAnimation.PlayStarParticle(collision.contacts[0].point,_playerPhysical.CurrentMovePower,new Color(255f,255f,255f));
        }

        _playerPhysical.Oncollision(collision);
        _playerAnimation.Oncollision(collision);

        if (collision.gameObject.tag.Equals("Ground"))
        {
            if (transform.position.y > collision.transform.position.y)
            {
                _playerUI.Oncollision();
                _playerSlowMotin.Oncollision(collision);
                _info._ballState = BALLSTATE.IDLE;

            }
        }

        if(collision.gameObject.tag.Equals("Ground"))
        {
            Color color = new Color(255f, 255f, 255f);
            Wall colWall = collision.gameObject.GetComponent<Wall>();
            if (colWall != null)
            {
                if (colWall.GroundType == GROUNDTYPE.Ground)
                    color = new Color(255f, 0f, 0f);
                else if (colWall.GroundType == GROUNDTYPE.Flower)
                    color = new Color(0f, 200f, 0f);
                else if (colWall.GroundType == GROUNDTYPE.Ice)
                    color = new Color(255f, 255f, 255f);
                else if (colWall.GroundType == GROUNDTYPE.Lava)
                    color = new Color(255f, 0f, 0f);
            }
            _playerAnimation.PlayStarParticle(collision.contacts[0].point,_playerPhysical.CurrentMovePower,color);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            //_playerAnimation.Animator.SetTrigger("stop");

            //_playerUI.objTrailRender.gameObject.SetActive(true);
        }
    }

    private void ClearPlayer()
    {
        _playerSlowMotin.ClearPlayer();
        GetComponent<trajectory>().DisableTragectoryPonints();
        gameObject.SetActive(false);
        LevelingData.Instance.IsDie = true;
        UIManager.instance.ShakeCamera(0.05f, 0.5f);
        UIManager.instance.YouDied();
    }
}
