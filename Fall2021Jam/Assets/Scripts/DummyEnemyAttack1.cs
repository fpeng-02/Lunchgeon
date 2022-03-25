using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemyAttack1 : Attack
{
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject attackObject;
    [SerializeField] private float offsetDistance;
    private float curCooldown = 0;

    private Transform playerTransform;

    public void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    public override void attack()
    {
        if (curCooldown <= 0)
        {
            //Find the direction the mouse is pointing and spawn an attack(gameobject) in that direction/rotation offsetDistance units away.
            Vector3 shootDirection = playerTransform.position - this.transform.position;
            shootDirection.z = 0;
            shootDirection = Vector3.Normalize(shootDirection);
            
            //calculate the angle of rotation
            float attackRotation = Mathf.Atan2(shootDirection.y, shootDirection.x);
            attackRotation = Mathf.Rad2Deg * attackRotation;
            //initialize new gameobject
            GameObject newAttack = Instantiate(attackObject, transform.position + shootDirection * offsetDistance, Quaternion.Euler(0, 0, attackRotation - 90), this.transform);

            //put the attack on cooldown
            curCooldown = cooldown;
        }
    }
    public void FixedUpdate()
    {
        if (curCooldown > 0)
        {
            curCooldown = curCooldown - Time.deltaTime;
        }

    }
}
