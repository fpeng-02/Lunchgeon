using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [SerializeField] private RoomEvent roomEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        roomEvent.StartRoom();
    }
}
