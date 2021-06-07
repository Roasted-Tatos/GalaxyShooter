using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _laserPrefab2;
    [SerializeField]
    private AudioSource _laserSound;

    [SerializeField]
    private float _health = 100f;
    [SerializeField]
    private GameObject _explosion01,_explosion02,_explosion03;
   


    // Start is called before the first frame update
    void Start()
    {
        _laserPrefab.SetActive(false);
        _laserPrefab2.SetActive(false);
        _explosion01.SetActive(false);
        _explosion02.SetActive(false);
        _explosion03.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(LaserPowerDownRoutine());
        }
        if (other.gameObject.tag == "Beam")
        {
            _health -= 5;
            _explosion01.SetActive(true);
            if(_health <= 50)
            {
                _explosion02.SetActive(true);
            }
            if(_health <=20)
            {
                _explosion03.SetActive(true);
            }
            if(_health <=0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    IEnumerator LaserPowerDownRoutine()
    {
        _laserPrefab.SetActive(true);
        _laserPrefab2.SetActive(true);
        _laserSound.Play();
        yield return new WaitForSeconds(4f);
        _laserPrefab.SetActive(false);
        _laserPrefab2.SetActive(false);
        _laserSound.Pause();
    }

  
   
}
