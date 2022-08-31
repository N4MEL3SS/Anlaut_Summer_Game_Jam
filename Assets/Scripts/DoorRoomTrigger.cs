using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animo;
    public float wait = 15.0f;
    
    private int _animStateID;

    private void Start()
    {
        _animStateID = Animator.StringToHash("DoorRoomState");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !animo.GetBool(_animStateID))
        {
            animo.SetBool(_animStateID, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait);
        
        animo.SetBool(_animStateID, false);
    }
}
