using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Player player;
	public UIManager uIManager;
	public CoinManager coinManager;
	public LevelManager levelManager;
	public ScoreManager scoreManager;
	public PowerUpManager powerUpManager;
	public ParticalManager particalManager;
	public SoundManager soundManager;
	//public TurretManager turretManager;

	public FollowCamera followCamera;

	
	[SerializeField] private Gradient colorGradient;
	public static GameManager instance;

	void Awake()
    {
        if (instance == null)
        {
			instance = this;
        }
        else
        {
			Destroy(gameObject);
        }

		player = FindObjectOfType<Player>();
		uIManager = FindObjectOfType<UIManager>();
		coinManager = FindObjectOfType<CoinManager>();
		levelManager = FindObjectOfType<LevelManager>();
		scoreManager = FindObjectOfType<ScoreManager>();
		powerUpManager = FindObjectOfType<PowerUpManager>();
		particalManager = FindObjectOfType<ParticalManager>();
		soundManager = FindObjectOfType<SoundManager>();
		//turretManager = FindObjectOfType<TurretManager>();
		followCamera = FindObjectOfType<FollowCamera>();
	}

	public void AddScore(int _score)
	{
		scoreManager.AddScore(_score);
	}
	public void ResetScore()
	{
		scoreManager.ResetScore();
	}
	public void DisplayScore(int _score)
	{
		uIManager.DisplayScore(_score);
	}
	public void DisplayHealth(float _health)
	{
		uIManager.DisplayHealth(_health);
	}
	public void DisplayPowerUpText(string powerUp)
	{
		uIManager.DisplayPowerUpText(powerUp);
	}
	public void DisplayPowerUpTimer(float activeTimer, float timer)
	{
		uIManager.DisplayPowerUpTimer(activeTimer, timer);
	}

    internal void GameOver()
    {
		//if (turretManager)
		//{
		//	turretManager.GameOver();
		//}
		player.GameOver();
		//CheckPlayerStatus(false);
		ResetScore();
		uIManager.SwitchPanel(PanelType.gameOver);
	}

    public void WinGame()
	{
		uIManager.SwitchPanel(PanelType.gameWin);
	}
	public void ReduceCount() 
	{
		coinManager.ReduceCount();
	}
	public int GetCoinCount()
	{
		return coinManager.GetCoinCount();
    }
	public void LoadNextlevel()
    {
		levelManager.LoadNextLevel();
    }
	public void LoadPreviousLevel()
	{
		levelManager.LoadPreviousLevel();
	}
	public void LoadFirstLevel()
	{
		levelManager.LoadFirstLevel();
	}
	public void PlayParticleEffect(Vector3 position)
	{
		if (particalManager != null)
		{
			particalManager.PlayParticleEffect(position);
		}
	}
	public void Play()
	{
        if (levelManager.GetCurrentLevel()>0)
        {
			print(levelManager.GetCurrentLevel());
			uIManager.SwitchPanel(PanelType.hud);
			player.Play();
		}
		else
        {
			LoadNextlevel();
		}
	}
	public void Replay()
    {
		powerUpManager.ResetPowerUps();
		coinManager.ResetCoins();
  //      if (turretManager != null)
  //      {
		//	turretManager.Replay();
		//}
		Play();
    }
	public void CheckPlayerStatus(bool _isPlayerAlive)
	{
		//turretManager.CheckPlayerStatus(_isPlayerAlive);
	}
	public void ResetFollowCam()
	{
		followCamera.Replay();
	}
	public void IsKeyboardInput(bool status)
	{
		player.IsKeyboardInput(status);
	}
	public void BgController(bool decision)
	{
		soundManager.BgController(decision);
	}
	public void PlaySound(string soundName)
    {
		soundManager.PlaySound(soundName);
	}
	public void PlayPauseSound(bool playPause)
	{
		soundManager.PlayPauseSound(playPause);
	}
	public Color DisplayColorGradient(float _percentage)
    {
		return colorGradient.Evaluate(_percentage);
	}
	public void CanGamePause(bool decision)
	{
		player.CanGamePause(decision);
	}
}
