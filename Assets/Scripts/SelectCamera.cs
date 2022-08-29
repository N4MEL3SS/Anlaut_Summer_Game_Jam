using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCamera : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject aimCamera;

    private bool _mouseButtonIsPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        aimCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 1") && !_mouseButtonIsPressed)
        {
            _mouseButtonIsPressed = true;

            aimCamera.transform.position = mainCamera.transform.position;
            aimCamera.transform.rotation = mainCamera.transform.rotation;
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else if (Input.GetKeyDown("mouse 1") && _mouseButtonIsPressed)
        {
            _mouseButtonIsPressed = false;
            
            mainCamera.transform.position = aimCamera.transform.position;
            mainCamera.transform.rotation = aimCamera.transform.rotation;
            
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }
}