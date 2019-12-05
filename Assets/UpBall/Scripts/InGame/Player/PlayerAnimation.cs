using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    private Animator _animator;
    private Animation _animation; 

    private ParticleSystem _particleScatterStar;

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
        _particleScatterStar = GameObject.Find("Particle_ScatterStar").GetComponent<ParticleSystem>();
        _animator.SetTrigger("stop");
        _particleScatterStar.Stop();
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
            _particleScatterStar.Play();
        }
    }

    public void Oncollision(Collision2D collision, BALLSTATE _ballState)
    {
        _animator.SetTrigger("stop");
        _particleScatterStar.Stop();
    }
}
