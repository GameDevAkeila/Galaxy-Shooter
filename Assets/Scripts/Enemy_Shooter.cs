using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player_Shooter _playerShooter;
    private Animator _anim;// handle to animator component

    

    // Start is called before the first frame update
     void Start()
    {
        _playerShooter = GameObject.Find("Player_Shooter").GetComponent<Player_Shooter>();
        // null check for the player
        if (_playerShooter == null)
        {
            Debug.LogError("The Player_Shooter is NULL");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Anomator is Null.");
        }

        //assign the componenet to Anim
    }


    // Update is called once per frame
    void Update()

    {
        // move enemy down 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if at bottom of screen
        //respawn at top with new random X position

        if (transform.position.y < -5f)
        {
            float randomV = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomV, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is player
        //damage the player
        //destroy us
        if(other.tag == "Player_Shooter")
        {
            Player_Shooter player = other.transform.GetComponent<Player_Shooter>();
            if (player != null)
            {
                player.Damage();
            }
            // trigger aimation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0f;
            Destroy(this.gameObject, 2.25f);
       }

        //if other is laser
        //damage laser
        //destroy us
        if(other.tag == "Laser")
       {
           Destroy(other.gameObject);
           if (_playerShooter != null)
            {
                _playerShooter.AddScore(5);
            }
            // trigger animation
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(this.gameObject,2.25f);
        }//add 10 to Score
    }
}
