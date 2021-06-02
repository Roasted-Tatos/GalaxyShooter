using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingBulletBehavior : MonoBehaviour
{
    [SerializeField]
    private float _movementspeed = 7f;
    [SerializeField]
    private AudioSource _dmgSound;

    private Rigidbody2D _rb;
    private Player _target;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.Find("Player").GetComponent<Player>();
        _dmgSound = GetComponent<AudioSource>();
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
            Destroy(this.gameObject,0.8f);
        }
        
    }

}
