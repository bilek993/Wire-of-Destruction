using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public float MaxAudioVolume = 0.3f;
    public AudioSource CurrentAudioSource;

	void Start () {
		
	}

	void FixedUpdate ()
    {
        if (CurrentAudioSource.volume < MaxAudioVolume)
        {
            CurrentAudioSource.volume += Time.fixedDeltaTime * 0.05f;
        }
	}
}
