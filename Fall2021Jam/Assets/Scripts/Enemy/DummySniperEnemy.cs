using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySniperEnemy : Enemy
{
    [SerializeField] private float cycleLength = 1.0f;
    [SerializeField] private float baseMoveSpeed = 0.5f;
    private float countdown;
    private Vector3 dirVect;
    private Transform playerTransform;
    [SerializeField] private EnemyAttack atk1;

    [SerializeField] private float attackDistance = 2.0f;
    [SerializeField] private float attackPause = 1f;
    private float attackTimer;


    protected override void Start()
    {
        base.Start();
        SetState(State.Regular);
        countdown = -1;
        playerTransform = GameObject.Find("Player").transform;

        attackTimer = attackPause;
    }

    public override void Update()
    {
        base.Update();
        /* every 3 seconds, randoly choose a direction to move in
        if (countdown > 0) countdown -= Time.deltaTime;
        else {
            countdown = cycleLength;
            dirVect = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;
        }
        */

        //Move towards
        dirVect = playerTransform.position - this.transform.position;
        dirVect.z = 0;
        if (dirVect.magnitude < attackDistance)
        {
            SetState(State.Attacking);
        }
        dirVect = Vector3.Normalize(dirVect);



    }
    public void FixedUpdate()
    {

        switch (GetState())
        {
            case State.Stunned:
                break;
            case State.Knocked:
                if (rb.velocity.magnitude < 1.0f) SetState(State.Regular);
                break;
            case State.Attacking:

                //Code for attacking

                if (attackTimer < 0.5)
                {
                    atk1.Attack();
                }
                if (attackTimer <= 0)
                {
                    attackTimer = attackPause;
                    SetState(State.Regular);
                }
                else
                {
                    attackTimer = attackTimer - Time.deltaTime;
                }


                break;
            default:
                rb.MovePosition(rb.transform.position + dirVect * baseMoveSpeed * Time.deltaTime);
                break;
        }
    }

    public override void ApplyHit(float damage, Vector3 vector)
    {
        base.ApplyHit(damage, vector);
        SetState(State.Knocked);
    }
}
