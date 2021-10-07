using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 4;
    private float h;
    private float v;
    private Vector3 dirVect;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        dirVect = (new Vector3(h, v, 0)).normalized;
        rb.MovePosition(rb.transform.position + dirVect * baseMoveSpeed * Time.deltaTime);
    }
}
