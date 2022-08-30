using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField] private float gravityForce = -15f;
    [SerializeField] private float gravityIdle = -0.5f;
    
    private CharacterController _characterController;
    private float _gravity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public float GravityFunction()
    {
        if (!_characterController.isGrounded)
        {
            _gravity -= gravityForce * Time.deltaTime;
        }
        else
        {
            _gravity = gravityIdle;
        }

        return _gravity;
    }
}
