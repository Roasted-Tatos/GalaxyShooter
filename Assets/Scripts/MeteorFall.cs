using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorFall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int _movementID;
    [SerializeField]
    private AudioSource _explosionSound;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
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
        transform.Translate(new Vector3(direct, -1, 0).normalized * _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            _player.playerLaser.Remove(other.gameObject);
            _explosionSound.Play();
        }
        if(other.gameObject.tag == "Laser")
        {
            _player.playerLaser.Remove(other.gameObject);
            _explosionSound.Play();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
