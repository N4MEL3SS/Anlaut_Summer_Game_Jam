using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTriggerScript : MonoBehaviour
{

    public LiftScript lift;
    public Animator liftDoor;
    public bool liftUp;
    public bool liftDown;
    public float animWaitTime = 15.0f;
    
    private int _animatorBool;
    
    // Start is called before the first frame update
    private void Start()
    {
        _animatorBool = Animator.StringToHash("DoorState");
        liftDoor.SetBool(_animatorBool, false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lift.onTrigger = true;
            lift.isUp = liftDown;
            lift.isDown = liftUp;
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        liftDoor.SetBool(_animatorBool, true);

        yield return new WaitForSeconds(animWaitTime);
        
        liftDoor.SetBool(_animatorBool, false);
    }
}
