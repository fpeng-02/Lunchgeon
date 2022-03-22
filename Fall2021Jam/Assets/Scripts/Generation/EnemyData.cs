using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Class: EnemyData
 * EnemyData class that contains meta data about a enemy spawn.
*/
[System.Serializable]
public class EnemyData
{
    [SerializeField] private Vector2 enemyPos; //vector 2 position of the enemy (relative to the current room)
    [SerializeField] private GameObject enemyGO; //actual enemy GO
    // Start is called before the first frame update


    public EnemyData(Vector2 enemyPos, GameObject enemyGO)
    {
        this.enemyPos = enemyPos;
        this.enemyGO = enemyGO;
    }

    public GameObject GetEnemyGO() { return enemyGO; }
    public Vector2 GetEnemyPos() { return enemyPos; }

}
