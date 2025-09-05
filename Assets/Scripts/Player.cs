using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	public Player player;
	
	public HealthScript healthScript;
	public Rigidbody rigidbdy;
	public JoyStickController joyStick;
	public Gun gun;

	public LayerMask groundLayer;
	public float magnitude;
	public float horizontal;
	public float vertical;
	public float moveFront;
	public float fastSpeed;
	public float rotateSpeed;
	private Vector3 startingPosition;
	public GameObject coinParticle;
	public PowerType powerType;

	private int score = 1;
	private bool play = false;
	public float jumpLevel;

	[Range(0, 1f)] public float activeTimer;
	private float maxTimer = 3f;
	private float timer;
	public bool isGround;
	public bool keyboard;
	[SerializeField] bool isPause = false;
	[SerializeField] float shootTime, shootingRate = 0.1f;

	public float ActiveTimer
	{
		get
		{
			return activeTimer;
		}
		set
		{
			activeTimer = value;

			DisplayPowerUpTimer(activeTimer, timer);
		}
	}

	void Start()
	{
		fastSpeed = moveFront * 2;
		startingPosition = transform.position;
		rigidbdy = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		if (play && !isPause)
		{
			Movement(InputDirection());
			Rotation(InputDirection());

            if (!keyboard)
            {
				//if (CrossPlatformInputManager.GetButton("Shoot"))
				//{
				//	ShootBtn();
				//}
				//if (CrossPlatformInputManager.GetButton("Jump"))
				//{
				//	JumpBtn();
				//}
            }
            else
            {
				Shoot();
				Jump();
			}
			
			shootTime += Time.deltaTime;
			IsFalling = !isGround;
		}
	}
	
	public void IsKeyboardInput(bool status)
	{
		keyboard = status;

	}
	Vector3 InputDirection()
    {
        if (keyboard)
        {
			horizontal = Input.GetAxis("Horizontal");
			vertical = Input.GetAxis("Vertical");
		}
        else
        {
			horizontal = JoyStickController.moveDirection.x;
			vertical = JoyStickController.moveDirection.y;
		}

        Vector3 input = new Vector3(horizontal, 0f, vertical);
		return input;
		
	}
	void Movement(Vector3 direction)
    {
		Vector3 move = direction;

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			moveFront = fastSpeed;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			moveFront = fastSpeed/2;
		}
		
		magnitude = move.magnitude;
		transform.position += transform.forward * magnitude * moveFront * Time.deltaTime;
	}
	void Rotation(Vector3 rotation)
	{
		Vector3 twoDInput = rotation;
		if (twoDInput.magnitude > 0)
		{
			Quaternion from = transform.rotation;
			Quaternion to = Quaternion.LookRotation(twoDInput);
			transform.rotation = Quaternion.Lerp(from, to, Time.deltaTime * 2f * rotateSpeed);
		}
	}
	void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
			JumpBtn();
        }
    }
	public void JumpBtn()
    {
		isGround = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
		Debug.DrawRay(transform.position, Vector3.down, Color.green);
		if (isGround)
        {
			rigidbdy.AddForce(Vector3.up * jumpLevel, ForceMode.Impulse);
		}
    }
	
	
	public void Shoot()
    {
		if (Input.GetMouseButtonDown(0))
		{
			ShootBtn();
		}
    }
	public void ShootBtn()
    {
		if (play && !isPause)
		{
			if (!gun)
			{
				return;
			}
			if (shootTime > shootingRate)
			{
				shootTime = 0;
				gun.Shoot();
			}
		}
	}
	
	public void FinishCoin()
    {
		Invoke("WinGame", 0.5f);
	} 
	// Collider
	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishCoin")
        {
			FinishCoin();
			other.gameObject.SetActive(false);
			PlayParticleEffect(other.gameObject.transform.position);
			AddScore(score);
			PlaySound("coin");
		}
        if (other.gameObject.tag == "Coin")
        {
			other.gameObject.SetActive(false);
			PlayParticleEffect(other.gameObject.transform.position);
			AddScore(score);
			PlaySound("coin");
			ReduceCount();
		}
        if (other.gameObject.tag == "Healer")
        {
			EvaluateIsHealthMode(true);
		}
		if (other.gameObject.tag == "powerUp")
		{
			
            if (powerType != PowerType.none)
            {
				print("You are Already in PowerUp mode");
			}
            else
            {
				PlaySound("powerUp");
				powerType = other.gameObject.GetComponent<PowerUp>().powerType;
				other.gameObject.SetActive(false);
				DisplayPowerUpText(other.gameObject.name);
				switch (powerType)
				{
					case PowerType.none:
						//DisplayPowerUpText("None");
						break;
					case PowerType.doubleCoin:
						StartCoroutine("MultiScore");
						break;
					case PowerType.immunity:
						StartCoroutine("ImmunityBox");
						break;
					case PowerType.doubleSpeed:
						StartCoroutine("DoubleSpeed");
						break;
					default:
						break;
				}
			}
        }
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Healer")
		{
			EvaluateIsHealthMode(false);
		}
	}

    #region PowerUps
    IEnumerator ImmunityBox()
    {
		TakeDamages(false);
		timer = maxTimer;
        while (timer > 0f)
        {
			timer -= Time.deltaTime;
			ActiveTimer = timer / maxTimer;
			yield return new WaitForSeconds(Time.deltaTime);
        }
		TakeDamages(true);
		powerType = PowerType.none;
    }
	IEnumerator DoubleSpeed()
    {
		moveFront = moveFront * 3;
		timer = maxTimer;
		while (timer > 0f)
        {
			timer -= Time.deltaTime;
			ActiveTimer = timer / maxTimer;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		moveFront = moveFront / 3;
		powerType = PowerType.none;
	}
	IEnumerator MultiScore()
	{
		score = 10;
		timer = maxTimer;
		while (timer > 0f)
		{
			timer -= Time.deltaTime;
			ActiveTimer = timer / maxTimer;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		score = 1;
		powerType = PowerType.none;
	}
	#endregion

	public void DisplayPowerUpTimer(float activeTimer, float timer)
	{
		GameManager.instance.DisplayPowerUpTimer(activeTimer, timer);
	}
	public void DisplayPowerUpText(string powerUp)
	{
		GameManager.instance.DisplayPowerUpText(powerUp);
	}
	void AddScore(int _score)
	{
		GameManager.instance.AddScore(_score);
	}

 
	public void Play()
    {
		play = true;
		HealthScriptPlay();
	}
	public void Replay()
	{
		transform.SetPositionAndRotation(startingPosition, Quaternion.AxisAngle(Vector3.forward, 0f));
		//joyStick.ResetJoystickPosition();
		ResetFollowCam();
		Play();
		rigidbdy.useGravity = true;
		rigidbdy.Sleep();
		print("sleep here");
	}
	void HealthScriptPlay()
    {
		healthScript.Play();
    }
	void ResetFollowCam()
    {
		GameManager.instance.ResetFollowCam();
    }
	void ReduceCount()
    {
		GameManager.instance.ReduceCount();
        if (GameManager.instance.GetCoinCount()==0)
        {
			Invoke("WinGame", 0.5f);
        }
	}
	public void WinGame()
	{
		StopAllCoroutines();
		play = false;
		CallStopAllCoroutines();
		GameManager.instance.WinGame();
	}

	public void TakeDamages(bool damageStatus)
    {
		healthScript.TakeDamages(damageStatus);
    }
	public void CallStopAllCoroutines()
	{
		healthScript.CallStopAllCoroutines();
	}
	public void GameOver()
	{
		StopAllCoroutines();
		play = false;
		CallStopAllCoroutines();
	}
	public void EvaluateIsHealthMode(bool decision)
	{
		healthScript.EvaluateIsHealthMode(decision);
	}
	private void PlayParticleEffect(Vector3 position)
	{
		GameManager.instance.PlayParticleEffect(position);
	}

	public void PlaySound(string soundName)
	{
		GameManager.instance.PlaySound(soundName);
	}

	public bool isFalling;
	public float airTime;
	public float fallHeight;

	public bool IsFalling
    {
        get
        {
			return isFalling;
        }
		set
        {
            if (isFalling != value)
			{
				isFalling = value;
                if (!isFalling)
                {
					fallHeight = -(airTime*airTime * Physics.gravity.y) / 2f; // formula
                    if (fallHeight > 5f)
                    {
						DamageFromFall(fallHeight);
					}
                }
            }
			if (value && rigidbdy.velocity.y < 0f)
			{
				airTime += Time.deltaTime;
				if (airTime > 2.5f)
				{
					DeathOfFall();
				}
			}
			else
			{
				airTime = 0f;
			}
		}
    }
	public void DeathOfFall()
    {
		airTime = 0f;
		rigidbdy.useGravity = false;
		rigidbdy.Sleep();
		healthScript.DeathOfFall();
    }
	public void DamageFromFall(float _fallHeight)
    {
		healthScript.DamageFromFall(_fallHeight);
	}

	
	public void CanGamePause(bool decision)
    {
		isPause = decision;
    }

    
}	
