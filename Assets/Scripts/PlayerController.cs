using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    
    [SerializeField] private float speedMove = 15.0f;
    [SerializeField] private float speedAngle = 0.2f;
    [SerializeField] private float smoothTime = 5.0f;

    private CharacterController _characterController;
    private Gravity _gravity;
    private Vector3 _moveVector;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gravity = GetComponent<Gravity>();
    }

    // Update is called once per frame
    private void Update()
    {
        CharacterMove();
    }

    private void CharacterMove()
    {
        _moveVector = Vector3.zero;

        // Возможно стоит разделить скорость на вертикальную и горизонтальную? (speedHorizontal, speedVertical);
        _moveVector.x = Input.GetAxis("Horizontal") * speedMove;
        _moveVector.z = Input.GetAxis("Vertical") * speedMove;
        

        // Debug.Log(_moveVector + "");
        
        
        // if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        if (_moveVector.magnitude >= 0.1f)
        {
            // Debug.Log(Vector3.Angle(Vector3.forward, _moveVector) + "");
            
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, speedAngle, 0f);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation(moveDirection),
				smoothTime * Time.deltaTime);
        }
        
        _moveVector.y = _gravity.GravityFunction();
        _characterController.Move(_moveVector * Time.deltaTime);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(0, 3, 1), Vector3.forward * 3);
    }
}
