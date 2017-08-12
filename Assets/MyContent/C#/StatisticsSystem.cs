using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsSystem : MonoBehaviour
{
    public GameObject Canvas;
    public AudioSource MainAudioSource;
    public Text FramesText;
    public Text FramesFixedText;

    private bool _isEnabled;
	
	void Update ()
	{
	    if (Input.GetKeyUp(KeyCode.BackQuote))
	    {
	        _isEnabled = !_isEnabled;
            Canvas.SetActive(_isEnabled);
	    }

	    if (_isEnabled)
	    {
	        StatFrames();
	    }
	}

    void FixedUpdate()
    {
        if (_isEnabled)
        {
            StatFramesFixed();
        }
    }

    private void StatFrames()
    {
        FramesText.text = "Timing: " + (1 / Time.smoothDeltaTime).ToString("#0.00") + "FPS (" + Time.smoothDeltaTime.ToString("#0.00") + "ms)";
    }

    private void StatFramesFixed()
    {
        FramesFixedText.text = "Fixed timing: " + (1 / Time.fixedDeltaTime).ToString("#0.00") + "FPS (" + Time.fixedDeltaTime.ToString("#0.00") + "ms)";
    }
}
