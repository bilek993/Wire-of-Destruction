using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothGunMovement : MonoBehaviour
{
    public float MovementScale = 0.02f;
    public float MaximumMove = 0.03f;
    public float TimeMultiplier = 5;

    private Vector3 _initialPositionVector3;

    void Start()
    {
        _initialPositionVector3 = transform.localPosition;
    }

    void Update()
    {
        Vector2 rotation = new Vector2(-Input.GetAxis("Mouse X") * MovementScale, -Input.GetAxis("Mouse Y") * MovementScale);

        rotation.x = Mathf.Clamp(rotation.x, -MaximumMove, MaximumMove);
        rotation.y = Mathf.Clamp(rotation.y, -MaximumMove, MaximumMove);

        transform.localPosition = Vector3.Lerp(transform.localPosition,
            new Vector3(rotation.x, rotation.y, 0) + _initialPositionVector3,
            Time.deltaTime * TimeMultiplier);
    }
}
