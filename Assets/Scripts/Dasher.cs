using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : MonoBehaviour {

	public bool decision;
    public Vector3 obsPostion;
    public GameObject obs;
    public float xPosition = -8f;
	public bool Decision
    {
        get
        {
			return decision;
        }
        set
        {
			decision = value;
            if (decision)
            {
				
            }
            else
            {

            }
        }
    }
	void Start() {
        //obs = GetComponent<GameObject>();
        //obsPostion = GetComponent<GameObject>().transform.position;
    }

	// Update is called once per frame
	void Update() {

        
        if (decision)
        {
            //con
            //obs.transform.position.x += Time.deltaTime * 0.1f;
            if (transform.position.x > 18f)
            {
                decision = false;
            }
        }
        else
        {
            //con
            if (transform.position.x < -8f)
            {
                decision = true;
            }
        }
	}
	
}
