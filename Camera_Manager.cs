﻿using UnityEngine;
using System.Collections;

public class Camera_Manager : MonoBehaviour {
	
	public static Camera_Manager Instance;
	public Transform TargetLookAt;

	private float minY = 1;
	private float maxY = 100;
	private float minZ = 1;
	private float maxZ = 100;
	private Vector3 ValidCamPos;
	
	private Vector2 MouseAxis;
	
	void Awake()
	{
		//Store an Instance of itself
		Instance = this;
	}
	
	// Use this for initialization
	void Start () {
		// Validate camera position using Mathf.Clamp & Save the validated camera position
		ValidCamPos = Camera.mainCamera.transform.position;
		ValidCamPos = new Vector3 (
			ValidCamPos.x,
			Mathf.Clamp(ValidCamPos.y, minY, maxY),
			Mathf.Clamp(ValidCamPos.z, minZ, maxZ)
		);
		Camera.mainCamera.transform.LookAt(TargetLookAt);
		InitialCameraPosition();
	}
	
	void InitialCameraPosition() {
		// Set the default value for both mouse axis
		MouseAxis = new Vector2(); // SET TO 0
		
		// Set the validated initial camera position
		Camera.mainCamera.transform.position = ValidCamPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate() {
		VerifyUserMouseInput();
	}

	void VerifyUserMouseInput ()
	{
		// Check if the right mouse button is depressed (not required but useful for easy debugging)
		if (!Input.GetMouseButtonDown(1)) { // DOESN T WORK
			// Rotates the camera based on the users inputs
				// SET THE TARGET AS PIVOT POINT
				// ROTATE
				// LOOK AT THE TARGET
			
			// DEBUG
			Camera.mainCamera.transform.RotateAround(TargetLookAt.position, Vector3.up, -20 * Time.deltaTime); // Positive or negative for left or right
			Camera.mainCamera.transform.RotateAround(TargetLookAt.position, Vector3.left, 20 * Time.deltaTime); // Positive or negative for left or right			
		}
	}

	
	
	public static void InitialCameraCheck() {
		Camera_Manager cameraManager;		
		GameObject mainCamera;
		GameObject targetLookAt;
		
		// If no main camera then create one
		if (Camera.mainCamera) {
			mainCamera = Camera.mainCamera.gameObject;
		} else {
			mainCamera = new GameObject("MainCamera");
			mainCamera.AddComponent("Camera");
			mainCamera.tag = "MainCamera";
		}
		
		// Attach Camera_Manager script to the MainCamera
		mainCamera.AddComponent("Camera_Manager");
		cameraManager = mainCamera.GetComponent("Camera_Manager") as Camera_Manager;
		
		// Look for a targetLookAt, create one if it doesn't exist
		targetLookAt = GameObject.Find("targetLookAt") as GameObject;
		if (!targetLookAt) {
			targetLookAt = new GameObject("targetLookAt");
			targetLookAt.transform.position = Vector3.zero;
		}
		
		// Save the target look at value
		cameraManager.TargetLookAt = targetLookAt.transform; 
	}
}
