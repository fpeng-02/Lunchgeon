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
    [SerializeField] private Attack atk1;
    [SerializeField] private PlayerInventory inventorySO;
    private ItemContainer inventory;
    
    public ItemContainer GetPlayerInventory() { return inventory; }


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = inventorySO.GetItemContainer();
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
        switch (GetState())
        {
            case State.Stunned:
                break;
            case State.Knocked:
                if (rb.velocity.magnitude < 1.0f) SetState(State.Regular);
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
