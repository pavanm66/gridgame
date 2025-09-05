using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthScript : MonoBehaviour, IHealth
{

	public bool isHealthMode;
	private bool gameOver;
	public float health = 30f;
	private float waitTime = 0.1f;
	private float maxHealth = 30f;
	public float damageTake = 2f;
	public float damage;

	// Health Properties
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
				GameOver = true;
				health = 0f;
				print("You Are Dead");
			}
			DisplayHealth(health / maxHealth);
		}
	}

	// IsHealthMode Properties
	public bool IsHealthMode
	{
		get
		{
			return isHealthMode;
		}
		set
		{
			isHealthMode = value;
			if (isHealthMode)
			{
				StopCoroutine("TakeDamage");
				StartCoroutine("GainHealth");
			}
			else
			{
				StopCoroutine("GainHealth");
				StartCoroutine("TakeDamage");
			}
		}
	}

	// GameOver Properties
	public bool GameOver
	{
		get
		{
			return gameOver;
		}
		set
		{
			gameOver = value;
			if (gameOver)
			{
				StopAllCoroutines();
				DisplayGameOver();
			}
		}
	}

	// Code to take damage
	IEnumerator TakeDamage()
	{
		while (Health > 0f)
		{
			Health -= waitTime * damageTake;

			yield return new WaitForSeconds(waitTime);
		}
	}

	// Code to Gain health
	IEnumerator GainHealth()
	{
		while (Health <= maxHealth)
		{
			Health += waitTime * damageTake;

			yield return new WaitForSeconds(waitTime);
		}
	}

	// Display Healthbar 
	void DisplayHealth(float _health)
	{
		GameManager.instance.DisplayHealth(_health);
	}

	// Display GameOver
	void DisplayGameOver()
	{
		GameManager.instance.GameOver();
	}
	public void Play()
	{
		Health = maxHealth;
		GameOver = false;
		IsHealthMode = false;
	}

	public void TakeDamages(bool damageStatus)
	{
		StopCoroutine("TakeDamage");
		if (damageStatus)
		{
			StartCoroutine("TakeDamage");
		}
	}

	public void EvaluateIsHealthMode(bool decision)
	{
		IsHealthMode = decision;
	}

	public void CallStopAllCoroutines()
    {
		StopAllCoroutines();
	}

	public void DeathOfFall()
	{
		Health = 0f;
	}

	public void DamageFromFall(float _fallHeight)
    {
		damage = Mathf.InverseLerp(6f, 30f, _fallHeight);
		damage = maxHealth * damage;
		//Health -= damage;
		TakeDamage(damage);
    }

    public void TakeDamage(float _damage)
    {
		Health -= _damage;
	}
}
public interface IHealth
{
	void TakeDamage(float _damage);
}