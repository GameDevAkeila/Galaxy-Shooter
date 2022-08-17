using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _laserPrefab;

    private float _frequency = 2f;
    //width of cycle
    private float _amplitude = 5f;
    //speed  to complete cycyle
    private float _newSpeed = 2f;
    private Vector3 _pos;
    private Vector3 _axisPos;

    private Player_Shooter _playerShooter;
    private Animator _anim;// handle to animator component
    private AudioSource _audioSource;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
        _axisPos = transform.right;
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
        EnemyMovement();

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
        transform.Translate(Vector3.down * _speed * Time.deltaTime);// move enemy down 4 meters per second
        if (transform.position.y <-5f)          //if at bottom of screen
        {
            float randomV = Random.Range(-8f, 8f);//respawn at top with new random X position
            transform.position = new Vector3(randomV, 7, 0);
        }
    }

    void EnemyMovement()
    {
        _pos += Vector3.down * _newSpeed * Time.deltaTime;
        transform.position = _pos + _axisPos * Mathf.Sin(Time.time * _frequency) * _amplitude;
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
