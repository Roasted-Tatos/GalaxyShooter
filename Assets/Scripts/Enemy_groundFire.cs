using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_groundFire : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private AudioSource _fireSound;


    private float _canFire = -1f;
    private float _fireRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BunkerFire()
    {
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(0.8f, 1f);
            _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            _fireSound.Play();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            BunkerFire();
        }

    }
}
