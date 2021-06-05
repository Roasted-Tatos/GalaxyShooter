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
    private GameObject _AfterBurnerLeft, _AfterBurnerRight;

    [SerializeField]
    private Animator _anim;

    //PowerUps
    [SerializeField]
    private int _ammoCount = 50;
    [SerializeField]
    private SpriteRenderer _ammoRenderer;
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
    [SerializeField]
    private GameObject _SpecialLaserBeam;
    [SerializeField]
    private int _specialPower = 3;
    
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
    [SerializeField]
    private AudioSource _beamSound;
    
   
    
    
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
        _AfterBurnerLeft.SetActive(false);
        _AfterBurnerRight.SetActive(false);
        //_chargingGlow.SetActive(false);
        _SpecialLaserBeam.SetActive(false);

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

        if (Input.GetKey(KeyCode.E) && Time.time > _canFire)
        {
            if (_specialPower == 0)
            {
                return;
            }
            SpecialFire();
        }


        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            if (_ammoCount ==0)
            {
                return;
            }
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
            _thrusterEnergy -= 0.1f;
            _uiManager.UpdateEnergyStatus(_thrusterEnergy);
            _AfterBurnerLeft.SetActive(true);
            _AfterBurnerRight.SetActive(true);
        }
        else
        {
            transform.Translate(Direction * _speed * Time.deltaTime);
            _AfterBurnerLeft.SetActive(false);
            _AfterBurnerRight.SetActive(false);
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

        if (transform.position.x >= 9.2f)
        {
            transform.position = new Vector3(9.2f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9f)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        AmmoCount(-1);
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

    public void AmmoCount(int bullets)
    {
        if(bullets >= _ammoCount)
        {
            _ammoCount = 50;
            
        }
        else
        {
            _ammoCount += bullets;
        }
        
        _uiManager.UpdateAmmo(_ammoCount);
    }

    public void SpecialFire()
    {
      
        StartCoroutine(SpecialBeamFireRoutine());
        SpecialBeamCount(-1);
        _canFire = Time.time + _fireRate;
        _beamSound.Play();

    }
    public void SpecialBeamCount(int beams)
    {
        if(beams >=_specialPower)
        {
            _specialPower = 3;
        }
        else
        {
            _specialPower += beams;
        }
        _uiManager.UpdateSpecialPower(_specialPower);
    }

    IEnumerator SpecialBeamFireRoutine()
    {
        _SpecialLaserBeam.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        _SpecialLaserBeam.SetActive(false);
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
                    _shieldRenderer.color = Color.blue;
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
       
        if(_thrusterEnergy >= 100f)
        {
            _thrusterEnergy = 100f;
        }
        else 
        {
            _thrusterEnergy = 100f;
        }
        _uiManager.UpdateEnergyStatus(_thrusterEnergy);
       
    }
   

    public void ShieldisActive()
    {
        _Shields.enabled = true;
        _shieldsActive = true;
        _shieldRenderer.color = Color.white;
        _shieldStrenght = 3;
    }

    public void restoreHealth(int healingPoints)
    {
        if(healingPoints >= _lives)
        {
            _lives = 3;
        }
        else
        {
            _lives += healingPoints;
        }
        _uiManager.UpdateLives(_lives);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
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
