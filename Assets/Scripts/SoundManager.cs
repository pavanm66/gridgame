using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioSource BgAudio;

	public AudioSource[] audioSources;
	void Awake () {
		audioSources = new AudioSource[transform.childCount];
		for (int i = 0; i < audioSources.Length; i++)
		{
			audioSources[i] = transform.GetChild(i).GetComponent<AudioSource>();
		}
		BgAudio = GetComponent<AudioSource>();
	}

	private bool playSound = true;
	public void PlaySound(string soundName)
    {
        if (playSound)
        {
			foreach (AudioSource audioSource in audioSources)
			{
				if (audioSource.name == soundName)
				{
					audioSource.Play();
					return;
				}
			}
		}
        else
        {
			return;
        }
    }
	public void PlayPauseSound(bool playPause)
    {
		playSound = playPause;
    }
	public void BgController(bool decision)
    {

		if (decision)
        {
			BgAudio.Play();
        }
        else
        {
			BgAudio.Pause();
        }
    }
}
