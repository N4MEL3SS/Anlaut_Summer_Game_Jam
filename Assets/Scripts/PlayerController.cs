using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private bool forwardDirection = false;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Vertical");
        float zMove = Input.GetAxis("Horizontal");

        if (xMove != 0 || zMove != 0)
        {
            Vector3 moveDirection;
            
            if (forwardDirection)
            {
                moveDirection = new Vector3(xMove, 0, -zMove);
            }
            else
            {
                moveDirection = new Vector3(xMove - zMove, 0, -zMove - xMove);
            }
            
            Vector3 movePosition = transform.position + moveDirection;
            navMeshAgent.SetDestination(movePosition);
        }
        else
        {
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
