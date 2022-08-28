using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;         // handle to Text
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private TMP_Text _gameOverText;
    [SerializeField]
    private TMP_Text _restartText;
    [SerializeField]
    private TMP_Text _youWinText;
    [SerializeField]
    private TMP_Text _ammoCountText;
    [SerializeField]
    private TMP_Text _waveCountText;
  //[SerializeField]
   //private TMP_Text _waveNumber;

    private int _wavesNumber;

    private TMP_Text _enemiesLeftText;
    private TMP_Text _wavesLeftText;
    
    private bool _playerWins = false;
    
    private int MaxAmmo = 30;
    
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _waveCountText.text = "Waves" + 12;
        //assign text component to the handle
        UpdateAmmoCount(MaxAmmo);
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager is  NULL.");
        }
    }


    public void UpdateScore(int playerShooterScore)
    {
        _scoreText.text = "Score: " + playerShooterScore.ToString();
    }


    public void UpdateAmmoCount(int ammoCount )
    {
        _ammoCountText.text = "Ammo:" + ammoCount + "/" + MaxAmmo;
        if (ammoCount == 0)
        { 
            _ammoCountText.color = Color.red;
        }
        else if (ammoCount == 15)
        {
            _ammoCountText.color = Color.yellow;
        }

        else if (ammoCount == 30)
        {
            _ammoCountText.color = Color.green;
        }
    }

    public void UpdateWaveCount(int wavesNumber)
    {
        _waveCountText.text = "Wave" + wavesNumber;
        StartCoroutine(UpdateWaveCountRoutine());
    }

    IEnumerator UpdateWaveCountRoutine()
    {
        _waveCountText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _waveCountText.gameObject.SetActive(false);
        StopCoroutine(UpdateWaveCountRoutine());
    }

    public void CurrentWave(int wavesLeft, int enemiesLeft)
    {
        _wavesLeftText.text = "Waves Remaining" + wavesLeft;
        _enemiesLeftText.text = "Enemies Remaining" + enemiesLeft;
    }

    public void YouWin()
    {
        _playerWins = true;
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(YouWon());

    }

    IEnumerator YouWon()
    {
        while(_playerWins)
        {
           // _youWin.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.25f);
           // _youWin.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.25f);

        }
    }



    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprite[currentLives]; //display img sprite//give it a new one base on currentLives index

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }


    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = " GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }      

} 

   
