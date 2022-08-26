using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool forwardDirection = false;
    public GameObject obj;
    
    private RaycastHit _hit;
    private float _speed;
    private float _boostSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _boostSpeed = agent.speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Vertical");
        float zMove = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Shift Pressed");
            _speed = _boostSpeed;
        }

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
            agent.speed = _speed;
            agent.SetDestination(movePosition);
        }
        else
        {
            agent.SetDestination(transform.position);
        }

        if (Input.GetKeyDown("mouse 0"))
        {
            Debug.Log("Mouse 0 pressed");
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out _hit))
            {
                Debug.Log(_hit.point.x + ", ");
                Debug.Log(_hit.point.y + ", ");
                Debug.Log(_hit.point.z + "\n");
                if (_hit.transform.CompareTag("Ground"))
                {
                    Debug.Log("Player is Ground");
                    // Instantiate(obj, _hit.point, obj.transform.rotation);
                    agent.destination = _hit.point;
                }
            }
        }
    }
}
