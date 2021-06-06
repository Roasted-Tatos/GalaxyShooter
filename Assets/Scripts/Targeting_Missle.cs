using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting_Missle : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float _speed = 3f;
    [SerializeField]
    private BoxCollider2D _collider;

    [SerializeField]
    private Animator _explosion;
    [SerializeField]
    private AudioSource _explosionSound;

    private bool _isAlive = true;
    private BoxCollider2D _boxCollider;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        player = GameObject.Find("Player").transform.GetComponent<Transform>();
        _explosion = GetComponent<Animator>();
        _explosionSound = GetComponent<AudioSource>();
        //_collider.enabled = true;
        _collider.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        moveMissle(movement);
    }
    private void moveMissle(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * _speed * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }

            _explosion.SetTrigger("OnImpact");
            _explosionSound.Play();
            _speed = 0f;
            _collider.enabled = (false);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.0f);

        }
        if (other.gameObject.tag == "Beam")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }

            _explosion.SetTrigger("OnImpact");
            _explosionSound.Play();
            _speed = 0.2f;
            _collider.enabled = false;
            _boxCollider.enabled = (false);
            Destroy(this.gameObject, 2.0f);

        }

        if (other.gameObject.tag == "Player")
        {
            //Storing the player component in a variable
            Player player = other.transform.GetComponent<Player>();

            //If player is found the Damage component from Player
            if (player != null)
            {
                player.Damage();
            }

            _explosion.SetTrigger("OnImpact");
            _explosionSound.Play();
            _collider.enabled = (false);
            _speed = 0;
            Destroy(this.gameObject, 2.8f);

        }
    }
}
