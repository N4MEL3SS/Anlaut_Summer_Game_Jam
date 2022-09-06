using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Rigidbody _rigidbody;
    [SerializeField] private Waiter _waiter;
    [SerializeField] private string _projectileTag;
    // Start is called before the first frame update
    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_projectileTag))
        {
            _waiter.GetWaiterAttantion(gameObject);
        }
    }

    public void ResetPosition()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.velocity = Vector3.zero;
    }
}
