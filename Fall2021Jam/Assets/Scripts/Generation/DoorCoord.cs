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
    [SerializeField] private GameObject PhysicalDoor;
    [SerializeField] private GameObject PhysicalFiller;
    private bool filled;        //is the door filled already? (default false)

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

    //returns the grid that the door is pointing to.
    public Vector2 NextCoord()
    {
        return doorCoord + doorDir;
    }
}