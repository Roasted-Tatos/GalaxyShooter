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
    private Text _gameoverText;
    [SerializeField]
    private Text _restartLevel;
    [SerializeField]
    private Image _thrusterBar;
    private float _maxEnergy = 100f;

    [SerializeField]
    private Text _ammoText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //Starting score text, set to zero
        _scoreText.text = "Score: " + 0;
        _ammoText.text = 50.ToString();
        _gameoverText.gameObject.SetActive(false);
        _restartLevel.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    public void UpdateAmmo (int playerAmmo)
    {
        _ammoText.text = playerAmmo.ToString();
    }

    public void GameOver()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartLevel.gameObject.SetActive(true);
        _gameManager.GameOver();
    }
    public void UpdateEnergyStatus(float value)
    {
        float amount = value / _maxEnergy;
        _thrusterBar.fillAmount = amount;
    }
}
