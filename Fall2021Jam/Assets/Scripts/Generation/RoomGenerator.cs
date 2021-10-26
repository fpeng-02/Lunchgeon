using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DoorCoord : MonoBehaviour
{
    private Vector2 doorCoord;
    private Vector2 doorDir; 
    
    public DoorCoord(Vector2 coord, Vector2 dir)
    {
        doorCoord = coord;
        doorDir = dir;
    }

    public Vector2 GetCoord() { return this.doorCoord; }
    public Vector2 GetDir() { return this.doorDir; }
    public void setCoord(Vector2 doorCoord) { this.doorCoord = doorCoord; }

    public Vector2 NextCoord()
    {
        return doorCoord + doorDir;
    }
    
}

public class Node
{
    private List<Vector2> coord;
    private List<DoorCoord> doorCoord;
    private Node parent;
    private List<Node> children;

    public Node(List<Vector2> coord, List<DoorCoord> doorCoord, Node parent)
    {
        this.coord = coord;
        this.doorCoord = doorCoord;
        this.parent = parent;
    }

    public void AddChild(Node child)
    {
        children.Add(child);
    }
}
public class RoomGenerator : MonoBehaviour
{
    [SerializeField] private static float roomSize = 10;
    [SerializeField] private static List<Room> rooms;
    [SerializeField] private static Room startRoom;

    void Start()
    {
        generateStage();
    }

    public void generateStage()
    {
        Node startNode = new Node(startRoom.GetCoord(), startRoom.GetDoors(), null);
        

    }

    public void generateRoom(GameObject room, Vector2 coords)
    {
        Instantiate(gameObject, coords * roomSize, room.transform.rotation);
    }
}
