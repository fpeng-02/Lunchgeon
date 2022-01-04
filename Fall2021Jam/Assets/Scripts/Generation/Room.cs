using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private List<Vector2> fill;
    [SerializeField] private List<DoorCoord> doorCoord;

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

    /// <summary>
    /// Configures a room to enable a door specfied by an index of the doorCoord List. 
    /// </summary>
    /// <param name="doorIndex"></param>
    public void UnblockDoor(int doorIndex) { doorCoord[doorIndex].UnblockDoor(); }

    
    /// <summary>
    /// Enables a door when we don't know its index by matching requested coord & dir with that of stored DoorCoords; 
    /// this method assumes that door coord and direction is "unique" 
    /// (there are no two doors with the exact same coord & dir, which is a reasonable assumption).
    /// </summary>
    /// <param name="doorCoord"></param>
    public void UnblockDoor(DoorCoord doorCoord) 
    {
        foreach(DoorCoord testDoorCoord in this.doorCoord) {
             if (testDoorCoord.GetCoord().Equals(doorCoord.GetCoord()) && testDoorCoord.GetDir().Equals(doorCoord.GetDir())) {
                testDoorCoord.UnblockDoor();
                break;
            }
        }
    }
}
