using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    private int roomProgress;
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private List<GameObject> roomTriggers;

    public void Start()
    {
        foreach (GameObject door in doors) {
            door.SetActive(false);
        }
    }

    public void StartRoom()
    {
        // Replace this with something more modular
        GameObject go = Instantiate(enemy, this.transform);
        go.GetComponent<Enemy>().SetRoomEvent(this);
        roomProgress = 1;

        // Close doors
        foreach (GameObject door in doors) {
            door.SetActive(true);
        }

        // Disable room triggers (don't want to start the room twice
        foreach (GameObject roomTrigger in roomTriggers) {
            roomTrigger.SetActive(false);
        }
    }

    public void ProgressRoom()
    {
        roomProgress -= 1;
        if (roomProgress <= 0) EndRoom();
    }

    public void EndRoom()
    {
        foreach (GameObject door in doors) {
            door.SetActive(false);
        }
    }
}
