using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MouseController : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	
	[SerializeField] private float xSensitivity = 2f;
	[SerializeField] private float ySensitivity = 2f;
	
	[SerializeField] private float minimumX = -90F;
	[SerializeField] private float maximumX = 90F;
	
	[SerializeField] private bool clampVerticalRotation = true;
	[SerializeField] private bool lockCursor = true;
	
	[SerializeField] private bool smooth = true;
	[SerializeField] private float smoothTime = 5f;

	private Quaternion _characterRotation;
	private Quaternion _cameraRotation;
	
	private bool _mCursorIsLocked = true;
	
	private void Start()
	{
		_characterRotation = transform.localRotation;
		if (Camera.main != null)
			_cameraRotation = mainCamera.transform.localRotation;
	}

	private void Update()
	{
		LookRotation(transform, mainCamera.transform);
		UpdateCursorLock();
	}
	
	private void LookRotation(Transform character, Transform playerCamera)
	{
		float yRotation = Input.GetAxis("Mouse X") * xSensitivity;
		float xRotation = Input.GetAxis("Mouse Y") * ySensitivity;

		_characterRotation *= Quaternion.Euler(0f, yRotation, 0f);
		_cameraRotation *= Quaternion.Euler(-xRotation, 0f, 0f);

		if (clampVerticalRotation)
		{
			_cameraRotation = ClampRotationAroundXAxis(_cameraRotation);
		}

		if (smooth)
		{
			character.localRotation = Quaternion.Slerp (character.localRotation, _characterRotation,
				smoothTime * Time.deltaTime);
			playerCamera.localRotation = Quaternion.Slerp (playerCamera.localRotation, _cameraRotation,
				smoothTime * Time.deltaTime);
		}
		else
		{
			character.localRotation = _characterRotation;
			playerCamera.localRotation = _cameraRotation;
		}
	}

	private void UpdateCursorLock()
	{
		if (!lockCursor)
		{
			return;
		}
		if (Input.GetKeyUp (KeyCode.Escape))
		{
			_mCursorIsLocked = false;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_mCursorIsLocked = true;
		}
		
		switch (_mCursorIsLocked)
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

	private Quaternion ClampRotationAroundXAxis (Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, minimumX, maximumX);
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}
