using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;





public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;


    private void Update()
    {
        // if the R key is pressed 
        // restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current Game Scene
        }

        // if the escape key is pressed
        //quit the application

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    public void GameOver()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();

       _isGameOver = true;
    }  

 
    
    
    
}
