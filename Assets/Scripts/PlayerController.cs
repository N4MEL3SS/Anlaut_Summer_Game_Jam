using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedHorizontal = 15.0f;
    [SerializeField] private float speedVertical = 15.0f;
    [SerializeField] private float speedAngle = 0.2f;
    [SerializeField] private float smoothTime = 5.0f;

    private CharacterController _characterController;
    private Vector3 _moveVector;
    private float _gravityForce;

    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CharacterMove();
        Gravity();
    }

    private void CharacterMove()
    {
        _moveVector = Vector3.zero;

        // Возможно стоит разделить скорость на вертикальную и горизонтальную? (speedHorizontal, speedVertical);
        _moveVector.x = Input.GetAxis("Horizontal") * speedHorizontal;
        _moveVector.z = Input.GetAxis("Vertical") * speedVertical;
        

        // Debug.Log(_moveVector + "");
        
        
        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Debug.Log(Vector3.Angle(Vector3.forward, _moveVector) + "");
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, speedAngle, 0f);
            transform.localRotation = Quaternion.Slerp (transform.localRotation, Quaternion.LookRotation(direction),
				smoothTime * Time.deltaTime);
        }
        
        _moveVector.y = _gravityForce;
        _characterController.Move(_moveVector * Time.deltaTime);
    }

    private void Gravity()
    {
        if (!_characterController.isGrounded)
        {
            _gravityForce -= 10f * Time.deltaTime;
        }
        else
        {
            _gravityForce = -1f;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(Vector3.forward, Vector3.forward * 3);
    }
}
