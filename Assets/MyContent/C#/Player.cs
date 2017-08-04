using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float MovementSpeed = 7;
    public Camera PlayerCamera;

	private Rigidbody _rigidbodyThis;

	void Start () 
	{
		_rigidbodyThis = GetComponent<Rigidbody>();
        // TODO: Move cursor logic to another class
        MouseHelper.DisableCursor();
	}

	void Update () 
	{
        Movement();
        MouseLook();
	}

    private void Movement()
    {
        Vector3 movement = transform.forward * (Input.GetAxis("Vertical"));
        movement += transform.right * (Input.GetAxis("Horizontal"));
        movement *= MovementSpeed;

        NormalizeMovement(ref movement);

        movement = transform.position + (movement * Time.deltaTime);
        _rigidbodyThis.MovePosition(movement);
    }

    private void MouseLook()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
        PlayerCamera.transform.Rotate(-Input.GetAxis("Mouse Y"), 0, 0);
    }

    private void NormalizeMovement(ref Vector3 vectorToNormalize)
    {
        float pythagoras = ((vectorToNormalize.x * vectorToNormalize.x) + (vectorToNormalize.z * vectorToNormalize.z));

        if (pythagoras > MovementSpeed * MovementSpeed)
        {
            float magnitude = Mathf.Sqrt(pythagoras);
            float multiplier = MovementSpeed / magnitude;
            vectorToNormalize.x *= multiplier;
            vectorToNormalize.z *= multiplier;
        }
    }
}
