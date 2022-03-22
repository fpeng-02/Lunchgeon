using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Class: DoorCoord
 * Doorcoord class that contains the tile the door is on and the cardinal direction the door is facing
*/
[System.Serializable]
public class DoorCoord 
{
    //local vars
    [SerializeField] private Vector2 doorCoord;  //coords of the door
    [SerializeField] private Vector2 doorDir;    //direction the door is facing (make sure this is (+-1,0) or (0,+-1)) 
    [SerializeField] private GameObject physicalDoor;
    [SerializeField] private GameObject physicalFiller;
    private bool filled = true;        //is the door filled already? (default false)

    //constructor
    public DoorCoord(Vector2 coord, Vector2 dir)
    {
        doorCoord = coord;
        doorDir = dir;
        filled = true;
    }

    //getters
    public Vector2 GetCoord() { return this.doorCoord; }
    public Vector2 GetDir() { return this.doorDir; }
    public bool GetFilled() { return filled; }
    //setters
    public void SetCoord(Vector2 doorCoord) { this.doorCoord = doorCoord; }
    public void SetFilled(bool filled) { this.filled = filled; }

    /// <summary>
    /// Enables a physical door.
    /// We start out with all the doors being blocked by "filler walls";
    /// once the generator decides that it wants to use this door, we unblock it so it's also unblocked in physical space
    /// (and acts like a real door!)
    /// Based on our current generation scheme, there shouldn't be any reason to "turn off" walls, but we can add that method if we need to.
    /// </summary>
    public void UnblockDoor() { physicalFiller.SetActive(false); filled = false; }

    //returns the grid that the door is pointing to.
    public Vector2 NextCoord()
    {
        return doorCoord + doorDir;
    }
}