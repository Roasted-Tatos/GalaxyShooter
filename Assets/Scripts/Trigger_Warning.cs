using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Warning : MonoBehaviour
{
    [SerializeField]
    private GameObject _warningSign;
    [SerializeField]
    private AudioSource _alarm;

    // Start is called before the first frame update
    void Start()
    {
        _warningSign.SetActive(false);
        _alarm = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StartWarningRoutine());
        }
    }

    IEnumerator StartWarningRoutine()
    {
        _warningSign.SetActive(true);
        _alarm.Play();
        yield return new WaitForSeconds(5f);
        _alarm.Pause();
        _warningSign.SetActive(false);
    }
}
