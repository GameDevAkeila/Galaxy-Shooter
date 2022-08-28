using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagEnemy : Enemy_Shooter
{
   // private float _frequency = 2f;
    //width of cycle
   /// private float _amplitude = 5f;
    //speed  to complete cycyle
   /// private float _newSpeed = 2f;
   /// private Vector3 _pos;
   // private Vector3 _axisPos;

    // Start is called before the first frame update
    void Start()
    {
     //   _pos = transform.position;
      //  _axisPos = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
      ///  EnemyMovement();
    }

  //  void EnemyMovement()
   // {
      //  _pos += Vector3.down * _newSpeed * Time.deltaTime;
      //  transform.position = _pos + _axisPos * Mathf.Sin(Time.time * _frequency) * _amplitude;
   // }
}
