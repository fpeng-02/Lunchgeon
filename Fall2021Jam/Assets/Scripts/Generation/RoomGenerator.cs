using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/*Class: DoorCoord
 * Doorcoord class that contains the tile the door is on and the cardinal direction the door is facing
*/
public class DoorCoord : MonoBehaviour
{
    //local vars
    private Vector2 doorCoord;  //coords of the door
    private Vector2 doorDir;    //direcftion the door is facing
    private bool filled;        //Is the door filled already (default false)
    
    //constructor
    public DoorCoord(Vector2 coord, Vector2 dir)
    {
        doorCoord = coord;
        doorDir = dir;
        filled = false;
    }

    //getters
    public Vector2 GetCoord() { return this.doorCoord; }
    public Vector2 GetDir() { return this.doorDir; }
    public bool getFilled() { return filled; }
    //setters
    public void setCoord(Vector2 doorCoord) { this.doorCoord = doorCoord; }
    public void setFilled(bool filled) { this.filled = filled; }

    //returns the grid that the door is pointing to.
    public Vector2 NextCoord()
    {
        return doorCoord + doorDir;
    }
    
}

/*Class: Node
 * node class that contains the information for one room. 
*/
public class Node
{
    //local vars
    private List<Vector2> coord;        //list of coordinates the room occupies
    private List<DoorCoord> doorCoord;  //list of all the doors the room has.
    private Node parent;                //parent of the node
    private List<Node> children;        //children of the node
    private Room repRoom;               //Room gameobject the node represents

    //constructor 
    public Node(List<Vector2> coord, List<DoorCoord> doorCoord, Node parent, Room repRoom)
    {
        this.coord = coord;
        this.doorCoord = doorCoord;
        this.parent = parent;
        this.repRoom = repRoom;
    }

    //Getters
    public List<DoorCoord> GetDoorCoord(){ return doorCoord; }
    public List<Vector2> GetCoord() { return coord; }


    //Add a child node to the child list
    public void AddChild(Node child)
    {
        children.Add(child);
    }

    //choose a random (kinda?) door from the availible non closed doors.

}


public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private float roomSize = 10;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private Room startRoom;
    [SerializeField] private int maxRoom;


    private List<Vector2> occupiedCoord = new List<Vector2>();

    private Node startNode;     //start node/root node
    private Node currNode;      //current/working node
    private DoorCoord currDoor; //current/working door
    private int roomCount;      //overall room count

    private Vector2 offset = new Vector2(0, 0); //offset from origin


    void Start()
    {
        generateNodes();
    }

    public void generateNodes()
    {
        //create first start room node
        startNode = new Node(startRoom.GetCoord(), startRoom.GetDoors(), null, startRoom);
        currNode = startNode;
        InitNode(currNode);
        
        while (roomCount < maxRoom)
        {
            currDoor = chooseDoor(currNode);
            if (currDoor == null)
            {
                //TODO: CASE WHEN NO DOORS OPEN IN THE CURRENT ROOM
            }
            offset = currDoor.NextCoord();
            Node newNode = checkRoom();
            InitNode(newNode);
            currNode.AddChild(newNode);
            currNode = newNode;
            //TODO BRANCH STOPPING MEKANISM.


        }
    }

    public Node checkRoom()
    {
        Vector2 localOffset;
        int randRoomInd = (int)Random.Range(0, rooms.Count);
        //check all rooms starting from a random room
        for (int i = 0; i < rooms.Count; i++)
        {
            Room randRoom = rooms[(randRoomInd + i) / (int)rooms.Count];
            List<DoorCoord> randRoomDoors = randRoom.GetDoors();
            //for every room check if there are doors that match the direction of our current door
            for (int j = 0; j < randRoomDoors.Count; j++)
            {
                //If there is an opposite corresponding door on the room to the current door, 
                //check if the current room fits if the two doors are attached.
                if (randRoomDoors[j].GetDir() == -currDoor.GetDir())
                {
                    //Moves the offset to the bottom left of the Room
                    localOffset = offset - randRoomDoors[j].GetDir();

                    bool isValid = true;
                    List<Vector2> randRoomCoords = randRoom.GetCoord();
                    //check if every space in that the room would occupy is full 
                    for (int k = 0; k < randRoomCoords.Count; k++)
                    {
                        //if the offsetted piece of room is already taken, the room is invalid and the loop ends.
                        if (occupiedCoord.Contains(localOffset + randRoomCoords[k]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    //If all spots of the room are valid, then return a new node with 
                    if (isValid)
                    {

                        return new Node(randRoom.GetOffsetCoord(localOffset), randRoom.GetOffsetDoorCoord(localOffset), currNode, randRoom);
                    }

                }
            }
        }
        return null;
    }

    

    //Chose an open door 
    public DoorCoord chooseDoor(Node currNode)
    {
        int counter = 0;
        int numInd = currNode.GetDoorCoord().Count;

        DoorCoord newCoord;
        //start at a random door in the door array
        int randInd = (int)Random.Range(0, numInd);
        //check consequtively increasing doors, starting at the random starting point, to find a non filled door
        while (counter < numInd)
        {
            newCoord = currNode.GetDoorCoord()[(randInd + counter) / ((int)numInd)];
            //if door is not filled && the spot that the door leads to is not filled then return it
            if (!newCoord.getFilled() && !occupiedCoord.Contains(newCoord.NextCoord()))
            {
                return newCoord;
            }
            counter += 1;
        }
        return null;

    }

    //Updates room Count and adds the spaces of the newNode to the occupiedCoord list.
    public void InitNode(Node newNode)
    {
        //NOTE: USER DEFINED CLASSES ARE PASSED BY VALUE NOT REFERENCE
        AddToOccupied(newNode.GetCoord());
        roomCount++;
    }

    //Add all the spaces in coord to occupiedCoord.
    public void AddToOccupied(List<Vector2> coord)
    {
        for (int i = 0; i < coord.Count; i++)
        {
            occupiedCoord.Add(coord[i]);
        }
    }

    //Convert The Nodes Into Stage GameObjects
    public void generateStage()
    {

    }


    public void generateRoom(GameObject room, Vector2 coords)
    {
        Instantiate(gameObject, coords * roomSize, room.transform.rotation);
    }
}
