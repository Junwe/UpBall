using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    protected Animator _animator;
    private Animation _animation; 

    protected ParticleSystem _particleScatterStar;
    protected ParticleSystem _particleStar;

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
        _particleStar = GameObject.Find("Particle_Star").GetComponent<ParticleSystem>();
        _animator.SetTrigger("stop");
        _particleScatterStar.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelingData.Instance.IsDie || LevelingData.Instance.IsExit)
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
            _animator.Play("Jump");
            _particleScatterStar.Play();
        }
    }

    public void Oncollision(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            _animator.Play("idle");
            _particleScatterStar.Stop();
            _particleStar.Stop();
        }
        if (collision.gameObject.tag.Equals("Wall"))
        {

        }
    }

    public void PlayStarParticle(Vector2 point,float movepower,Color color)
    {
        if (movepower >= 8.0f)
        {
            _particleStar.transform.position = new Vector3(point.x, point.y, -6.36f);
            ParticleSystem.MainModule ps = _particleStar.main;
            ps.startColor = color;
            _particleStar.Play();
        }
    }
}
