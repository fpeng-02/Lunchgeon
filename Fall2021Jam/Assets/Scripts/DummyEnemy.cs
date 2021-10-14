using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Entity
{
    [SerializeField] private float cycleLength = 1.0f;
    [SerializeField] private float baseMoveSpeed = 3.0f;
    private float countdown;
    private Vector3 dirVect;
    private Rigidbody2D rb;
    private RoomEvent roomEvent;

    void Start()
    {
        setState(State.Regular);
        countdown = -1;
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetRoomEvent(RoomEvent re)
    {
        this.roomEvent = re;
    }

    public override void Update()
    {
        base.Update();
        // every 3 seconds, randoly choose a direction to move in
        if (countdown > 0) countdown -= Time.deltaTime;
        else {
            countdown = cycleLength;
            dirVect = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized;
        }
    }
    public void FixedUpdate()
    {
        switch (getState()){
            case State.Stunned:
                break;
            case State.Knocked:
                if (rb.velocity.magnitude < 1.0f)
                {
                    setState(State.Regular);
                }
                break;
            default:
                rb.MovePosition(rb.transform.position + dirVect * baseMoveSpeed * Time.deltaTime);
                break;

        }

    }

    public override void applyHit(float damage, Vector3 vector)
    {
        base.applyHit(damage, vector);
        setState(State.Knocked);
    }

    public override void Die()
    {
        roomEvent.ProgressRoom();
        base.Die();
    }
}
