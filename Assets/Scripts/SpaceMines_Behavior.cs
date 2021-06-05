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
    private AudioSource _explosionSound;

    private Player _player;
    private CircleCollider2D _collider;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player not found");
        }

        _collider = GetComponent<CircleCollider2D>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            _explosionAnim.SetBool("Explosion", true);
            _explosionSound.Play();
            _collider.enabled = false;
            Destroy(other.gameObject);
            Destroy(this.gameObject, 2.3f);

        }
        if (other.gameObject.tag == "Beam")
        {
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
