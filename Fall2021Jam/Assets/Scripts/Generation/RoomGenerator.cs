using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    /*Class: Node
     * node class that contains the information for one room. 
    */
    public class Node
    {
        //local vars
        private Vector2 pos;       //location of the lower left corner of this room, in global space
        private Node parent;                //parent of the node
        private List<Node> children;        //children of the node
        private Room repRoom;               //Room gameobject the node represents

        //constructor 
        public Node(Vector2 pos, Node parent, Room repRoom)
        {
            this.pos = pos;
            this.parent = parent;
            this.repRoom = repRoom;
            children = new List<Node>();
        }

        //Getters
        public Vector2 GetPos() { return this.pos; }
        public List<DoorCoord> GetDoorCoords() { return repRoom.GetDoorCoords(); }
        public List<Vector2> GetFill() { return repRoom.GetFill(); }
        public Room GetRoom() { return this.repRoom; }

        //Add a child node to the child list
        public void AddChild(Node child)
        {
            children.Add(child);
        }

        //choose a random (kinda?) door from the availible non closed doors
    }

    [SerializeField] private float roomSize;
    [SerializeField] private List<Room> rooms;
    [SerializeField] private Room startRoom;
    [SerializeField] private int maxRoom;


    private List<Vector2> occupiedCoord = new List<Vector2>();

    private Node startNode;     //start node/root node
    private Node currNode;      //current/working node
    private DoorCoord currDoor; //current/working door
    private int roomCount;      //overall room count

    private Vector2 nextDoorSquare = new Vector2(0, 0); //offset from origin


    void Start()
    {
        generateNodes();
    }

    public void generateNodes()
    {
        //create first start room node
        startNode = new Node(new Vector2(0, 0), null, startRoom);
        currNode = startNode;
        generateRoom(startNode.GetRoom().gameObject, startNode.GetPos());
        InitNode(currNode);
        
        while (roomCount < maxRoom)
        {
            currDoor = chooseDoor(currNode);
            if (currDoor == null)
            {
                Debug.Log("TODO: No more open doors in the current room!");
            }
            nextDoorSquare = currDoor.NextCoord() + currNode.GetPos();
            Node newNode = checkRoom();
            generateRoom(newNode.GetRoom().gameObject, newNode.GetPos());
            InitNode(newNode);
            currNode.AddChild(newNode);
            currNode = newNode;
            //TODO BRANCH STOPPING MEKANISM.
        }
    }

    public Node checkRoom()
    {
        Vector2 testPosition;
        int randRoomInd = (int)Random.Range(0, rooms.Count);
        //check all rooms starting from a random room
        for (int i = 0; i < rooms.Count; i++)
        {
            // TODO: current algorithm is less optimal because if we hit a room that doesn't work, we always loop to a room, effectively increasing its chance;
            // Better to check all rooms, generate a set of valid ones, then choose a random one from that.
            Room randRoom = rooms[(randRoomInd + i) % (int)rooms.Count];
            List<DoorCoord> randRoomDoors = randRoom.GetDoorCoords();
            int randDoorOffset = (int)Random.Range(0, randRoomDoors.Count);
            //for every room check if there are doors that match the direction of our current door
            for (int j = 0; j < randRoomDoors.Count; j++)
            {
                //If there is an opposite corresponding door on the room to the current door, 
                //check if the current room fits if the two doors are attached.
                if (randRoomDoors[(j + randDoorOffset) % randRoomDoors.Count].GetDir() == -currDoor.GetDir())
                {
                    //Moves the offset to the bottom left of the Room
                    testPosition = nextDoorSquare - randRoomDoors[j].GetCoord();

                    bool isValid = true;
                    List<Vector2> randRoomFill = randRoom.GetFill();
                    //check if every space in that the room would occupy is full 
                    for (int k = 0; k < randRoomFill.Count; k++)
                    {
                        //if the offsetted piece of room is already taken, the room is invalid and the loop ends.
                        if (occupiedCoord.Contains(testPosition + randRoomFill[k]))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    //If all spots of the room are valid, then return a new node with 
                    if (isValid)
                    {
                        return new Node(testPosition, currNode, randRoom);
                    }

                }
            }
        }
        Debug.Log("TODO: No valid rooms found!");
        return null;
    }

    //Chose an open door 
    public DoorCoord chooseDoor(Node currNode)
    {
        int counter = 0;
        List<DoorCoord> doors = currNode.GetDoorCoords();
        int numInd = doors.Count;

        DoorCoord randDoor;
        //start at a random door in the door array
        int randInd = (int)Random.Range(0, numInd);
        //check consequtively increasing doors, starting at the random starting point, to find a non filled door
        while (counter < numInd)
        {
            randDoor = doors[(randInd + counter) % ((int)numInd)];
            //if door is not filled && the spot that the door leads to is not filled then return it
            if (!randDoor.getFilled() && !occupiedCoord.Contains(randDoor.NextCoord() + currNode.GetPos()))
            {
                return randDoor;
            }
            counter += 1;
        }
        return null;
    }

    //Updates room Count and adds the spaces of the newNode to the occupiedCoord list.
    public void InitNode(Node newNode)
    {
        //NOTE: USER DEFINED CLASSES ARE PASSED BY VALUE NOT REFERENCE
        newNode.GetFill().ForEach(fillSquare => occupiedCoord.Add(fillSquare + newNode.GetPos()));
        roomCount++;
    }

    /*//Add all the spaces in coord to occupiedCoord.
    public void AddToOccupied(List<Vector2> coord)
    {
        for (int i = 0; i < coord.Count; i++)
        {
            occupiedCoord.Add(coord[i]);
        }
    }*/

    //Convert The Nodes Into Stage GameObjects
    public void generateStage()
    {

    }


    public void generateRoom(GameObject room, Vector2 coords)
    {
        Instantiate(room, coords * roomSize, room.transform.rotation);
    }
}
