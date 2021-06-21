using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BOSS : MonoBehaviour
{
    [SerializeField]
    private float _speed =0.5f;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Transform _PositionA;
    [SerializeField]
    private Transform _PositionB;

    [SerializeField]
    private Slider _HealthBar;

    [SerializeField]
    private GameObject _LaserPatternA;
    [SerializeField]
    private GameObject _ScatterShot;

    private float _canFire = -1f;
    private float _fireRate = 2f;
  
    public enum BossState
    {
        Idle,
        Lasers,
        Bullets,
        StartPosition,
        PositionA,
        PositionB,
        Death
    }
    public BossState CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = BossState.Idle;
        transform.position = new Vector3(0.17f, 11.7f, 0);


        _target.transform.position = new Vector3(0f, 5.51f, 0f);
        _PositionA.transform.position = new Vector3(-5.2f, 0.5f, 0);
        _PositionB.transform.position = new Vector3(5f, 0.5f, 0);
        _LaserPatternA.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        _HealthBar.value = _health;
        switch (CurrentState)
        {
            case BossState.Idle:
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                StartCoroutine(SwitchtoNextState());
                break;
            case BossState.Lasers:
                
                break;
            case BossState.Bullets:
                if (Time.time > _canFire)
                {
                    _fireRate = Random.Range(2f, 4f);
                    _canFire = Time.time + _fireRate;
                    ScatterShot();
                }
                new WaitForSeconds(5f);
                CurrentState = BossState.StartPosition;
                break;
            case BossState.StartPosition:
                transform.position = Vector3.MoveTowards(transform.position, _PositionA.position, _speed * Time.deltaTime);
                new WaitForSeconds(5f);
             
                break;
            case BossState.PositionA:
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
                break;
            case BossState.PositionB:
                break;

            case BossState.Death:
                break;
        }
    }
    private void ScatterShot()
    {
        for (int fireAngle = 0; fireAngle < 360; fireAngle += 30)
        {
            var newBullet = Instantiate(_ScatterShot, transform.position, Quaternion.identity);
            newBullet.transform.eulerAngles = Vector3.forward * fireAngle;
        }

    }

    IEnumerator SwitchtoNextState()
    {
        yield return new WaitForSeconds(5f);
        CurrentState = BossState.Bullets;
        yield return new WaitForSeconds(5f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Laser")
        {
            _health -= 2;
            Destroy(other.gameObject);
        }
    }

}
