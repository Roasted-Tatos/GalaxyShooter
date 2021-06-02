using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour
{
    [SerializeField]
    private Animator _fader;
    [SerializeField]
    private AudioSource _volumeLower;

    private float _volumelow = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        _fader = GameObject.Find("Fade_OUT").GetComponent<Animator>();
        _volumeLower = GameObject.Find("Background Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            StartCoroutine(LoadSceneRoutine());
        }
            
    }
    IEnumerator LoadSceneRoutine()
    {
        _fader.SetBool("Next_Level", true);
        _volumeLower.volume = _volumelow;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
