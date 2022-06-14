using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    // Start is called before the first frame update

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

            Destroy(this.gameObject);
       }

        //if other is laser
        //damage laser
        //destroy us
        if(other.tag == "Laser")
       {
           Destroy(other.gameObject);
           Destroy(this.gameObject);
        }
    }
}
