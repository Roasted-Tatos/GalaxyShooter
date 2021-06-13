using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingEnemy : MonoBehaviour
{
    [SerializeField]
    private float _safetyDistance = 4f;
    [SerializeField]
    private float _dodgeSpeed = 5f;

    [SerializeField]
    private float _speed = 5f;

    private Player _player;
    private BoxCollider2D _collider;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _bulletSpread;

    private float _canFire = -1f;
    private float _fireRate = 2f;
    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private AudioClip _explosionSound;


    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
        EnemeyMovement();
        Dodging();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 4f);
            _canFire = Time.time + _fireRate;
            FireLaser();
        }
        if (_player == null)
        {
            PlayerisDead();
            Debug.Log("Stop Shooting damn it");
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

    private void FireLaser()
    {
        if(_isAlive == true)
        {
            for ( int fireAngle = 0; fireAngle < 360; fireAngle += 30)
            {
                var newBullet = Instantiate(_bulletSpread, transform.position, Quaternion.identity);
                newBullet.transform.eulerAngles = Vector3.forward * fireAngle;
            }
        }
    }

    private void PlayerisDead()
    {
        _isAlive = false;
    }

    private void Dodging()
    {
        Vector3 laserPosition = new Vector3(0, 0, 0);
        if (_player.playerLaser.Count != 0)
        {
            foreach (GameObject laser in _player.playerLaser)
            {
                laserPosition = laser.transform.position;
            }
            if (Vector3.Distance(transform.position, laserPosition) <= _safetyDistance)
            {
                if (laserPosition.x > transform.position.x)
                {
                    transform.Translate(Vector3.left * _dodgeSpeed * Time.deltaTime);
                }
                if (laserPosition.x < transform.position.x)
                {
                    transform.Translate(Vector3.right * _dodgeSpeed * Time.deltaTime);
                }
            }
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _safetyDistance);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            Instantiate(_explosion, transform.position, Quaternion.identity);
            
            _speed = 0f;
            _isAlive = false;
            _collider.enabled = (false);
            _player.playerLaser.Remove(other.gameObject);
            AudioSource.PlayClipAtPoint(_explosionSound, new Vector3(0, 0, -10));
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            

        }
        if (other.gameObject.tag == "Beam")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            Instantiate(_explosion, transform.position, Quaternion.identity);
            //_explosionSound.Play();
            _speed = 0f;
            _isAlive = false;
            _collider.enabled = (false);
            AudioSource.PlayClipAtPoint(_explosionSound, new Vector3(0, 0, -10));
            Destroy(this.gameObject);

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

            Instantiate(_explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSound, new Vector3(0, 0, -10));
            _collider.enabled = (false);
            _speed = 0;
            _isAlive = false;
            
            Destroy(this.gameObject, 0.1f);

        }
    }
}