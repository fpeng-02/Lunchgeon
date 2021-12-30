    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Vector2> fill;
    [SerializeField] private List<DoorCoord> doorCoord;

    [SerializeField] private List<GameObject> fillerWalls;

    public void Start()
    {
    }

    //public GameObject GetGameObject() { return this.gameObject; }

    public List<Vector2> GetFill()
    {
        List<Vector2> newList = new List<Vector2>(fill);
        return newList;
    }
    public List<DoorCoord> GetDoorCoords()
    {
        List<DoorCoord> newList = new List<DoorCoord>(doorCoord);
        return newList;
    }

    /*public List<Vector2> GetOffsetCoord(Vector2 offset)
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
    }*/
}
