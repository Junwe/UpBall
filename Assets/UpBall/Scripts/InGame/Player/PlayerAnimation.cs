using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    private Animator _animator;
    private Animation _animation;  

    Animator IPlayerAnimation.Animator
    {
        get
        {
            return _animator;
        }
    }

    Animation IPlayerAnimation.Animation
    {
        get
        {
            return _animation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animation = GetComponent<Animation>();
        _animator.SetTrigger("stop");
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.IsDie || LevelingData.IsExit)
        {
         _animator.enabled = false;
        }
        else
        {
         _animator.enabled = true;

        }
    }

    public void SetStateEvent(int mouseEvent)
    {
        if (mouseEvent == 0) // up
        {
            _animator.SetTrigger("jump");
        }
    }

    public void Oncollision(Collision2D collision, BALLSTATE _ballState)
    {
        _animator.SetTrigger("stop");
    }
}
