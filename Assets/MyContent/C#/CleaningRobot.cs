using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningRobot : MonoBehaviour
{
    public Transform LightTube;

	void Start () {
		
	}
	
	void FixedUpdate () {
		LightTube.RotateAround(LightTube.forward, 0.05f * Time.timeScale);
	}
}
