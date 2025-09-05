using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tanker : MonoBehaviour, IHealth
{
	[SerializeField] private Image fillImage;
	[SerializeField] private Slider healthBar;

	[SerializeField] float health = 20f;
	[SerializeField] float maxHealth = 20f;
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health >= maxHealth)
			{
				health = maxHealth;

			}
			if (health <= 0f)
			{
				//gameObject.SetActive(false);
				//gameObject.GetComponentInParent<Turret>().gameObject.SetActive(false);
				health = 0f;
			}
			DisplayTankHealth(health / maxHealth);
		}
	}
	//private void OnTriggerEnter(Collider other)
	//{
	//	if (other.tag == "Bullet")
	//	{
	//		other.gameObject.SetActive(false);
	//		Health -= 5f;
	//	}
	//}
	void DisplayTankHealth(float _percentage)
    {
		healthBar.value = _percentage;
		fillImage.color = GameManager.instance.DisplayColorGradient(_percentage);
	}
	public void Replay()
    {
		Health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
		Health -= _damage;

	}
}
