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

    // Start is called before the first frame update
    void Start()
    {
        countdown = -1;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
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
        //rb.MovePosition(rb.transform.position + dirVect * baseMoveSpeed * Time.deltaTime);
    }
}
