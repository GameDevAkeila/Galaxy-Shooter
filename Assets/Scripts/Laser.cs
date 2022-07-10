using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
[SerializeField]
private float _speed = 8.0f;//speed variable of 8
[SerializeField]
private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
       if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }   

    void MoveUp()
    { 
        transform.Translate(Vector3.up * _speed * Time.deltaTime);//translate laser up
        
        if( transform.position.y > 8f)                            //if laser position is greater than 8 on the y
         {   
            if (transform.parent != null)                           // check if this object has a parent
            {
                Destroy(transform.parent.gameObject);               //destroy parent too!;
            }

            Destroy(this.gameObject);                               //destroy the object
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_Shooter" && _isEnemyLaser == true)
        {
            Player_Shooter player = other.GetComponent<Player_Shooter>();
            if(player != null)
            {
                player.Damage();
            }
        }
    }
}

