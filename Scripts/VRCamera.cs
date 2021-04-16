using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class VRCamera : MonoBehaviour {

	//Hand Tracking
	public GameObject CameraRigPrefab;
	private SteamVR_Behaviour_Pose poseLeft = null;
	private SteamVR_Behaviour_Pose poseRight = null;
	GameObject CameraRig;
	Transform LeftHand;
	Transform RightHand;
	Transform TrackedLeft;
	Transform TrackedRight;
	
	// Use this for initialization
	void Start () {
		LeftHand = transform.Find("LeftHand");
		RightHand= transform.Find("RightHand");

		//Creates the Camera Rig
		CameraRig = Instantiate(CameraRigPrefab);
		
		CameraRig.transform.position = transform.position;
		CameraRig.transform.rotation= transform.rotation;
		CameraRig.transform.localScale = Vector3.one;

		poseLeft = CameraRig.transform.Find("Controller (left)").GetComponent<SteamVR_Behaviour_Pose>();
		poseRight = CameraRig.transform.Find("Controller (right)").GetComponent<SteamVR_Behaviour_Pose>();

		var svrLeftHand = CameraRig.transform.Find("Controller (left)");
		var svrRightHand = CameraRig.transform.Find("Controller (right)");

		TrackedLeft = svrLeftHand.transform;
		TrackedRight = svrRightHand.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		modelTracking();
	}

	void modelTracking()
{
		LeftHand.position = TrackedLeft.position;
		LeftHand.rotation = TrackedLeft.rotation;

		RightHand.position = TrackedRight.position;
		RightHand.rotation = TrackedRight.rotation;
}
}

