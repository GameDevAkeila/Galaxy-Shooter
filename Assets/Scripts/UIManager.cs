using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{   [SerializeField]
    private TMP_Text _scoreText;         // handle to Text
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _livesSprite;

    // Start is called before the first frame update
    void Start()
    {
       // _livesSprite[CurrentPlayer_ShooterLives = 3]
        _scoreText.text = "Score: " + 0; //assign text copmonent to the handle
    }

    public void UpdateScore(int playerShooterScore)
    {
        _scoreText.text = "Score: " + playerShooterScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSprite[currentLives];  //display img sprite//give it a new one base on currentLives index
    }

} 

   
