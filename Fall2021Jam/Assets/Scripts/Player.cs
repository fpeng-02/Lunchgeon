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
    [SerializeField] private float reach;
    public ItemContainer Inventory {get; private set;}

    [field: SerializeField] public InventoryUIManager InventoryUI {get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Inventory = inventorySO.InventoryContainer;
    }

    // Update is called once per frame
    public override void Update()
    {
        if ((GetState() == State.Stunned) || (GetState() == State.Knocked)) return;

        base.Update();
        //Attacks
        if (Input.GetKey(KeyCode.Mouse0))
        {
            atk1.attack();
        }

        // Find interactable things, and tries to interact. Will only look at the "first" result from raycast.
        if (Input.GetButtonDown("Interact"))
        {
            LayerMask mask = LayerMask.GetMask("Interactable");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, reach, mask);
            hit.collider?.gameObject.GetComponent<IInteractable>().OnInteract();
        }

        if (Input.GetButtonDown("UseActiveItem"))
        {
            (inventorySO.ActiveItem.BaseItem as IUsableItem)?.ItemAction();
        }

        if (Input.GetButtonDown("OpenInventory"))
        {
            InventoryUI.ToggleBackpackEnable();
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
