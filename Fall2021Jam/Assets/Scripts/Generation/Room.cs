using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Vector2> coord;
    [SerializeField] private List<DoorCoord> doorCoord;

    public List<Vector2> GetCoord()
    {
        List<Vector2> newList = new List<Vector2>(coord);
        return newList;
    }
    public List<DoorCoord> GetDoors()
    {
        List<DoorCoord> newList = new List<DoorCoord>(doorCoord);
        return newList;
    }

    public List<Vector2> GetOffsetCoord(Vector2 offset)
    {
        List<Vector2> newList = new List<Vector2>(coord);
        newList.ForEach(c => c = c + offset);
        return newList;
    }

    public List<DoorCoord> GetOffsetDoorCoord(Vector2 offset)
    {
        List<DoorCoord> newList = new List<DoorCoord>(doorCoord);
        newList.ForEach(c => c.setCoord(c.GetCoord() + offset));
        return newList;
    }

}
