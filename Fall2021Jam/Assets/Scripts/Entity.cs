using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive;
    //knockback multiplier

    public void setHealth(float health)
    {
        this.health = health;
    }
    public float getHealth()
    {
        return health;
    }
    public virtual void applyHit(float damage, Vector3 vector)
    {
        health -= damage;
    }

    public virtual void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
