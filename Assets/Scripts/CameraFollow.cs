using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;

    [SerializeField] private Vector3 cameraOffset;
    
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
            mainCamera.transform.position = player.transform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        mainCamera.transform.position = player.transform.position + cameraOffset;
    }
}
