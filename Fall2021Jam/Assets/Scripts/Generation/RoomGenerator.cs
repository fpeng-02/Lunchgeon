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
        private Vector2 pos;                // location of the lower left corner of this room, in global space
        private Node parent;                // parent of the node
        private List<Node> children;        // children of the node
        private Room repRoom;               // Room gameobject the node represents
        private List<DoorCoord> doorConfig; // copy of the list of DoorCoord of repRoom; tweaked around to turn doors on/off

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
    void Start()
    {
        debug = new List<Vector3>();
        GenerateNodes();
    }

    public void GenerateNodes()
    {
        //variables used for branching/ postgen
        branchLength = 0;
        leafList = new List<Node>();    // List of leaf nodes (prefer not to branch from here)
        nodeList = new List<Node>();    // List of internal nodes (prefer to branch from here)

        //create first start room node
        startNode = new Node(new Vector2(0, 0), null, GenerateRoom(startRoom.gameObject, new Vector2(0, 0)));
        UpdateFill(startNode);
        nodeList.Add(startNode); // Add root node as an internal node

        /*
        The basic procedure for generation is like this:
        1.  Based on the current room we're selecting, try to find any room from the pool of all possible rooms.
        2.  This is done by first finding all "valid exit doors" from the current room, i.e. rooms that allow at least one of the possible rooms to be placed down without issue.
            Then, we choose a random door out of the valid ones.
        3.  Then, we generate a list of possible rooms to be placed down that connects to the door we've selected.
            (We know that this list is at least size 1, otherwise this door would not have been selected in Step 1.)
        4.  We place the room down, update fill (which means: mark the squares that it occupies as filled so we don't place down an overlapping room),
            then set the "current room" to the new room we've placed down.
        5.  Decide whether we should continue down our current branch or create a new one.
            The longer a branch is, the more likely we are to quit and start a new branch
        Additional notes on generation:
        -   Whenever we make a new node, it's based off of some Room data; we need to copy the door list of the Room.
        */
        currNode = startNode;
        Node nextNode = null;
        while (roomCount < maxRoom)
        {
            // Add the next room to the graph.
            nextNode = FindNextRoom(currNode);
            while (nextNode == null)
            {
                // If we can't find any rooms to put down, keep trying to find a new branch root until there's somewhere we can put a room
                nodeList.Remove(currNode);
                currNode = NewBranchRoot();
                nextNode = FindNextRoom(currNode);
            }
            UpdateFill(nextNode);
            currNode.AddChild(nextNode);
            roomCount++;
            currNode = nextNode;

            //Branching
            branchLength += 1;
            if (DetermineBranch(branchLength))
            {
                leafList.Add(currNode);
                currNode = NewBranchRoot();
                branchLength = 0;
            }
            else
            {
                nodeList.Add(currNode);
            }
        }
    }

    //Determine whether to branch or not given a branch length
    private bool DetermineBranch(int bl)
    {
        float branchChance = bl * branchIncrement;
        if (branchChance > Random.Range(0f, 1f))
        {
            return true;
        }
        return false;
    }

    private Node NewBranchRoot() { return nodeList.Count > 0 ? nodeList[Random.Range(0, nodeList.Count)] : leafList[Random.Range(0, leafList.Count)]; }

    public class Candidate
    {
        public DoorCoord toDoorCoord;
        public DoorCoord fromDoorCoord;
        public Room room;
        public Vector2 anchorPoint;
        public Candidate(Room room, DoorCoord fromDoorCoord, DoorCoord toDoorCoord, Vector2 anchorPoint)
        {
            this.fromDoorCoord = fromDoorCoord;
            this.toDoorCoord = toDoorCoord;
            this.room = room;
            this.anchorPoint = anchorPoint;
        }
    }
    public Node FindNextRoom(Node currNode)
    {
        bool doorValid;
        Vector2 testAnchor = new Vector2(0, 0);
        List<Candidate> candidates = new List<Candidate>();
        foreach (DoorCoord currDoor in currNode.GetDoorCoords()) {
            if (currDoor.GetFilled()) continue;     // ignore doors that have already been used (e.g. if curr node is a new branch root, some doors may be used already)
            foreach (Room candRoom in roomPool) {
                foreach (DoorCoord candDoor in candRoom.GetDoorCoords()) {
                    if (candDoor.GetDir() != -currDoor.GetDir()) continue;  // only look for doors that are aligned with the current one used in generation
                    doorValid = true;  // assume the current door is valid; if we find that placement fails, this will become false
                    testAnchor = currDoor.NextCoord() + currNode.GetPos() - candDoor.GetCoord();
                    foreach (Vector2 fillTest in candRoom.GetFill()) {  // test the situation: if we placed a room down using this door for alignment, will it overlap?
                        if (occupiedCoord.Contains(testAnchor + fillTest)) {
                            doorValid = false;
                            break;
                        }
                    }
                    if (doorValid)
                        candidates.Add(new Candidate(candRoom, currDoor, candDoor, new Vector2(testAnchor.x, testAnchor.y)));
                }
            }
        }
        if (candidates.Count == 0) {
            Debug.Log("No valid rooms found!");
            return null;
        }
        else {
            Candidate chosen = candidates[Random.Range(0, candidates.Count)];   // chooses random room *template* from possible choices
            currNode.GetRoom().UnblockDoor(chosen.fromDoorCoord);    // opens door of current room
            Room newRepRoom = GenerateRoom(chosen.room.gameObject, chosen.anchorPoint); // instantiates the appropriate prefab from our choice
            newRepRoom.UnblockDoor(chosen.toDoorCoord);     // opens door of next room
            debug.Add(new Vector3(chosen.anchorPoint.x, chosen.anchorPoint.y, -5)); // for gizmos
            return new Node(chosen.anchorPoint, currNode, newRepRoom);
        }
    }

    //Updates room Count and adds the spaces of the newNode to the occupiedCoord list.
    public void UpdateFill(Node newNode)
    {
        //NOTE: USER DEFINED CLASSES ARE PASSED BY VALUE NOT REFERENCE
        newNode.GetRoom().GetFill().ForEach(fillSquare => occupiedCoord.Add(fillSquare + newNode.GetPos()));
    }
    /// <summary>
    /// Places the room down in world space, based on our grid coordinates.
    /// </summary>
    /// <param name="templateRoom"></param>
    /// <param name="coords"></param>
    /// <returns> Returns the instance of Room attached to the generated prefab. </returns>
    public Room GenerateRoom(GameObject templateRoom, Vector2 coords)
    {
        GameObject spawnedRoom = Instantiate(templateRoom, coords * roomSize, templateRoom.transform.rotation);
        return spawnedRoom.GetComponent<Room>();
    }
    public void DrawAllGizmos(Node startR)
    {
        if (startR.GetChildren().Count == 0) {
            return;
        }
        foreach (Node childNode in startR.GetChildren()) {
            Gizmos.DrawLine(startR.GetPos() * roomSize, childNode.GetPos() * roomSize);
            DrawAllGizmos(childNode);
        }

    }

    public void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        if (startNode != null) DrawAllGizmos(startNode);
    }
}
