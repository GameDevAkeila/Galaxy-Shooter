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
private float _speedMultiplier = 2;
[SerializeField]
private GameObject _laserPrefab;
[SerializeField]
private GameObject _tripleShotPrefab;
[SerializeField]
private float _fireRate = 0.15f;
[SerializeField]
private float _canFire = -1f;
[SerializeField]
private int _lives = 3;
[SerializeField]
private SpawnManager _spawnManager;

private bool _isTripleShotActive = false;//variable for isTripleShotActive
private bool _isSpeedBoostActive = false;
private bool _isShieldActive = false;
[SerializeField]
private GameObject _shieldVisual;

[SerializeField]
private int _score;             //variable reference to the shield visualizer

private UIManager _uiManager;

// Start is called before the first frame update

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
         //find the GameObject. Then GetComponent
         if (_spawnManager == null)
         {
             Debug.LogError("The Spawn Manager is NULL.");
         }

         if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()

    {
        CalculateMovement();
        //if i hit the space key
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
        if (_isTripleShotActive == true)
        //instantiate for the triple shot
        {
            Instantiate (_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0) , Quaternion.identity);
        }
        // if space key press, fire 1 laser
        //if triple shot active true
        // fire 3 lasers (triple shot prefab)
        //else fire one laser

        // instantiate 3 lasers (triple shot prefab)
    }

    public void Damage()
    {                                          //if shield is active
        if(_isShieldActive == true)           //do nothing...
        {
            _isShieldActive = false; 
            _shieldVisual.SetActive(false);   //deactivate shield//disable the visualizer
            return;                          //return;
        }
        _lives--;
       
         if(_lives < 1)                    // Debug.Log(_lives);//check if dead//if we are destroy us
         {
            _spawnManager.OnPlayerDeath(); //communicate with SpawnManager //let them know to stop
            Destroy(this.gameObject);
         }

    }

    public void TripleShotActive()
    { 
        _isTripleShotActive = true;                  //start the power down coroutine for triple shot
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;  
    }
    

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
       _shieldVisual.SetActive(true);     // enable the visualizer
       
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
 //method to add 10 to the Score
 //communicate with the UI to update the score   
}
