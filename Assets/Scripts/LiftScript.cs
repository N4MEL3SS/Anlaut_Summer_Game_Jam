using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public Transform upPoint;
    public Transform downPoint;
    
    [SerializeField] private float liftSpeed = 0.1f;
    [SerializeField] private float waitTime = 3.0f;

    public bool isUp = true;
    public bool isDown = false;
    public bool onTrigger = false;
    public GameObject player;
    // Start is called before the first frame update
    
    // Update is called once per frame
    private void Update()
    {
        if (onTrigger && isUp)
        {
            LiftMove(downPoint.transform.position.y);
        }
        else if (onTrigger && isDown)
        {
            LiftMove(upPoint.transform.position.y);
        }
    }
    
    private void LiftMove(float targetPoint)
    {
        float target = Mathf.Lerp(transform.position.y, targetPoint, liftSpeed * Time.deltaTime);
        Vector3 diretcion = new Vector3(transform.position.x, target, transform.position.z);
        player.transform.position += new Vector3(0, target * 2, 0);
        transform.position = diretcion;

        if (Math.Abs(transform.position.y - targetPoint) < 0.05f)
        {
            onTrigger = false;
            isUp = !isUp;
            isDown = !isDown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!onTrigger)
            {
                StartCoroutine(Wait());
            }
            
            Debug.Log("Player on Trigger Lift");
            onTrigger = true;
        }
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
    }
    
}
