using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _movementID;

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

    private bool _isAlive = true;

   

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
        _movementID = Random.Range(-1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        switch (_movementID)
        {
            case 0:
                EnemeyMovement();
                break;
            default:
                DiagonalMovement(_movementID);
                break;
        }

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 4f);
            _canFire = Time.time + _fireRate;
            FireLaser();
            _fireSound.Play();
        }
       
    }
    public void FireLaser()
    {
        if(_isAlive == true)
        {
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            return;
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

    private void DiagonalMovement(int direct)
    {
        transform.Translate(new Vector3(direct,-1,0).normalized * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            _isAlive = false;
            _anim.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            _speed = 0.2f;
            _boxCollider.enabled = (false);
            _player.playerLaser.Remove(other.gameObject);
            Destroy(other.gameObject,1f);
            Destroy(this.gameObject,2.0f);
            
        }

        if (other.gameObject.tag == "Beam")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            _isAlive = false;
            _anim.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            _speed = 0.2f;
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
            _isAlive = false;
            _anim.SetTrigger("OnEnemyDeath");
            _explosionSound.Play();
            _boxCollider.enabled = (false);
            _speed = 0;
            Destroy(this.gameObject,2.8f);
            
        }
    }
}
