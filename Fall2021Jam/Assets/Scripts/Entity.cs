using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float health;
    private bool isAlive = true;
    private State currState;

    //knockback multiplier

    public void setHealth(float health) {this.health = health;}
    public float getHealth() {return health;}
    public void setState(State currState) { this.currState = currState; }
    public State getState() { return currState; }

    public virtual void applyHit(float damage, Vector3 vector)
    {
        //Debug.Log("Hit Recieved!");
        health -= damage;
        gameObject.GetComponent<Rigidbody2D>().AddForce(vector, ForceMode2D.Impulse);

        //Debug.Log(health);
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}


//TODO: Implement states, (make an IENUM), add state for stunned when knockback (player cant input movement and enemies can't change their pathing). Knockback state ends
//when velocity from knockback goes under a certain speed (knock ends).