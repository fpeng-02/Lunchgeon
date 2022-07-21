using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile: Hitbox
{
    public Vector3 initialPos { get; set; }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void FixedUpdate()
    {
        //destroy this gameobject after duration time
        setDuration(getDuration() - Time.deltaTime);
        if (getDuration() < 0)
        {
            Destroy(this.gameObject);
        }

        /*
        //Get all the colliders overlapping with the overlapbox
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gameObjeczt.transform.position, transform.localScale, transform.eulerAngles.z);
        int i = 0;
        //iterate through hit colliders and apply the hit to enemies
        while (i < hitColliders.Length)
        {
            //Add hit detection for walls etc.
            if (hitColliders[i].gameObject.tag == "Player")
            {
                
                Vector3 knock = getKnock() * new Vector3(this.transform.up.x, this.transform.up.y, 0);
                Debug.Log("Hit Sent!");
                hitColliders[i].gameObject.GetComponent<Entity>().ApplyHit(getDamage(), knock);

                Destroy(this.gameObject);
            }
            i++;
        }
        */
    }

    private void ProcessHit(GameObject target)
    {
        Vector3 knock = getKnock() * Vector3.Normalize(target.transform.position - initialPos);
        target.GetComponent<Entity>().ApplyHit(getDamage(), knock);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigenter");
        if (collision.gameObject.tag == getTargetTag())
        {
            Debug.Log("Player hit");
            ProcessHit(collision.gameObject);
        }

    }

}