using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int powerUpID;//0 = TripleShot 1 = Speed 2 = Shield
    [SerializeField]
    private AudioClip _clip;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);// move down at the speed of 3

        if (transform.position.y < -5.0f)
        {
            Destroy(this.gameObject);
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)//OnTriggerEnter
    {
        if (other.tag == "Player_Shooter")          //Only be collectable by Player
        {
            Player_Shooter player = other.transform.GetComponent<Player_Shooter>();

            AudioSource.PlayClipAtPoint(_clip, transform.position);

            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                         player.SpeedBoostActive();         //else if powerUp is 1
                         break;
                    case 2:
                        player.ShieldActive();              //else if powerup is 2
                        break;
                    //case 3:
                        //player.CheckAmmo(15);
                      //  break;
                    case 3:
                        player.LifeRefillActive(1);          //call this method from the Player script
                        break;
                    case 4:
                       player.MultiShotActive();
                       break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
               
            }
           
            Destroy(this.gameObject);
        }
    }
} 
