using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject[] Receivers;
    public string[] Messages;

    private void OnTriggerEnter(Collider other)
    {
        if (Receivers.Length != Messages.Length)
        {
            print("Messages array length != Receivers array length. Return called!");
            return;
        }

        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < Receivers.Length; i++)
            {
                Receivers[i].SendMessage(Messages[i]);
            }
        }
    }
}
