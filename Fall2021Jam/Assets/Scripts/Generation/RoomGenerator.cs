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
        public List<Node> GetChildren() { return this.children; }

        //Add a child node to the child list
        public void AddChild(Node child)
        {
            children.Add(child);
        }

        //choose a random (kinda?) door from the availible non closed doors
    }

    [SerializeField] private float roomSize;
    [SerializeField] private List<Room> roomPool;
    [SerializeField] private Room startRoom;
    [SerializeField] private int maxRoom;
    [SerializeField] private float branchIncrement;


    private List<Vector3> debug; // stores anchor of each generated room, connecting them will give a sort of "path" that gen followed


    private List<Vector2> occupiedCoord = new List<Vector2>();

    private Node startNode;     //start node/root node
    private Node currNode;      //current/working node
    private int roomCount;      //overall room count

    private Vector2 nextDoorSquare = new Vector2(0, 0); //offset from origin

    private int branchLength;
    private List<Node> leafList;
    private List<Node> nodeList;

    private GameObject prevRoom;
    private GameObject currRoom;


    void Start()
    {
        debug = new List<Vector3>();
        GenerateNodes();
    }

    public void GenerateNodes()
    {
        //create first start room node
        startNode = new Node(new Vector2(0, 0), null, startRoom);
        currNode = startNode;
        Node nextNode = null;
        //GenerateRoom(startNode.GetRoom().gameObject, startNode.GetPos());
        UpdateFill(currNode);

        //variables used for branching/ postgen
        branchLength = 0;
        leafList = new List<Node>();
        nodeList = new List<Node>();

        nodeList.Add(startNode);


        //variables used for connecting passageways between two rooms
        prevRoom = null;
        currRoom = null;

        while (roomCount < maxRoom)
        {
            //Debug.Log(currDoor);
            //Write fail condition for Checkroom (Currently never fails because the 1x1 preset will always fill small spaces)
            //Debug.Log("cumm");
            nextNode = FindNextRoom(currNode);
            if (nextNode == null)
            {
                nodeList.Remove(currNode);

                currNode = nodeList.Count > 0 ? nodeList[Random.Range(0, nodeList.Count)] : leafList[Random.Range(0, leafList.Count)];
                continue;
            }

            //GenerateRoom(nextNode.GetRoom().gameObject, nextNode.GetPos());
            UpdateFill(nextNode);
            currNode.AddChild(nextNode);
            roomCount++;
            currNode = nextNode;


            //Branching
            branchLength += 1;
            if (DetermineBranch(branchLength))
            {
                leafList.Add(currNode);
                               
                currNode = nodeList.Count > 0 ? nodeList[Random.Range(0, nodeList.Count)] : leafList[Random.Range(0, leafList.Count)];
                branchLength = 0;
            }
            else
            {
                nodeList.Add(currNode);
            }
        }
        
        GenerateAllRooms(startNode);
    }

    //Determine whether to branch or not given a branch length
    public bool DetermineBranch(int bl)
    {
        float branchChance = bl * branchIncrement;
        if (branchChance > Random.Range(0f, 1f))
        {
            return true;
        }
        return false;
    }

    public Node FindNextRoom(Node currNode)
    {
        //Choose a door to create a new room out of
        DoorCoord currDoor = ChooseDoor(currNode); // Exit door to place new room
        

        //If no doors are available, branch to a new Node
        if (currDoor == null)
        {
            Debug.Log("TODO: No more open doors in the current room!");
            return null;
        }
        nextDoorSquare = currDoor.NextCoord() + currNode.GetPos();
        debug.Add(new Vector3(nextDoorSquare.x, nextDoorSquare.y, -5));


        Vector2 testPosition;
        int randRoomInd = (int)Random.Range(0, roomPool.Count);
        //check all rooms starting from a random room

        // For every room, check every door alignment for whether or not we can place it somewhere.
        bool doorValid;
        bool firstValidDoor;
        Vector2 testAnchor = new Vector2(0, 0);
        List<Room> validNewRooms = new List<Room>();
        List<List<Vector2>> validNewRoomCoords = new List<List<Vector2>>();  // list will be parallel to validNewRooms
        List<Vector2> t = null;

        foreach (Room room in roomPool) {
            firstValidDoor = true;  // used to make sure a list is initialized properly
            foreach (DoorCoord door in room.GetDoorCoords()) {
                if (door.GetDir() != -currDoor.GetDir()) continue;  // only look for doors that are aligned with the current one used in generation
                doorValid = true;  // assume the current door is valid; if we find that placement fails, this will become false
                testAnchor = nextDoorSquare - door.GetCoord();
                foreach (Vector2 fillTest in room.GetFill()) {  // test the situation: if we placed a room down using this door for alignment, will it overlap?
                    if (occupiedCoord.Contains(testAnchor + fillTest)) {
                        doorValid = false;
                        break;
                    }
                }
                if (doorValid) {
                    if (firstValidDoor) {
                        validNewRooms.Add(room);  // one valid door validates the room! also, this will only be hit once.
                        validNewRoomCoords.Add(new List<Vector2>());
                        firstValidDoor = false;
                        t = validNewRoomCoords[validNewRoomCoords.Count - 1];
                    }
                    t.Add(testAnchor);  // t will be initialized since the if condition is always hit before
                }
            }
        }
        if (validNewRooms.Count == 0) {
            Debug.Log("No valid rooms found!");
            return null;
        }
        else {
            int chosenRoomIndex = Random.Range(0, validNewRooms.Count);
            int chosenAnchorIndex = Random.Range(0, validNewRoomCoords[chosenRoomIndex].Count);
            //Debug.Log(validNewRoomCoords[chosenRoomIndex][chosenAnchorIndex]);
            return new Node(validNewRoomCoords[chosenRoomIndex][chosenAnchorIndex], currNode, validNewRooms[chosenRoomIndex]);
        }
    }

    //Chose an open door 
    public DoorCoord ChooseDoor(Node currNode)
    {
        int counter = 0;
        List<DoorCoord> validDoors = new List<DoorCoord>();

        foreach (DoorCoord door in currNode.GetDoorCoords()) {
            if (!door.getFilled() && !occupiedCoord.Contains(door.NextCoord() + currNode.GetPos())) {
                validDoors.Add(door);
            }
        }

        if (validDoors.Count == 0) return null;
        else return validDoors[Random.Range(0, validDoors.Count)];
    }

    //Updates room Count and adds the spaces of the newNode to the occupiedCoord list.
    public void UpdateFill(Node newNode)
    {
        //NOTE: USER DEFINED CLASSES ARE PASSED BY VALUE NOT REFERENCE
        newNode.GetFill().ForEach(fillSquare => occupiedCoord.Add(fillSquare + newNode.GetPos()));
    }

    //Convert The Nodes Into Stage GameObjects
    public void GenerateStage()
    {

    }


    public void GenerateRoom(GameObject room, Vector2 coords)
    {
        Instantiate(room, coords * roomSize, room.transform.rotation);
    }

    /// <summary>
    /// Places Room prefabs onto world space by traversing the generated Node graph recursively.
    /// Also configures the placed room's doors to blocked or open.
    /// </summary>
    /// <param name="startR"></param>
    public void GenerateAllRooms(Node startR)
    {
        Instantiate(startR.GetRoom(), startR.GetPos() * roomSize, startR.GetRoom().transform.rotation);
        if (startR.GetChildren().Count == 0)
        {
            return;
        }
        foreach (Node childNode in startR.GetChildren())
        {
            GenerateAllRooms(childNode);
        }

    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        for (int i = 0; i < debug.Count - 1; i++)
            Gizmos.DrawLine(debug[i] * roomSize, debug[i+1] * roomSize);
    }
}
