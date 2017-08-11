using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorBridge : MonoBehaviour
{
    public Player MainPlayer;

    public void ShootingEvent()
    {
        MainPlayer.ShootingEvent();
    }
}
