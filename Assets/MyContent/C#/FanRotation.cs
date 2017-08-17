using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public float Speed = 90f;

	void FixedUpdate ()
    {
        transform.RotateAround(transform.position, transform.forward, Time.fixedDeltaTime * Speed);
    }
}
