using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private SurfaceSlider surfaceSlider;
    [SerializeField] private float speed;
    // Start is called before the first frame update

    public void Move(Vector3 direction)
    {
        Vector3 directionAlongSurface = surfaceSlider.Project(direction.normalized);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);
        
        rigidBody.MovePosition(rigidBody.position + offset);
    }
}
