using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorIndicator : MonoBehaviour
{
    public Material LcdMaterial;
    public float StartValue = 1.94f;
    public float EndValue = 0.0125f;

    private float _currentOffset;

	void Start ()
	{
	    _currentOffset = StartValue;
		LcdMaterial.SetTextureOffset("_MainTex", new Vector2(0, StartValue));
	}
	
	void FixedUpdate ()
    {
        if (_currentOffset > EndValue)
        {
            _currentOffset -= Time.fixedDeltaTime * (_currentOffset - EndValue) * 1.75f;
            LcdMaterial.SetTextureOffset("_MainTex", new Vector2(0, _currentOffset));
        }
	}
}
