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
    private TMP_Text _ammoCountText;
    
   


    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        //assign text component to the handle
        _ammoCountText.text = "Ammo: " + 15;
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

    public void AmmoCountUpdate(int playerShooterAmmo)
    {
        _ammoCountText.text = "Ammo: " + playerShooterAmmo.ToString();
        
        if (playerShooterAmmo == 0)
        {
            _ammoCountText.color = Color.red;
        }
        else if (playerShooterAmmo == 5)
        {
            _ammoCountText.color = Color.yellow;
        }
        else if(playerShooterAmmo == 15)
        {
            _ammoCountText.color = Color.green;
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
            yield return new WaitForSeconds(0.3f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.3f);
        }
    }      

} 

   
