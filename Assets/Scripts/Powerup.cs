using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // move down at the speed of 3
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }

        //when leave screen, destroy us
    }

    //OnTriggerEnter
    //Only be collectable by Player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_Shooter")
        {
            Player_Shooter player = other.transform.GetComponent<Player_Shooter>();
            if (player != null)
            {
                player.TripleShotActive();
            }
           
            Destroy(this.gameObject);
        }
    }
} //communicate with the Player script
