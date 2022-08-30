using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCamera : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aimCamera;

    public bool mouseButtonIsPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        aimCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("mouse 1") && !mouseButtonIsPressed)
        {
            mouseButtonIsPressed = true;

            // aimCamera.transform.position = mainCamera.transform.position;
            // aimCamera.transform.rotation = mainCamera.transform.rotation;
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else if (Input.GetKeyDown("mouse 1") && mouseButtonIsPressed)
        {
            mouseButtonIsPressed = false;
            
            // mainCamera.transform.position = aimCamera.transform.position;
            // mainCamera.transform.rotation = aimCamera.transform.rotation;
            
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }
}
