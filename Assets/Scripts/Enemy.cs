using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;

    

    private Player _player;
    private Animator _anim;
    private BoxCollider2D _boxCollider;

    [SerializeField]
    private AudioSource _explosionSound;
    [SerializeField]
    private AudioSource _fireSound;

    //Can Fire
    private float _canFire = -1f;
    private float _fireRate = 2f;

   

    void Start()
    {
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
        EnemeyMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 4f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            _fireSound.Play();
        }
       
    }

    void EnemeyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
            
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
            Destroy(this.gameObject,2.0f);
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
            Destroy(this.gameObject,2.8f);
        }
    }
}
