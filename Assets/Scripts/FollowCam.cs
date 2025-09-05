 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	public CameraType cameraType;
	private bool cameraSetter;

	void Start () {
		offset = transform.position - player.transform.position;
        cameraSetter = true;
	}
	
	void Update () {
        // ChangeCamera();
        //transform.position = player.transform.position + offset;
        if (cameraSetter)
        {
            transform.position = player.transform.position + offset;
            // tpp
        }
        else
        {
            transform.position = player.transform.position;
            // fpp
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            cameraSetter = !cameraSetter;
        }
    }
	void ChangeCamera()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (cameraSetter)
            {
                transform.position = player.transform.position + offset;
                // tpp
            }
            else
            {
                transform.position = player.transform.position;
                // fpp
            }
            cameraSetter = !cameraSetter;
            /*
                switch (cameraType)
            {
                case CameraType.TppCamera:
                    break;
                case CameraType.FppCamera:
                    break;
                default:
                    break;
            }*/
        }
    }
}
