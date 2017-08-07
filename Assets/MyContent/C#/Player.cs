using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float MovementSpeed = 7;
    public float JumpVelocity = 3;
    public Camera PlayerCamera;
    public float maxCameraAngel = 75;
    public float minCameraAngel = -75;

    private Rigidbody _playerRigidbody;
    private bool _canDoubleJump = false;
    private float _distanceToGround;
    private float _currentRotationX;

	void Start () 
	{
		_playerRigidbody = GetComponent<Rigidbody>();
	    _distanceToGround = GetComponent<CapsuleCollider>().height / 2;

        // TODO: Move cursor logic to another class
        MouseHelper.DisableCursor();
	}

	void Update () 
	{
        Movement();
        MouseLook();
        Jumping();
	}

    private void Movement()
    {
        Vector3 movement = transform.forward * (Input.GetAxis("Vertical"));
        movement += transform.right * (Input.GetAxis("Horizontal"));
        movement *= MovementSpeed;

        NormalizeMovement(ref movement);

        movement = transform.position + (movement * Time.deltaTime);
        _playerRigidbody.MovePosition(movement);
    }

    private void MouseLook()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));

        _currentRotationX -= Input.GetAxis("Mouse Y");
        _currentRotationX  = Mathf.Clamp(_currentRotationX, minCameraAngel, maxCameraAngel);
        PlayerCamera.transform.localEulerAngles = new Vector3(_currentRotationX, 0, 0);
    }

    private void Jumping()
    {
        bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, _distanceToGround + 0.1f);

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded)
            {
                Jump();
            }
            else if (_canDoubleJump)
            {
                Jump();
                _canDoubleJump = false;
            }
        }

        if (isGrounded)
            _canDoubleJump = true;
    }

    private void Jump()
    {
        _playerRigidbody.velocity = Vector3.up * JumpVelocity;
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
