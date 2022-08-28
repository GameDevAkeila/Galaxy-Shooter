using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _enemyMovementSelector;
    [SerializeField]
    private int _enemyID;
    
    //private float _switchTime = 1f;// /
   // [SerializeField]
   // private float _angle = 90f;// /
   // [SerializeField]
   // private float _circleSpeed = 0.5f;// /
   // [SerializeField]
   // private float _radius = 5f;// /
    //[SerializeField]
    //private float _centerPosX, _centerPosY;// /
    /// 
    
    

    private float _frequency = 3f;
    //width of cycle
    private float _amplitude = 5f;
    //speed  to complete cycyle
    private float _newSpeed = 2f;

    private Vector3 _pos;
    private Vector3 _axisPos;
    private float _enemyStartPos;

    private Vector3 _spawn;
    private float _xSpawn = -10f;
    private float _rightToLeft = 10f;

    private Player_Shooter _playerShooter;
    private Animator _anim;// handle to animator component
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovementSelector = Random.Range(0, 7);
        _pos = transform.position;//
        _axisPos = transform.right;//
       _spawn = new Vector3 (_xSpawn, Random.Range(0, 4f), 0);
        transform.position = _spawn;
        
        _playerShooter = GameObject.Find("Player_Shooter").GetComponent<Player_Shooter>();
        // null check for the player
        _audioSource = GetComponent<AudioSource>();
        if (_playerShooter == null)
        {
            Debug.LogError("The Player_Shooter is NULL");
        }

        _anim = GetComponent<Animator>();//assign the componenet to Anim

        if (_anim == null)
        {
            Debug.LogError("The Animator is Null.");
        }

    }
  
    void Update()
    {
        CalculateMovement();
        //EnemyMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 5f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
   
            for (int i = 0; i < lasers.Length; i++)
            {
               lasers[i].AssignEnemyLaser();
            }
        }
    }

    void CalculateMovement()
    {
        switch (_enemyMovementSelector)
        {
            //enemy move down
            case 0:
                EnemyMoveDown();
                break;

            //enemy moves left 
            case 1:
                EnemyLeft();
                break;

            case 2:
                EnemyRight();
                break;

            //enemy zigzag
            case 3:
                EnemyZigZagMovement();
                break;
            //enemy UpDown
            case 4:
                EnemyUpDown();
                break;
            //enemy circle
            case 5:
                //transform.Translate(Vector3.down * _speed * Time.deltaTime);
                //EnemyMovementCircle();
                break;
            default:
                Debug.Log("Default Value");
                break;
        }
        // move enemy down 4 meters per second//
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        //if at bottom of screen//
        if (transform.position.y <-5f)          
        {   
            //respawn at top with new random X position
            float randomV = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomV, 7, 0);
        }

        if (transform.position.x > 11f)    //if player on the x > 11
        {
            //transform.position = new Vector3 (Random.Range(11f, -11f), 7, 0);
            transform.position = new Vector3(-11.3f, transform.position.y, 0);//x pos = -11
        }
        else if (transform.position.x < -11.3f)                                //else if player on the x is less than -11
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);//x pos = 11
        }
    }

    void EnemyMoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
    }

   void EnemyLeft()
   {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(Vector3.left * _speed * Time.deltaTime);    
   }

    void EnemyRight()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }



    void EnemyZigZagMovement()
    {
        _pos += Vector3.down * _newSpeed * Time.deltaTime;
        transform.position = _pos + _axisPos * Mathf.Sin(Time.time * _frequency) * _amplitude;
    }

void EnemyUpDown()
    {
        _pos += Vector3.right * _speed * Time.deltaTime;
        transform.position = _pos + transform.up * Mathf.Sin(Time.time * _frequency) * _amplitude;
        if (_enemyStartPos > _rightToLeft)
        {
           // Respawn();
        }
        //float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        //transform.position = new Vector3(x, _pos.y, 0);
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }

    void EnemyMovementCircle()
    {

        float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        float y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player_Shooter")                                           //if other is player
        {
            Player_Shooter player = other.transform.GetComponent<Player_Shooter>();//damage the player
            if (player != null)
            {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");           // trigger aimation
            _speed = 0f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.25f);            //destroy us
       }

        if(other.tag == "Laser")//if other is laser
       {
           Destroy(other.gameObject);//damage laser
           if (_playerShooter != null)
            {
                _playerShooter.AddScore(10);//add 10 to Score
            }
            _anim.SetTrigger("OnEnemyDeath");// trigger animation
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.25f);//destroy us
        }
    }
}
