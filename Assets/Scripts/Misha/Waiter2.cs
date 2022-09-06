using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Waiter : MonoBehaviour
{
    [Header("Path options")]
    [SerializeField] private List<GameObject> _pathArray;
    [SerializeField] private float _precision = 0.2f;
    [Header("Work Stop Points")]
    [SerializeField] private string _workPointTag = "Work_Point";
    [SerializeField] private float _workTime = 5.0f;
    [Header("Editor visualization")]
    [SerializeField] private float _sphereVerticalOffset = 4f;
    [SerializeField] private float _sphereRadius = 2f;
    [Header("Player Detection")]
    [SerializeField] private GameObject _player;
    [SerializeField] private List<GameObject> _waiterAlarmMarkers;
    [SerializeField] private float _verticalOffset = 1.0f;
    [SerializeField] private float _rayOffset = 1.0f;



    private bool _isWorking = false;
    private bool _isPlayerSpoted = false;
    private bool _isRepairing = false;

    private NavMeshAgent _agent;
    private int _i = 0;
    private Vector3 _rayBaseDirection = Vector3.forward * 1000;
    private Vector3 _rayLeftPositionOffset;
    private Vector3 _rayRightPositionOffset;
    private RaycastHit _rightHit;
    private RaycastHit _leftHit;
    private Coroutine _workCorutine;
    private GameObject _work;

    // Start is called before the first frame update
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_pathArray[_i].transform.position);
        _rayRightPositionOffset = new Vector3(_rayOffset, _verticalOffset, 0);
        _rayLeftPositionOffset = new Vector3(-_rayOffset, _verticalOffset, 0);
        foreach(GameObject waiterAlarmMarker in _waiterAlarmMarkers)
        {
            waiterAlarmMarker.SetActive(false);
        }
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (!_isWorking
            && !_isPlayerSpoted
            && !_isRepairing
            && Math.Abs(transform.position.x - _pathArray[_i].transform.position.x) < _precision 
            && Math.Abs(transform.position.z - _pathArray[_i].transform.position.z) < _precision)
        {
            if (_pathArray[_i].gameObject.CompareTag(_workPointTag))
            {
                _isWorking = true;
                _workCorutine = StartCoroutine(WorkCoorutine());
            }
            else
            {
                GoToNextPoint();
            }
        }
        else if (_isRepairing)
        {
            if (Math.Abs(transform.position.x - _work.transform.position.x) < _precision
            && Math.Abs(transform.position.z - _work.transform.position.z) < _precision)
            {
                if (!_isWorking)
                {
                    StartCoroutine(RepairCoorutine());
                }
            }
            else
            {
                _agent.SetDestination(_work.transform.position);
            }
        }

        if (!_isPlayerSpoted &&
            Physics.Raycast(transform.position + transform.rotation * _rayRightPositionOffset, 
            transform.rotation * _rayBaseDirection, out _rightHit)
            && Physics.Raycast(transform.position + transform.rotation * _rayLeftPositionOffset, 
            transform.rotation * _rayBaseDirection, out _leftHit)
            && _rightHit.collider.gameObject == _player 
            && _leftHit.collider.gameObject == _player)
        {
            PlayerDetected();
        } else if (_isPlayerSpoted) {
            _agent.SetDestination(_player.transform.position);
        }
    }

    IEnumerator WorkCoorutine()
    {
        yield return new WaitForSeconds(_workTime);
        _isWorking = false;
        GoToNextPoint();
        Debug.Log("Work Corutine");
    }

    IEnumerator RepairCoorutine()
    {
        _isWorking = true;
        yield return new WaitForSeconds(_workTime);
        Work work = _work.GetComponent<Work>();
        work.ResetPosition();
        PathGeneration(_pathArray[_i].transform.position);
        _isRepairing = false;
        _isWorking = false;
        Debug.Log("Repair Corutine");
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
                }
                else
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
                if (pathPoint.gameObject.CompareTag(_workPointTag))
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawSphere(currentSpherePositin, _sphereRadius);
                    Gizmos.color = Color.green;
                }
                else
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
        Gizmos.DrawRay(transform.position + transform.rotation * _rayRightPositionOffset, transform.rotation * _rayBaseDirection);
        Gizmos.DrawRay(transform.position + transform.rotation * _rayLeftPositionOffset, transform.rotation * _rayBaseDirection);
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

    private void PlayerDetected()
    {
        _isPlayerSpoted = true;
        _agent.SetDestination(_player.transform.position);
        foreach (GameObject waiterAlarmMarker in _waiterAlarmMarkers)
        {
            waiterAlarmMarker.SetActive(true);
        }
    }
    
    public void GetWaiterAttantion(GameObject attantionItem)
    {
        Debug.Log("Go to Work");
        if (!_isPlayerSpoted)
        {
            if (_isWorking)
            {
                StopCoroutine(_workCorutine);
                _isWorking = false;
            }
            _agent.SetDestination(attantionItem.transform.position);
            _work = attantionItem;
            _isRepairing = true;
        }
    }
}