using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private int _powerUpId;
    [SerializeField]
    private AudioClip _pickupSound;

    private Player _player;
    [SerializeField]
    private bool _magnetized = false;

    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Player Object was not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_magnetized)
        {
            float step = _speed * 2 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position,_player.transform.position, step);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        
        if (transform.position.y <= -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag =="Player")
        {
            switch(_powerUpId)
            {
                case 0:
                    _player.TripleShotActive();
                    
                    break;
                case 1:
                    _player.SpeedActive();
                    
                    break;
                case 2:
                    _player.ShieldisActive();
                   
                    break;
                case 3:
                    _player.restoreHealth(3);
                    break;
                case 4:
                    _player.AmmoCount(50);
                    break;
                case 5:
                    _player.SpecialBeamCount(3);
                    break;
                case 6:
                    _player.Slowed();
                    break;
                default:
                    Debug.Log("Default value");
                    break;
            }
            AudioSource.PlayClipAtPoint(_pickupSound, transform.position);

            Destroy(this.gameObject);
        }
    }

    private void MagOn()
    {
        if (gameObject.activeSelf)
        {
            _magnetized = true;
        }
        else
        {
            _magnetized = false;
        }
    }
    private void MagOff()
    {
        _magnetized = false;
    }
    private void OnEnable()
    {
        Player.OnMagnetPull += MagOn;
        Player.OnMagnetStop += MagOff;
    }
    private void OnDisable()
    {
        Player.OnMagnetPull -= MagOn;
        Player.OnMagnetStop -= MagOff;
    }

}

