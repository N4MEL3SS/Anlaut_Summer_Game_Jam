using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;

    // Start is called before the first frame update

    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        _normal = collision.contacts[0].normal;
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        
        Gizmos.color = Color.white;
        Gizmos.DrawLine(position, position + _normal * 5);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, position + Project(transform.forward));
    }

}
