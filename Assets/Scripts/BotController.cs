using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [SerializeField] private List<GameObject> pathArray;

    private NavMeshAgent _agent;
    
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // Not work!!!
        // PathGeneration();
    }

    private void PathGeneration()
    {
        foreach (var point in pathArray)
        {
            while (transform.position != point.transform.position && _agent.SetDestination(point.transform.position))
            {
                _agent.SetDestination(point.transform.position);
            }
        }
        Debug.Log("Next Point");
    }
}
