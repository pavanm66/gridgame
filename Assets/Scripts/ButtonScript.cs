using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public int sceneIndex;
	Button button;
	Text btntext;

	private void Awake()
	{
		button = GetComponent<Button>();
		btntext = GetComponentInChildren<Text>();
	}
	public void Initialise(int _sceneIndex, LevelManager _levelManager)
	{
		sceneIndex = _sceneIndex;
		button.onClick.AddListener(() => _levelManager.LoadLevel(sceneIndex));
		btntext.text = "Level " + (_sceneIndex);
	}
}
