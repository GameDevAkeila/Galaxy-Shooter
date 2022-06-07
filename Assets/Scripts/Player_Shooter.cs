using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooter : MonoBehaviour
{//public or private reference
//data type (int, float, bool, string)
//every variable has a name
//optional value assigned
[SerializeField]
private float _speed = 4.0f;
[SerializeField]
public GameObject _laserPrefab;
[SerializeField]
private float _fireRate = 0.15f;
[SerializeField]
private float _canFire = -1f;
[SerializeField]
private int _lives = 3;
[SerializeField]
private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
         //find the GameObject. Then GetComponent
         if (_spawnManager == null)
         {
             Debug.LogError("The Spawn Manager is Null");
         }
    }

    // Update is called once per frame
    void Update()

    {
        CalculateMovement();
        //if i hit the soace key
        //spawn GameObject
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }
      
    void CalculateMovement()  
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
              //new Vector3(5, 0, 0) * -5 * real time
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        
        
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if ( transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //if player on the x > 11
        //x pos = -11
        //else if player on the x is less than -11
        //x pos = 11
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);

        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f,transform.position.y, 0);
        }

    }

    void FireLaser()
    {
         _canFire = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0) , Quaternion.identity);
    }

    public void Damage()
    {
        
        _lives--;
        Debug.Log(_lives);
        //check if dead
        //if we are destroy us
         if(_lives < 1)
         {
             //communicate with SpawnManager 
             //let them know to stop 
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);

         }

    }
    
}
