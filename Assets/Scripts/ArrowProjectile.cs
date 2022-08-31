using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    public float force;
    private Rigidbody _rigidbodyComponent;

    [SerializeField] private GameObject ready;
    [SerializeField] private GameObject used;
    [SerializeField] private float wait = 10.0f;
    
    private void Awake()
    {
        _rigidbodyComponent = GetComponent<Rigidbody>();
        _rigidbodyComponent.centerOfMass = transform.position;
    }

    public void Fire()
    {
        used.SetActive(false);
        ready.SetActive(true);
        _rigidbodyComponent.AddForce(transform.forward * (force * Random.Range(1.3f, 1.7f)), ForceMode.Impulse);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            used.SetActive(true);
            ready.SetActive(false);
            _rigidbodyComponent.isKinematic = true;
            StartCoroutine(Countdown());
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(wait);
        Destroy(gameObject);
    }
    
}
