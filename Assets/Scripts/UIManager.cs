using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{   [SerializeField]
    private TMP_Text _scoreText;         // handle to Text
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _livesSprites;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0; //assign text copmonent to the handle
    }

    public void UpdateScore(int playerShooterScore)
    {
        _scoreText.text = "Score: " + playerShooterScore.ToString();
    }

} 

   
