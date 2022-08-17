using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    private bool _isCameraShakeActive =false;
    private float _camShakeForce = 0.01f;


    // Update is called once per frame
    void Update()
    {
        CameraShakeActive();
    }

    public void CameraShakeActive()
    {
       
        if(_isCameraShakeActive ==true)
        {
            Vector3 camPos = _mainCamera.transform.position;
            float offsetX = Random.value * _camShakeForce * 2 - _camShakeForce;
            float offsetY = Random.value * _camShakeForce * 2 - _camShakeForce;
            camPos.x += offsetX;
            camPos.y += offsetY;
            _mainCamera.transform.position = camPos;
           
        }
    }

    public void ShakeActivate()
    {
        _isCameraShakeActive = true;
         StartCoroutine(CameraShakeDeactivate());
    }
    IEnumerator CameraShakeDeactivate()
    {
        yield return new WaitForSeconds(1.0f);
        _isCameraShakeActive = false;
    }
}
