using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    [SerializeField] private bool cursorLock = true;
    private bool _cursorIsLocked;

    private void Update()
    {
        UpdateCursorLock();
    }

    private void UpdateCursorLock()
    {
        if (!cursorLock)
        {
            return;
        }
        
        if (Input.GetKeyUp (KeyCode.Escape))
        {
            _cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _cursorIsLocked = true;
        }
		
        switch (_cursorIsLocked)
        {
            case true:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case false:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
}

