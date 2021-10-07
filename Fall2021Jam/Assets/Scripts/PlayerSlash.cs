using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : Hitbox
{
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        setDuration(getDuration() - Time.deltaTime);
        if (getDuration() < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Entity>().applyHit(getDamage(), getKnock());
        }   
    }
}
