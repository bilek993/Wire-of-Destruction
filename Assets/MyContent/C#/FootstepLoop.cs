using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepLoop : MonoBehaviour
{

    public AudioSource FootRightAudioSource;
    public AudioSource FootLeftAudioSource;
    public bool IsWalking;
    public float maxVolume = 0.3f;

    private bool _isLeft = true;
    private float _timeToPlayNext;

	void Start ()
	{
	    _timeToPlayNext = FootRightAudioSource.clip.length / 3 * 2;
        setVolume(0f);
	    SendMessage("footLoop");
    }

    void FixedUpdate()
    {
        float currentVolume = FootRightAudioSource.volume;

        if (IsWalking && currentVolume < maxVolume)
            setVolume(currentVolume + Time.fixedDeltaTime * 2);
        else if (!IsWalking && currentVolume > 0)
            setVolume(currentVolume - Time.fixedDeltaTime * 2);
    }

    IEnumerator footLoop()
    {
        _isLeft = !_isLeft;
        if (!_isLeft)
            FootRightAudioSource.Play();
        else
            FootLeftAudioSource.Play();

        yield return new WaitForSeconds(_timeToPlayNext);
        SendMessage("footLoop");
    }

    private void setVolume(float volume)
    {
        FootRightAudioSource.volume = volume;
        FootLeftAudioSource.volume = volume;
    }
}
