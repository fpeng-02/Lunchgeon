using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive = true;
    private State currState;

    //knockback multiplier

    public void SetHealth(float health) {this.health = health;}
    public float GetHealth() {return health;}
    public void SetState(State currState) { this.currState = currState; }
    public State GetState() { return currState; }

    public virtual void ApplyHit(float damage, Vector3 vector)
    {
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


//TODO: Implement states, (make an IENUM), add state for stunned when knockback (player cant input movement and enemies can't change their pathing). Knockback state ends
//when velocity from knockback goes under a certain speed (knock ends).