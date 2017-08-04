using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 7;

	Rigidbody rigidbodyThis;

	void Start () 
	{
		rigidbodyThis = GetComponent<Rigidbody>();
	}

	void Update () 
	{
		Vector3 movement = transform.forward * (Input.GetAxis("Vertical"));
		movement += transform.right * (Input.GetAxis("Horizontal"));
        movement *= speed;

        NormalizeMovement(ref movement);

        movement = transform.position + (movement * Time.deltaTime);
        rigidbodyThis.MovePosition(movement);
	}

    void NormalizeMovement(ref Vector3 vectorToNormalize)
    {
        float pythagoras = ((vectorToNormalize.x * vectorToNormalize.x) + (vectorToNormalize.z * vectorToNormalize.z));

        if (pythagoras > speed * speed)
        {
            float magnitude = Mathf.Sqrt(pythagoras);
            float multiplier = speed / magnitude;
            vectorToNormalize.x *= multiplier;
            vectorToNormalize.z *= multiplier;
        }
    }
}
