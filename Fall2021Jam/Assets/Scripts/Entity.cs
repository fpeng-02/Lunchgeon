using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    private bool isAlive = true;
    private State currState;

    //knockback multiplier
    public float GetMaxHealth() { return maxHealth; }

    public void SetHealth(float health) {this.health = health;}
    public float GetHealth() {return health;}
    public void SetState(State currState) { this.currState = currState; }
    public State GetState() { return currState; }

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void ApplyHeal(float healAmount) {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public virtual void ApplyHit(float damage, Vector3 vector)
    {
        health -= damage;
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);
    }

    public virtual void Update()
    {
        if (rb.IsSleeping())
        {
            rb.WakeUp();
        }

        if (health <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}


//TODO: Implement states, (make an IENUM), add state for stunned when knockback (player cant input movement and enemies can't change their pathing). Knockback state ends
//when velocity from knockback goes under a certain speed (knock ends).