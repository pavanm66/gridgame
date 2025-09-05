using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	public List<GameObject> followCam;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
			followCam.Add(transform.GetChild(i).gameObject);
        }
	}
	
	void Update () {

	}
}
public enum CameraType{
	TppCamera,
	FppCamera
}
