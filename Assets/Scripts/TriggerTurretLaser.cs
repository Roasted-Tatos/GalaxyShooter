using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTurretLaser : MonoBehaviour
{
    [SerializeField]
    private GameObject _laser1;
    [SerializeField]
    private GameObject _laser2;
    [SerializeField]
    private GameObject _laser3;
    [SerializeField]
    private GameObject _laser4;

    [SerializeField]
    private AudioSource _laserSound;

    // Start is called before the first frame update
    void Start()
    {
        _laser1.SetActive(false);
        _laser2.SetActive(false);
        _laser3.SetActive(false);
        _laser4.SetActive(false);
        _laserSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(LaserRoutine());
            _laserSound.Play();
        }
    }
    IEnumerator LaserRoutine()
    {
        _laser3.SetActive(true);
        _laser4.SetActive(true);
        yield return new WaitForSeconds(1f);
        _laser1.SetActive(true);
        _laser2.SetActive(true);
        yield return new WaitForSeconds(3f);
        _laser1.SetActive(false);
        _laser2.SetActive(false);
        _laser3.SetActive(false);
        _laser4.SetActive(false);
    }
}
