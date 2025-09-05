using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

    [SerializeField] private Text musicText, soundText, controllerText;
    [SerializeField] private bool musicStatus = true;
    [SerializeField] private bool soundStatus = true;
    [SerializeField] private bool controllerStatus;

	void Start()
	{
		AudioSetup();
		ControllerState();
	}
	public bool ControllerStatus
	{
		get
		{
			return controllerStatus;
		}
		set
		{
			controllerStatus = value;
			controllerText.text = controllerStatus ? "JoyStick" : "Keyboard";
		}
	}

	string controllerKey = "controller";
	public GameObject inputPanel;
	public void IsKeyboardInput(bool status)
    {
		GameManager.instance.IsKeyboardInput(status);
	}
	public void ControllerButton()
	{
		ControllerStatus = !ControllerStatus;
		IsKeyboardInput(ControllerStatus);
		inputPanel.SetActive(!ControllerStatus);
		PlayerPrefs.SetString(controllerKey, ControllerStatus ? "key" : "joy");
	}
	public void ControllerState()
	{
		ControllerStatus = PlayerPrefs.GetString(controllerKey) == "key" ? true : false;
		IsKeyboardInput(ControllerStatus);
		inputPanel.SetActive(!ControllerStatus);
	}

	#region Music Stuff...
	string musicKey = "music";
	string soundKey = "sound";
	public bool MusicStatus
	{
		get
		{
			return musicStatus;
		}
		set
		{
			musicStatus = value;
			musicText.text = musicStatus ? "Music : On" : "Music : Off";
			BgController(musicStatus);
		}
	}
	public bool SoundStatus
	{
		get
		{
			return soundStatus;
		}
		set
		{
			soundStatus = value;
			soundText.text = soundStatus ? "Sound : On" : "Sound : Off";
			PlayPauseSound(soundStatus);
		}
	}
	public void SoundBtn()
	{
		SoundStatus = !SoundStatus;
		PlayerPrefs.SetInt(soundKey, SoundStatus ? 1 : 0);
	}
	public void MusicBtn()
	{
		MusicStatus = !MusicStatus;
		PlayerPrefs.SetInt(musicKey, MusicStatus ? 1 : 0);
	}
	void AudioSetup()
	{
		MusicStatus = PlayerPrefs.GetInt(musicKey) == 1 ? true : false;
		SoundStatus = PlayerPrefs.GetInt(soundKey) == 1 ? true : false;
	}
	#endregion
	public void BgController(bool decision)
	{
		GameManager.instance.BgController(decision);
	}
	public void PlayPauseSound(bool playPause)
	{
		GameManager.instance.PlayPauseSound(playPause);
	}
	public void ButtonSound()
	{
		GameManager.instance.PlaySound("button");
	}
}
