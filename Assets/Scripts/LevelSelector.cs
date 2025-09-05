using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
	public LevelManager levelManager;
	public GameObject btnPrefab;

	void Awake()
    {
		levelManager = FindObjectOfType<LevelManager>();
	}
	void Start()
	{
		int sceneCount = GetSceneCount();
		for (int i = 1; i < sceneCount; i++)
		{
			GameObject btn = Instantiate(btnPrefab, transform);
			btn.GetComponentInChildren<ButtonScript>().Initialise(i, levelManager);
		}
	}

	public int GetSceneCount()
    {
		return levelManager.GetSceneCount();
    }
}