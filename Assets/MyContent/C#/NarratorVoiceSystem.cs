using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorVoiceSystem : MonoBehaviour
{
    public AudioClip IncomingEnemyUnitsAudioClip;
    public AudioClip LifeSupportSystemRepairedAudioClip;
    public AudioClip MissionFailedAudioClip;
    public AudioClip ShieldOnlineAudioClip;

    private AudioSource _audio;

    public enum MessageType
    {
        IncomingEnemyUnits,
        LifeSupportSystemRepaired,
        MissionFailed,
        ShieldOnline
    }

	void Start ()
	{
	    _audio = GetComponent<AudioSource>();
	}

    public void Say (MessageType message)
    {
        switch (message)
        {
            case MessageType.IncomingEnemyUnits:
                _audio.clip = IncomingEnemyUnitsAudioClip;
                break;
            case MessageType.LifeSupportSystemRepaired:
                _audio.clip = LifeSupportSystemRepairedAudioClip;
                break;
            case MessageType.MissionFailed:
                _audio.clip = MissionFailedAudioClip;
                break;
            case MessageType.ShieldOnline:
                _audio.clip = ShieldOnlineAudioClip;
                break;
        }

        _audio.Play();
    }
}
