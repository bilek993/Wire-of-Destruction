using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float MovementSpeed = 7;
    public float JumpVelocity = 3;
    public Camera PlayerCamera;
    public float MaxCameraAngel = 75;
    public float MinCameraAngel = -75;
    public AudioSource LandingAudioSource;
    public float MaximumLandingForce = 11;
    public Animator PlayerAnimator;

    private Rigidbody _playerRigidbody;
    private bool _canDoubleJump;
    private float _distanceToGround;
    private float _currentRotationX;
    private bool _isGrounded;
    private bool _animationLandingIsPlaying;


    void Start () 
	{
		_playerRigidbody = GetComponent<Rigidbody>();
	    _distanceToGround = GetComponent<CapsuleCollider>().height / 2;

        // TODO: Move cursor logic to another class
        MouseHelper.DisableCursor();
	}

	void Update () 
	{
	    if (!_animationLandingIsPlaying)
	    {
	        Movement();
	        MouseLook();
	        Jumping();
        }
	}

    IEnumerator PlayLandingAnimation()
    {
        _animationLandingIsPlaying = true;

        Animator animator = PlayerCamera.GetComponent<Animator>();
        animator.enabled = true;
        PlayerAnimator.gameObject.SetActive(false);

        animator.SetTrigger("TriggerAnimation");
        yield return new WaitForSeconds(1f);
        animator.enabled = false;
        _currentRotationX = 0;
        PlayerAnimator.gameObject.SetActive(true);
        PlayerAnimator.SetTrigger("EquiptFast");

        _animationLandingIsPlaying = false;
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
        _currentRotationX  = Mathf.Clamp(_currentRotationX, MinCameraAngel, MaxCameraAngel);
        PlayerCamera.transform.localEulerAngles = new Vector3(_currentRotationX, 0, 0);
    }

    private void Jumping()
    {
        _isGrounded = Physics.Raycast(transform.position, -Vector3.up, _distanceToGround + 0.1f);

        if (Input.GetButtonDown("Jump"))
        {

            if (_isGrounded)
            {
                Jump();
            }
            else if (_canDoubleJump)
            {
                Jump();
                _canDoubleJump = false;
            }
        }

        if (_isGrounded)
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.y >= MaximumLandingForce)
        {
            LandingAudioSource.volume = 1;

            if (!_animationLandingIsPlaying)
                SendMessage("PlayLandingAnimation");
        }
        else
        {
            LandingAudioSource.volume = collision.impulse.y / MaximumLandingForce;
        }

        LandingAudioSource.Play();
    }
}
