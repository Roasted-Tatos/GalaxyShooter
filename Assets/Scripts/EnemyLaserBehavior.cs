using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private AudioSource _audioSource;
 
  
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
         if (transform.position.y < -8)
        {
              if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
             Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            //If player is found the Damage component from Player
            if (player != null)
            {
                player.Damage();
            }
            _audioSource.Play();
            Destroy(this.gameObject);
        }
    }
}
