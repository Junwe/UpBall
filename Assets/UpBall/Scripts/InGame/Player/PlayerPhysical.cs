using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 플레이에어 물리부분을 담당하는 클래스.
/// </summary>
public class PlayerPhysical : MonoBehaviour, IPlayerPhysical
{
    public Rigidbody2D myRigidbody;
    Rigidbody2D IPlayerPhysical.myRigidbody
    {
        get
        {
            return myRigidbody;
        }
    }

    float _currentMovePower;
    Vector2 _currentMovePowerVector;
    private Vector3 _trajectyVelocity;

    private GameObject parentMoveObj;
    private GameObject parentObj;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        parentMoveObj = GameObject.Find("G_InGameObject");
        parentObj = GameObject.Find("G_Ball");
    }
    private Vector3 _pausedVelocity;
    private float _pausedAngularVelocity;
    public void Pause()
    {
        _pausedVelocity = myRigidbody.velocity;
        _pausedAngularVelocity = myRigidbody.angularVelocity;
        myRigidbody.gravityScale = 0f;
        myRigidbody.Sleep();
    }

    public void Resume()
    {
        myRigidbody.WakeUp();
        myRigidbody.gravityScale = 1f;
        myRigidbody.velocity = _pausedVelocity;
        myRigidbody.angularVelocity = _pausedAngularVelocity;
    }
    private bool _isPauseInfoSave = false;
    // Update is called once per frame
    void Update()
    {
        if (LevelingData.IsExit)
        {
            if(_isPauseInfoSave == false)
            {
                Pause();
                _isPauseInfoSave = true;
            }
        }
        else
        {
            if(_isPauseInfoSave)
            {
                Resume();
                _isPauseInfoSave = false;
            }
        }
        // 1보다 크면 오른쪽보고 있음
        if (myRigidbody.velocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 1f);
        }
        // 작으면 왼쪽
        else if (myRigidbody.velocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 1f);
        }

        _currentMovePower = myRigidbody.velocity.magnitude;
        _currentMovePowerVector = myRigidbody.velocity;
    }

    public void SetMouseButton(Vector3 direction, float movePower)
    {
        TouchPower.instance.SetStayEvent(Input.mousePosition);
        if (TouchPower.instance.MovePower != 0.0f)
        {
            GetComponent<trajectory>().Pressed(transform.position, direction * movePower);
        }
    }

    public void SetMouseButtonUp(Vector3 direction, float movePower)
    {
        TouchPower.instance.SetUpEvent(Input.mousePosition);
        // 점프할때는 무조건 처음 속도 0
        if (movePower != 0f)
        {
            TouchPower.instance.SetTouchPowerInfo(Input.mousePosition);

            if (myRigidbody.velocity != Vector2.zero)
                myRigidbody.velocity = Vector2.zero;

            myRigidbody.AddForce(direction * movePower, ForceMode2D.Impulse);
            _trajectyVelocity = GetComponent<trajectory>().temp;
        }
    }

    public void Oncollision(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            if (_currentMovePower >= 8.0f)
                UIManager.instance.ShakeCamera(0.05f, 0.1f);

            _currentMovePower -= 2f;
            Vector3 incomingVector = _currentMovePowerVector;//transform.position - _startMovePos;  //입사각
                                                             //incomingVector = incomingVector.normalized * _currentMovePower;
            Vector3 inverseVector = -incomingVector; //입사각의 반대각

            Vector3 normalVector = collision.contacts[0].normal; //법선벡터

            Vector3 reflectVector = Vector3.Reflect(incomingVector, normalVector); //반사각

            myRigidbody.velocity = Vector2.zero;

            if (_trajectyVelocity != Vector3.zero) // 궤적이 있을경우에는 궤적 방향으로
                myRigidbody.AddForce(_trajectyVelocity, ForceMode2D.Impulse);
            else
                myRigidbody.AddForce(reflectVector, ForceMode2D.Impulse);

            GetComponent<trajectory>().temp = Vector3.zero;
            _trajectyVelocity = Vector3.zero;
        }

        if (collision.gameObject.tag.Equals("Ground"))
        {
            if (_currentMovePower >= 8.0f)
                UIManager.instance.ShakeCamera(0.025f, 0.1f);

            _trajectyVelocity = Vector3.zero;

            if (transform.position.y > collision.transform.position.y)
            {
                if (myRigidbody.velocity != Vector2.zero)
                    myRigidbody.velocity = Vector2.zero;
            }
        }
    }

    public void CheckUnderWall(BALLSTATE _ballState, ref bool _isCheck)
    {
        float LengthY = 0.23f;
        float LengthX = 0.7f;
        Vector2 MyPos = transform.position;
        bool isCheck = false;

        for (int i = -1; i <= 1; ++i)
        {
            // RayCast 확인.. 방향은 밑으로
            Ray2D ray = new Ray2D(MyPos + new Vector2((LengthX / 2f) * i, 0f), Vector2.down);
            RaycastHit2D Hit = Physics2D.Raycast(ray.origin, ray.direction, LengthY);
            Debug.DrawRay(ray.origin, LengthY * ray.direction, Color.blue, 1);

            if (Hit.collider != null && Hit.collider.gameObject.tag.Equals("Ground"))
            {
                Wall wall = Hit.collider.gameObject.transform.GetComponent<Wall>();
                // 충돌중인 오브젝트가 wall 스크립트를 가지고 있을때
                if (wall != null)
                {
                    // 이미 점수 체크한 벽인지 확인후 점수를 올려줌
                    if (wall.IsScore == false)
                    {
                        UIManager.instance.PlusScroe();
                        wall.IsScore = true;
                        WallManager.instance.CheckWllScore(wall);
                    }
                    // Ball도 Wall과 같이 내려가게 자식 오브젝트로 바꿔줌
                    if (_ballState == BALLSTATE.IDLE)
                    {
                        parentObj.transform.SetParent(Hit.collider.gameObject.transform);
                        isCheck = true;
                        myRigidbody.velocity = Vector2.zero;
                    }
                }
            }
        }
        // Wall에 없으면 Ball은 같이 내려가지 않아도됨
        if (isCheck == false)
        {
            if(parentObj != null)
            {
                parentObj.transform.SetParent(parentMoveObj.transform);
                _isCheck = false;
            }
        }
        else
        {
            _isCheck = true;
        }
    }


}
