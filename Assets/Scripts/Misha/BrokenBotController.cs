using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BrokenBotController : MonoBehaviour
{
    [Header("Path options")]
    [SerializeField] private string _breakePointTag = "Break_Point";
    [SerializeField] private List<GameObject> _pathArray;
    [SerializeField] private float _precision = 0.2f;
    [Header("Editor visualization")]
    [SerializeField] private float _sphereVerticalOffset = 4f;
    [SerializeField] private float _sphereRadius = 2f;

    private bool _b_IsStuck = false;
    [HideInInspector] public bool IsStuck { get { return _b_IsStuck; } }

    private NavMeshAgent _agent;
    private int _i = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_pathArray[_i].transform.position);
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (Math.Abs(transform.position.x - _pathArray[_i].transform.position.x) < _precision && Math.Abs(transform.position.z - _pathArray[_i].transform.position.z) < _precision)
        {
            if (_pathArray[_i].gameObject.CompareTag(_breakePointTag))
            {
                _b_IsStuck = true;
            } else
            {
                GoToNextPoint();
            }
        }
    }

#if UNITY_EDITOR 
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        GameObject firstPoint = null;
        GameObject lastPoint = null;
        bool isMoreThanTwoPoints = false;
        Vector3 currentSpherePositin = new Vector3();
        Vector3 previusSpherePositin = new Vector3();
        foreach (GameObject pathPoint in _pathArray)
        {
            if (pathPoint != null)
            {
                if (firstPoint == null)
                {
                    firstPoint = pathPoint;
                    currentSpherePositin.x = pathPoint.transform.position.x;
                    currentSpherePositin.y = pathPoint.transform.position.y + _sphereVerticalOffset;
                    currentSpherePositin.z = pathPoint.transform.position.z;
                } else
                {
                    previusSpherePositin.x = currentSpherePositin.x;
                    previusSpherePositin.y = currentSpherePositin.y;
                    previusSpherePositin.z = currentSpherePositin.z;

                    currentSpherePositin.x = pathPoint.transform.position.x;
                    currentSpherePositin.y = pathPoint.transform.position.y + _sphereVerticalOffset;
                    currentSpherePositin.z = pathPoint.transform.position.z;
                    Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
                    if (lastPoint != firstPoint)
                    {
                        isMoreThanTwoPoints = true;
                    }
                    lastPoint = pathPoint;
                }
                if (pathPoint.gameObject.CompareTag(_breakePointTag)) {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(currentSpherePositin, _sphereRadius);
                    Gizmos.color = Color.green;
                } else
                {
                    Gizmos.DrawSphere(currentSpherePositin, _sphereRadius);
                }
            }
        }
        if (isMoreThanTwoPoints)
        {
            previusSpherePositin.x = firstPoint.transform.position.x;
            previusSpherePositin.y = firstPoint.transform.position.y + _sphereVerticalOffset;
            previusSpherePositin.z = firstPoint.transform.position.z;
            Gizmos.DrawLine(previusSpherePositin, currentSpherePositin);
        }
    }
#endif

    private void GoToNextPoint()
    {
        if (++_i >= _pathArray.Count)
        {
            _i = 0;
        }

        PathGeneration(_pathArray[_i].transform.position);
    }

    private void PathGeneration(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void UnStack()
    {
        GoToNextPoint();
        _b_IsStuck = false;
    }

    private void OnMouseDown()
    {
        UnStack();
    }
}
