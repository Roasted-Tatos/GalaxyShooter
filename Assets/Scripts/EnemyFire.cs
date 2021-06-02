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

    // Start is called before the first frame update
    void Start()
    {
        _laserPrefab.SetActive(false);
        _laserPrefab2.SetActive(false);
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
