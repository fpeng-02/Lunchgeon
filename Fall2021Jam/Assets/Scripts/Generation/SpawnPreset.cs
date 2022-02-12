using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPreset : MonoBehaviour
{
    [SerializeField] private List<EnemyData> enemyList;

    void Start()
    {
        foreach (EnemyData enemyD in enemyList)
        {
            transform.parent.gameObject.GetComponentInChildren<RoomEvent>().addEnemy(enemyD);
        }
    }
}
