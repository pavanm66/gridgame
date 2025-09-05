using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
	public float bulletHitValue;
	void Start () {
		Destroy(gameObject, 0.8f);
	}
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") || (other.tag == "Enemy"))
        {
            other.GetComponent<IHealth>().TakeDamage(bulletHitValue);
        }
        Destroy(gameObject);
    }

}
