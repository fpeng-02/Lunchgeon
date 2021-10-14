using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive;
    //knockback multiplier

    public void SetHealth(float health)
    {
        this.health = health;
    }
    public float GetHealth()
    {
        return health;
    }
    public virtual void ApplyHit(float damage, Vector3 vector)
    {
        //Debug.Log("Hit Recieved!");
        health -= damage;
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);

        //Debug.Log(health);
    }

    public virtual void Update()
    {
        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
