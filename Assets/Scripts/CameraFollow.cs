using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    [SerializeField] private float cameraPositionX;
    [SerializeField] private float cameraPositionY;
    [SerializeField] private float cameraPositionZ;

    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        offset = new Vector3(cameraPositionX, cameraPositionY, cameraPositionZ);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mainCamera.transform.position = player.transform.position + offset;
    }
}
