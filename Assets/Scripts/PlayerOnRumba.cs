using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOnRumba : MonoBehaviour
{
    public ThirdPersonController playerThirdPersonController;
    public GameObject player;
    public GameObject up;

    private bool _isRumba = false;
    
    private void LateUpdate()
    {
        if (_isRumba && playerThirdPersonController.isStop)
        {
            Debug.Log("Player Stop");
            player.transform.position = new Vector3(transform.position.x, up.transform.position.y, transform.position.z);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Player up Rumba");
            _isRumba = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Player down Rumba");
        _isRumba = false;
    }
}
