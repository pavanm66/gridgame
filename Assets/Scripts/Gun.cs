using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletDamageValue;
	public Transform shootingPoint;
	public float fireRange;


    public void Shoot()
    {
		GameObject newBullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
		newBullet.GetComponent<BulletScript>().bulletHitValue = bulletDamageValue;
		newBullet.GetComponent<Rigidbody>().AddForce(shootingPoint.forward * fireRange, ForceMode.Impulse);
    }
}
