using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMines_Behavior : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 25f;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private Animator _explosionAnim;

    [SerializeField]
    private float _DetectionArea =4f;

    [SerializeField]
    private AudioSource _explosionSound;
    [SerializeField]
    private Player _player;
    private CircleCollider2D _collider;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _collider = GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (_player == null)
        {
            Destroy(this.gameObject);
        }
        Ramming();
        
    }

    private void Ramming()
    {
        
        if(Vector3.Distance(transform.position,_player.transform.position)<_DetectionArea)
        { 
             transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_DetectionArea);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            _explosionAnim.SetBool("Explosion", true);
            _explosionSound.Play();
            _collider.enabled = false;
            _player.playerLaser.Remove(other.gameObject);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.3f);

        }
        if (other.gameObject.tag == "Beam")
        {
            if (_player != null)
            {
                _player.GetPoints(100);
            }
            _explosionAnim.SetBool("Explosion", true);
            _explosionSound.Play();
            _collider.enabled = false;
            Destroy(this.gameObject, 2.3f);

        }

        if (other.gameObject.tag == "Player")
        {
            _explosionAnim.SetBool("Explosion", true);
            _explosionSound.Play();
            _collider.enabled = false;
            _player.Damage();
            Destroy(this.gameObject, 2.3f);
        }

    }
}
