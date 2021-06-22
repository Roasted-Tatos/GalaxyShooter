using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Behavior : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;
    private Player _player;
    private float _elaspeTime = 0;


    [SerializeField]
    private GameObject _scatterShot;
    [SerializeField]
    private GameObject _laserSpikes;

    private Animator anim;
    private float _speed = 6f;

    private int random;

    [SerializeField]
    private Animator Laser;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        Laser = GameObject.Find("AlienLaser").GetComponent<Animator>();

        random = Random.Range(0, 2);
        _laserSpikes.SetActive(false);

        transform.position = new Vector3(0, 10.6f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_elaspeTime >3 && _elaspeTime <5)
        {
            BossStart();
        }
        else
        {
            _elaspeTime += Time.deltaTime;
        }
        if (_elaspeTime > 6 && _elaspeTime <15)
        {
            if(random ==0)
            {
                PhaseA();
            }
            else
            {
                PhaseB();
            }
        }
        else
        {
            _elaspeTime += Time.deltaTime;
        }

        if(_health <= 50)
        {
            anim.SetTrigger("Stage2");
            _laserSpikes.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Laser")
        {
            _health -= 2;
            _player.playerLaser.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Beam")
        {
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
    public void FireLaser()
    {
        FireLaserRoutine();
    }

    IEnumerator FireLaserRoutine()
    {
        Laser.SetTrigger("FireLaser");
        yield return new WaitForSeconds(1);
    }
}
