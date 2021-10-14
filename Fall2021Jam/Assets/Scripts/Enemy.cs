using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity
{
    private RoomEvent roomEvent;

    public void SetRoomEvent(RoomEvent re) { this.roomEvent = re; }

    public override void Die()
    {
        roomEvent.ProgressRoom();
        base.Die();
    }
}
