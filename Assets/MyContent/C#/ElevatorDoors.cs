using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoors : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;
    public double delayTime = 0.1;

    private Vector3 _door1InitialPosition;
    private Vector3 _door2InitialPosition;
    private bool _closed;

    public void OpenDoors()
    {
        iTween.MoveBy(Door1, iTween.Hash("x", -1.5, "easeType", "easeInOutQuad", "delay", delayTime, "speed", 0.4));
        iTween.MoveBy(Door2, iTween.Hash("x", 1.5, "easeType", "easeInOutQuad", "delay", delayTime, "speed", 0.4));
    }

    public void CloseDoors()
    {
        if (!_closed)
        {
            _closed = true;
            iTween.MoveBy(Door2, iTween.Hash("x", -1.5, "easeType", "easeInOutQuad", "delay", delayTime, "speed", 0.4));
            iTween.MoveBy(Door1, iTween.Hash("x", 1.5, "easeType", "easeInOutQuad", "delay", delayTime, "speed", 0.4));
        }
    }
}
