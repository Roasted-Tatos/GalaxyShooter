using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Behavior : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;
    private Player _player;
    [SerializeField]
    private AudioClip _howl;
    [SerializeField]
    private AudioSource _dmgSound;
    [SerializeField]
    private GameObject _dmgFX;

    [SerializeField]
    private GameObject _scatterShot;
    [SerializeField]
    private GameObject _laserSpikes;
    [SerializeField]
    private Animator _bigLasers;

    private Animator anim;
    private float _speed = 6f;

    private int random;
    [SerializeField]
    private Animator _fader;

    [SerializeField]
    private Animator Laser;
    [SerializeField]
    private GameObject _explosions;
    [SerializeField]
    private Transform _explosionPointA,_explosionPointB,_explosionPointC;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        Laser = GameObject.Find("AlienLaser").GetComponent<Animator>();

        random = Random.Range(0, 2);
        _laserSpikes.SetActive(false);
        _fader = GameObject.Find("Fade_OUT").GetComponent<Animator>();

        transform.position = new Vector3(0, 10.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 50)
        {
            anim.SetTrigger("Stage2");
            _laserSpikes.SetActive(true);
        }
        if(_health ==0)
        {
            anim.SetTrigger("Death");
            AudioSource.PlayClipAtPoint(_howl, new Vector3(0, 0, 0));
            StartCoroutine(EndGame());
            Destroy(this.gameObject, 3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            _health -= 2;
            _dmgSound.Play();
            Instantiate(_dmgFX, transform.position, Quaternion.identity);
            _player.playerLaser.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Beam")
        {
            Instantiate(_dmgFX, transform.position, Quaternion.identity);
            _health -= 5;
        }
       
    }

    public void BossStart()
    {
            anim.SetTrigger("Start");
    }

    public void PhaseA()
    {
            anim.SetTrigger("PhaseA");
    }
    public void PhaseB()
    {
        anim.SetTrigger("PhaseB");
    }
    public void ScatterShot()
    {
        for (int fireAngle = 0; fireAngle < 360; fireAngle += 30)
        {
            var newBullet = Instantiate(_scatterShot, transform.position, Quaternion.identity);
            newBullet.transform.eulerAngles = Vector3.forward * fireAngle;
        }
        
    }
    public void FireBigLaser()
    {
        _bigLasers.SetTrigger("FireBigLaser");
    }

    public void DeathScene()
    {
        Instantiate(_explosions, _explosionPointA.position, Quaternion.identity);
        StartCoroutine(DelayExplosion());
    }
    
    IEnumerator DelayExplosion()
    {
        Instantiate(_explosions, _explosionPointB.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(_explosions, _explosionPointC.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator FireLaserRoutine()
    {
        Laser.SetTrigger("FireLaser");
        yield return new WaitForSeconds(1);
    }

    IEnumerator EndGame()
    {
        _fader.SetBool("Next_Level", true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
