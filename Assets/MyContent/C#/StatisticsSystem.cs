using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StatisticsSystem : MonoBehaviour
{
    public GameObject Canvas;
    public AudioSource MainAudioSource;
    public Text FramesText;
    public Text FramesFixedText;
    public LineRenderer GraphLine;

    private bool _isEnabled;

    void Start()
    {
        SendMessage("UpdateGraph");

        for (int i = 0; i < GraphLine.positionCount; i++)
        {
            Vector3 point = GraphLine.GetPosition(i);
            point.x = (float)i / (float)GraphLine.positionCount;
            point.x = (point.x * 11) - 5.5f;
            GraphLine.SetPosition(i, point);
        }
    }

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
        FramesText.text = "Timing: " + (1 / Time.smoothDeltaTime).ToString("0.00") + "FPS (" + Time.smoothDeltaTime.ToString("0.00") + "ms)";
    }

    private void StatFramesFixed()
    {
        FramesFixedText.text = "Fixed timing: " + (1 / Time.fixedDeltaTime).ToString("0.00") + "FPS (" + Time.fixedDeltaTime.ToString("0.00") + "ms)";
    }

    IEnumerator UpdateGraph()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Vector3 point;

            for (int i = 0; i < GraphLine.positionCount - 1; i++)
            {
                point = GraphLine.GetPosition(i);
                Mathf.Clamp(GraphLine.GetPosition(i + 1).z, 0, 5.1f);
                point.z = GraphLine.GetPosition(i + 1).z;
                GraphLine.SetPosition(i, point);
            }

            point = GraphLine.GetPosition(GraphLine.positionCount - 1);
            point.z = ((1 / Time.smoothDeltaTime) / 60) * 5;
            GraphLine.SetPosition(GraphLine.positionCount - 1, point);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
