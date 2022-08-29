using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float smoothTime = 0.5f;
    
    private CharacterController _characterController;
    private Gravity _gravity;
    
    private float _smoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gravity = GetComponent<Gravity>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xMove, 0, zMove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _smoothVelocity, smoothTime);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection.y = _gravity.GravityFunction();
            _characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
