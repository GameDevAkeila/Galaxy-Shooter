using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerUpID;//0 = TripleShot 1 = Speed 3 = Shield
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
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                         Debug.Log("Collected Speed Boost");
                         break;
                    case 2:
                        Debug.Log("Shields Collected");
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
               
                //else if powerUp is 1
                //play speed powerip
                //else if powerup is 2 
                //shields powerup
            }
           
            Destroy(this.gameObject);
        }
    }
} //communicate with the Player scriptA
