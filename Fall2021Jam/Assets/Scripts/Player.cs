using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float baseMoveSpeed = 4;
    private float h;
    private float v;
    private Vector3 dirVect;
    private Rigidbody2D rb;
    [SerializeField] private PlayerAttack1 atk1;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //Attacks
        if (Input.GetKey(KeyCode.Mouse0)) {
            atk1.attack();
        }


        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirVect = (new Vector3(h, v, 0)).normalized;
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.transform.position + dirVect * baseMoveSpeed * Time.deltaTime);
    }
}
