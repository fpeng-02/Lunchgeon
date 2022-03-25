using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent : MonoBehaviour
{

    private List<EnemyData> enemyList;
    private int roomProgress;
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private List<GameObject> roomTriggers;

    // Should only be used for debugging things
    public void SetProgress(int newProg) { this.roomProgress = newProg;  }

    public void Start()
    {
        foreach (GameObject door in doors) {
            door.SetActive(false);
        }
        if (enemyList == null) enemyList = new List<EnemyData>();
    }

    

    public void StartRoom()
    {
        roomProgress = 0;
        
        // Basic implementation of spawning enemies based on preset
        foreach (EnemyData enemyGO in enemyList)
        {
            GameObject go = Instantiate(enemyGO.GetEnemyGO(), this.transform);
            go.GetComponent<Enemy>().SetRoomEvent(this);
            go.transform.localPosition = enemyGO.GetEnemyPos();

            roomProgress += 1;

        }

        // Close doors
        foreach (GameObject door in doors) {
            door.SetActive(true);
        }

        // Disable room triggers (don't want to start the room twice
        foreach (GameObject roomTrigger in roomTriggers) {
            roomTrigger.SetActive(false);
        }
    }

    public void addEnemy(EnemyData newEnemyData)
    {
        if (enemyList == null) enemyList = new List<EnemyData>();
        enemyList.Add(newEnemyData);
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
