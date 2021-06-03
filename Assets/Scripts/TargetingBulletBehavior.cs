using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float _movementspeed = 7f;
    [SerializeField]
    private AudioSource _dmgSound;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Animator _Explosion;

    private Rigidbody2D _rb;
    private Player _target;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").GetComponent<Player>();
        _dmgSound = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _Explosion = GetComponent<Animator>();
        moveDirection = (_target.transform.position - transform.position).normalized * _movementspeed;
        _rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(this.gameObject, 4f);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _target.Damage();
            _dmgSound.Play();
            _spriteRenderer.enabled = false;
            _Explosion.SetTrigger("OnImpact");
            Destroy(this.gameObject,3f);
        }
        
    }

}
