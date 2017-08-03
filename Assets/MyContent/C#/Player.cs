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
		Vector3 movement = transform.forward * (Input.GetAxis("Vertical") * Time.deltaTime * speed);
		movement += transform.right * (Input.GetAxis("Horizontal") * Time.deltaTime * speed);
		rigidbodyThis.MovePosition(transform.position + movement);
	}
}
