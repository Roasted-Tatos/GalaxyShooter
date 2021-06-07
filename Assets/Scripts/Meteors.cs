using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteors : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private AudioSource _dmgSound;
    [SerializeField]
    private AudioSource _explosionSound;
    [SerializeField]
    private float _health = 10f;
    // Start is called before the first frame update
    void Start()
    {
        _dmgSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            //If player is found the Damage component from Player
            if (player != null)
            {
                player.Damage();
                Instantiate(_explosion, transform.position, Quaternion.identity);
                _dmgSound.Play();
            }
        }
        if(other.gameObject.tag == "Laser")
        {
            _health -= 2;
            Destroy(other.gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _dmgSound.Play();
            if (_health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if(other.gameObject.tag == "Beam")
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _explosionSound.Play();
            Destroy(this.gameObject,0.5f);
        }
    }
}
