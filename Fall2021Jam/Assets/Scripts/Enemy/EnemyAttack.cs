using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Possibly add differnt classes for melee ranged depending on need
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    [SerializeField] protected GameObject attackObject;
    [SerializeField] protected float offsetDistance;
    [SerializeField] protected float velocity;


    protected float curCooldown = 0;
    protected Transform playerTransform;

    protected virtual void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    public virtual void Attack() { }

    protected virtual void FixedUpdate()
    {
        if (curCooldown > 0)
        {
            curCooldown = curCooldown - Time.deltaTime;
        }

    }
}
