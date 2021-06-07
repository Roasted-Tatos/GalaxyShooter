using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _LivesImageHandle;
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _ammoRenderer;

    [SerializeField]
    private Image _specialPowerUPHandle;
    [SerializeField]
    private Sprite[] _specialPowerUPSprites;

    //[SerializeField]
    //private Text _gameoverText;
    //[SerializeField]
    //private Text _restartLevel;

    [SerializeField]
    private GameObject _gameOverMenu;
    
    [SerializeField]
    private Image _thrusterBar;
    private float _maxEnergy = 100f;

    [SerializeField]
    private Text _ammoText;

    private GameManager _gameManager;
    private Player _player;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        //Starting score text, set to zero
        _scoreText.text = "Score: " + 0;
        _ammoText.text = 50.ToString();
        //_gameoverText.gameObject.SetActive(false);
        //_restartLevel.gameObject.SetActive(false);
        _gameOverMenu.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //public method so player can asscess/Update the UI text
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        _LivesImageHandle.sprite = _liveSprites[currentLives];

    }
    public void UpdateSpecialPower(int currentpower)
    {
        _specialPowerUPHandle.sprite = _specialPowerUPSprites[currentpower];
    }

    public void UpdateAmmo (int playerAmmo)
    {
        _ammoText.text = playerAmmo.ToString();
        if (playerAmmo == 0)
        {
            switch (playerAmmo)
            {
                case 0:
                    _ammoRenderer.color = Color.red;
                    break;
                default:
                    Debug.Log("Default value");
                    break;
            }
        }
        else
        {
            _ammoRenderer.color = Color.white;
        }
    }

    public void GameOver()
    {
        //_gameoverText.gameObject.SetActive(true);
        //_restartLevel.gameObject.SetActive(true);
        _gameOverMenu.SetActive(true);
        _gameManager.GameOver();
    }
    public void Respawned()
    {
        _player.PlayerRespawn();
        _gameOverMenu.SetActive(false);
        _gameManager.Respawned();
        _spawnManager.StartCoroutine();
    }
    public void UpdateEnergyStatus(float value)
    {
        float amount = value / _maxEnergy;
        _thrusterBar.fillAmount = amount;
    }
}
