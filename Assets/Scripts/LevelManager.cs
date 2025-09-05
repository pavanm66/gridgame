using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	int totalScenes;
	int sceneIndexToLoad;
	int currentBuiltIndex;

	void Awake()
    {
		totalScenes = SceneManager.sceneCountInBuildSettings;
	}
	void Start () {
		
		currentBuiltIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuiltIndex > 0)
        {
			Play();
        }
		Display(currentBuiltIndex);
	}
	
	public void LoadNextLevel()
    {
		sceneIndexToLoad = currentBuiltIndex+1;
		print("load "+sceneIndexToLoad);

        if (sceneIndexToLoad < totalScenes)
		{
			SceneManager.LoadScene(sceneIndexToLoad);
        }
        else
        {
			print("More Level Are Coming soon");
        }
    }
	public void LoadFirstLevel()
    {
		SceneManager.LoadScene(0);
	}
	public void LoadPreviousLevel()
    {
		sceneIndexToLoad = currentBuiltIndex - 1;
        if (sceneIndexToLoad > 0)
        {
			SceneManager.LoadScene(sceneIndexToLoad);
        }
		else
        {
			print("No More Scenes to load");
        }
    }
	void Play(){
		GameManager.instance.Play();
    }
	void Display(int levelNo)
    {
		GameManager.instance.uIManager.DisplaylevelNo(levelNo);
    }
	public int GetCurrentLevel()
    {
		return currentBuiltIndex;
    }
	public int GetSceneCount()
    {
		return totalScenes;
    }
	public void LoadLevel(int _sceneIndex)
	{
		print(_sceneIndex);
		SceneManager.LoadScene(_sceneIndex);
	}
}
