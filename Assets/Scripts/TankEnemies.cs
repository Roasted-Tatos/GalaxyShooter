using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 1f;

    private Player _player;
    [SerializeField]
    private Animator _anim;
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private AudioSource _explosionSound;
    [SerializeField]
    private GameObject _bigShot;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-8f,8f), 7, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        //Animator
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.Log("Anim not found");
        }
        _boxCollider = GetComponent<BoxCollider2D>();
        //Audio Reference
        _explosionSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if (transform.position.y <= -6f)
        {
            //transform.position = new Vector3(Random.Range(8f, -8f),7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.GetPoints(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            _speed = 0.2f;
            _boxCollider.enabled = (false);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 1f);
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
            _anim.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            _boxCollider.enabled = (false);
            _speed = 0;
            Destroy(this.gameObject, 1f);
        }
    }
}
