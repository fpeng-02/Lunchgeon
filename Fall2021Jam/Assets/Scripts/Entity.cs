using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private float health;
    private bool isAlive;
    public void setHealth(float health)
    {
        this.health = health;
    }
    public float getHealth()
    {
        return health;
    }
}
