using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //the speed variable
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedIncrease = 2f;
    [SerializeField]
    private float _thrusterEnergy = 100f;

    [SerializeField]
    private Animator _anim;

    //PowerUps
    [SerializeField]
    private GameObject _laser;
    [SerializeField]
    private GameObject _TripleShot;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _shieldsActive = false;
    [SerializeField]
    private SpriteRenderer _shieldRenderer;
    private int _shieldStrenght = 3;
    
    //[SerializeField]
    //private bool _isSpeedActive = false;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;

    //Special Effects
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _screenCrack1;
    [SerializeField]
    private GameObject _screenCrack2;


    private SpawnManager _spawnManager;
    private CameraShake _cameraShake;

    [SerializeField]
    private SpriteRenderer _Shields;

    //Scoring and granting access to the UImanager
    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    //Audio
    [SerializeField]
    private AudioSource _laserSound;
    
   
    
    
    // Start is called before the first frame update
    void Start()
    {
        //starting position = new postion
        transform.position = new Vector3(0, 0, 0);

        _anim = GetComponent<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI manager not found");
        }

        _leftEngine.SetActive(false);
        _rightEngine.SetActive(false);
        _screenCrack1.SetActive(false);
        _screenCrack2.SetActive(false);

        //Finding the Spawn Manager
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager is not found");
        }
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if(_cameraShake == null)
        {
            Debug.LogError("camera shake script not foudn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        //Movement ANimation
        if (Input.GetKey(KeyCode.A))
        {
            _anim.SetBool("Turn_Left", true);
            
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _anim.SetBool("Turn_Right", true);
        }
        else
        {
            _anim.SetBool("Turn_Right", false);
        }

    }

    void CalculateMovement()
    {
        //Axis Variable
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);
        //movement
        if(Input.GetKey(KeyCode.LeftShift) && _thrusterEnergy > 0f)
        {
            transform.Translate(Direction * _speed *_speedIncrease* Time.deltaTime);
            _thrusterEnergy -= 0.5f;
            _uiManager.UpdateEnergyStatus(_thrusterEnergy);
        }
        else
        {
            transform.Translate(Direction * _speed * Time.deltaTime);
        }
        
        
       
        
       
        //Player Bounds
        if (transform.position.y >= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }
        else if (transform.position.y <= -3.37f)
        {
            transform.position = new Vector3(transform.position.x, -3.37f, 0);
        }

        if (transform.position.x >= 11.2f)
        {
            transform.position = new Vector3(-11.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.2f)
        {
            transform.position = new Vector3(11.2f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        
            _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        {
            Instantiate(_TripleShot, transform.position, Quaternion.identity);
        }
        else if (_isTripleShotActive == false)
        {
            Instantiate(_laser, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }

        _laserSound.Play();
        
    }

    public void  Damage()
    {
        if (_shieldsActive == true && _shieldStrenght >=1)
        {
            _shieldStrenght --;
            switch (_shieldStrenght)
            {
                case 0:
                    _shieldsActive = false;
                    _Shields.enabled = false;
                    break;
                case 1:
                    _shieldRenderer.color = Color.red;
                    break;
                case 2:
                    _shieldRenderer.color = Color.yellow;
                    break;
                default:
                    Debug.Log("Default value");
                    break;
            }
            return;
        }
        _shieldStrenght = 3;
        _lives -= 1;
        _cameraShake.StartShaking();

        if (_lives ==2)
        {
            _leftEngine.SetActive(true);
            StartCoroutine(CrackScreenRoutine1());
        }
        else if (_lives ==1)
        {
            _rightEngine.SetActive(true);
            StartCoroutine(CrackScreenRoutine1());
            StartCoroutine(CrackScrenRoutine2());
        }

        _uiManager.UpdateLives(_lives);

        if (_lives <1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.GameOver();
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        // _isSpeedActive = true;
        //_speed *= _speedIncrease;
        if(_thrusterEnergy >= 100f)
        {
            _thrusterEnergy = 100f;
        }
        else 
        {
            _thrusterEnergy = 100f;
        }
        _uiManager.UpdateEnergyStatus(_thrusterEnergy);
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
       // _isSpeedActive = false;
        //_speed /= _speedIncrease;
    }

    public void ShieldisActive()
    {
        _Shields.enabled = true;
        _shieldsActive = true;
        _shieldRenderer.color = Color.white;
        _shieldStrenght = 3;
    }

    public void GetPoints(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    //Screen Crack Routine
    IEnumerator CrackScreenRoutine1()
    {
        _screenCrack1.SetActive(true);
        yield return new WaitForSeconds(6f);
        _screenCrack1.SetActive(false);
    }
    IEnumerator CrackScrenRoutine2()
    {
        _screenCrack2.SetActive(true);
        yield return new WaitForSeconds(4f);
        _screenCrack2.SetActive(false);
    }
}
